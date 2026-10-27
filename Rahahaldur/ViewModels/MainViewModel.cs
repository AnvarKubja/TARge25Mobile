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

    private ObservableCollection<MoneyEntry> _entries = new();
    private string _incomeText = MoneyEntry.Format(0);
    private string _expenseText = MoneyEntry.Format(0);
    private string _balanceText = MoneyEntry.Format(0);

    public ObservableCollection<MoneyEntry> Entries
    {
        get => _entries;
        set => SetProperty(ref _entries, value);
    }

    public string IncomeText
    {
        get => _incomeText;
        set => SetProperty(ref _incomeText, value);
    }

    public string ExpenseText
    {
        get => _expenseText;
        set => SetProperty(ref _expenseText, value);
    }

    public string BalanceText
    {
        get => _balanceText;
        set => SetProperty(ref _balanceText, value);
    }

    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }

    public MainViewModel(DatabaseService db, NavigationService nav, AudioService audio, LocalizationService loc)
    {
        _db = db;
        _nav = nav;
        _audio = audio;
        _loc = loc;

        AddCommand = new Command(async () => await _nav.GoToAsync("EditPage"));

        EditCommand = new Command<MoneyEntry>(async entry =>
        {
            var parameters = new ShellNavigationQueryParameters { { "Item", entry } };
            await _nav.GoToAsync("EditPage", parameters);
        });

        DeleteCommand = new Command<MoneyEntry>(async entry =>
        {
            bool yes = await _nav.ConfirmAsync(_loc["DeleteTitle"], _loc["DeleteQuestion"], _loc["Yes"], _loc["No"]);
            if (!yes)
                return;

            await _db.DeleteAsync(entry);
            await _audio.PlayEffectAsync("delete.wav");
            await LoadAsync();
        });

        // Kui keel muutub, laeme nimekirja uuesti (kategooriate tõlked uuenevad)
        _loc.LanguageChanged += async () => await LoadAsync();
    }

    public async Task LoadAsync()
    {
        var list = await _db.GetAllAsync();

        foreach (var entry in list)
            entry.CategoryText = _loc["Cat_" + entry.Category];

        Entries = new ObservableCollection<MoneyEntry>(list);

        double income = list.Where(e => e.IsIncome).Sum(e => e.Amount);
        double expense = list.Where(e => !e.IsIncome).Sum(e => e.Amount);

        IncomeText = MoneyEntry.Format(income);
        ExpenseText = MoneyEntry.Format(expense);
        BalanceText = MoneyEntry.Format(income - expense);
    }
}
