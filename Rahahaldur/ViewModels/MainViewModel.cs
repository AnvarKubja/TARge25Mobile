using System.Collections.ObjectModel;
using System.Windows.Input;
using RahaHaldur.Models;
using RahaHaldur.Services;

namespace RahaHaldur.ViewModels;

// Ülevaate lehe loogika: nimekiri, summad, lisa/muuda/kustuta.
public class MainViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly NavigationService _nav;
    private readonly AudioService _audio;
    private readonly LocalizationService _loc;

    private ObservableCollection<MoneyEntry> _entries = new ObservableCollection<MoneyEntry>();
    private string _incomeText = "0.00 €";
    private string _expenseText = "0.00 €";
    private string _balanceText = "0.00 €";

    // Nimekiri, mida ekraan näitab
    public ObservableCollection<MoneyEntry> Entries
    {
        get => _entries;
        set
        {
            _entries = value;
            OnPropertyChanged(nameof(Entries));
        }
    }

    public string IncomeText
    {
        get => _incomeText;
        set
        {
            _incomeText = value;
            OnPropertyChanged(nameof(IncomeText));
        }
    }

    public string ExpenseText
    {
        get => _expenseText;
        set
        {
            _expenseText = value;
            OnPropertyChanged(nameof(ExpenseText));
        }
    }

    public string BalanceText
    {
        get => _balanceText;
        set
        {
            _balanceText = value;
            OnPropertyChanged(nameof(BalanceText));
        }
    }

    // Käsud (nupuvajutused), mida ekraan kasutab
    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }

    public MainViewModel(DatabaseService db, NavigationService nav, AudioService audio, LocalizationService loc)
    {
        _db = db;
        _nav = nav;
        _audio = audio;
        _loc = loc;

        AddCommand = new Command(AddEntry);
        EditCommand = new Command<MoneyEntry>(EditEntry);
        DeleteCommand = new Command<MoneyEntry>(DeleteEntry);

        // Kui keel muutub, laeme nimekirja uuesti (kategooriate tõlked uuenevad)
        _loc.LanguageChanged += OnLanguageChanged;
    }

    private async void AddEntry()
    {
        AppState.EntryToEdit = null; // null = uus kirje
        await _nav.GoToAsync("EditPage");
    }

    private async void EditEntry(MoneyEntry entry)
    {
        AppState.EntryToEdit = entry;
        await _nav.GoToAsync("EditPage");
    }

    private async void DeleteEntry(MoneyEntry entry)
    {
        bool yes = await _nav.ConfirmAsync(_loc["DeleteTitle"], _loc["DeleteQuestion"], _loc["Yes"], _loc["No"]);
        if (!yes)
            return;

        await _db.DeleteAsync(entry);
        await _audio.PlayEffectAsync("delete.wav");
        await LoadAsync();
    }

    private async void OnLanguageChanged()
    {
        await LoadAsync();
    }

    // Loeb andmebaasist kõik kirjed ja arvutab summad
    public async Task LoadAsync()
    {
        List<MoneyEntry> list = await _db.GetAllAsync();

        double income = 0;
        double expense = 0;

        foreach (MoneyEntry entry in list)
        {
            entry.CategoryText = _loc["Cat_" + entry.Category];

            if (entry.IsIncome)
                income = income + entry.Amount;
            else
                expense = expense + entry.Amount;
        }

        Entries = new ObservableCollection<MoneyEntry>(list);

        IncomeText = MoneyEntry.Format(income);
        ExpenseText = MoneyEntry.Format(expense);
        BalanceText = MoneyEntry.Format(income - expense); // saldo = tulud - kulud
    }
}
