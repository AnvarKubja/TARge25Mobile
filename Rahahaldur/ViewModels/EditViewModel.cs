using System.Globalization;
using System.Windows.Input;
using RahaHaldur.Models;
using RahaHaldur.Services;

namespace RahaHaldur.ViewModels;

// Lisamise / muutmise lehe loogika.
public class EditViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly NavigationService _nav;
    private readonly AudioService _audio;
    private readonly LocalizationService _loc;

    private MoneyEntry _entry;      // kirje, mida lisame või muudame
    private string _pageTitle;
    private string _title = "";
    private string _amountText = "";
    private bool _isIncome;
    private int _categoryIndex;

    public string PageTitle
    {
        get => _pageTitle;
        set
        {
            _pageTitle = value;
            OnPropertyChanged(nameof(PageTitle));
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public string AmountText
    {
        get => _amountText;
        set
        {
            _amountText = value;
            OnPropertyChanged(nameof(AmountText));
        }
    }

    public bool IsIncome
    {
        get => _isIncome;
        set
        {
            _isIncome = value;
            OnPropertyChanged(nameof(IsIncome));
            OnPropertyChanged(nameof(IsExpense)); // kategooria valik näitab/peidab end
        }
    }

    // Kategooria valik näidatakse ainult kulude puhul
    public bool IsExpense
    {
        get => !_isIncome;
    }

    public int CategoryIndex
    {
        get => _categoryIndex;
        set
        {
            if (value >= 0) // Picker võib hetkeks anda -1
            {
                _categoryIndex = value;
                OnPropertyChanged(nameof(CategoryIndex));
            }
        }
    }

    // Kategooriate nimed valikuloendi jaoks (tõlgitud)
    public List<string> CategoryNames { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public EditViewModel(DatabaseService db, NavigationService nav, AudioService audio, LocalizationService loc)
    {
        _db = db;
        _nav = nav;
        _audio = audio;
        _loc = loc;

        CategoryNames = new List<string>();
        foreach (string category in Categories.All)
            CategoryNames.Add(_loc["Cat_" + category]);

        if (AppState.EntryToEdit == null)
        {
            // Uus kirje
            _entry = new MoneyEntry();
            _pageTitle = _loc["AddEntry"];
        }
        else
        {
            // Olemasoleva muutmine: täidame väljad kirje andmetega
            _entry = AppState.EntryToEdit;
            _pageTitle = _loc["EditEntry"];
            _title = _entry.Title;
            _amountText = _entry.Amount.ToString("0.##", CultureInfo.InvariantCulture);
            _isIncome = _entry.IsIncome;

            int index = Array.IndexOf(Categories.All, _entry.Category);
            if (index >= 0)
                _categoryIndex = index;
        }

        SaveCommand = new Command(Save);
        CancelCommand = new Command(Cancel);
    }

    private async void Cancel()
    {
        await _nav.GoBackAsync();
    }

    private async void Save()
    {
        // Komakoht tehakse punktiks, et "12,5" oleks sama mis "12.5"
        string text = AmountText.Replace(',', '.');
        double amount;
        bool amountOk = double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out amount);

        if (string.IsNullOrWhiteSpace(Title) || !amountOk || amount <= 0)
        {
            await _nav.AlertAsync(_loc["ErrorTitle"], _loc["ErrorMessage"], _loc["Ok"]);
            return;
        }

        _entry.Title = Title.Trim();
        _entry.Amount = amount;
        _entry.IsIncome = IsIncome;

        if (IsIncome)
            _entry.Category = Categories.Income;
        else
            _entry.Category = Categories.All[CategoryIndex];

        await _db.SaveAsync(_entry);
        await _audio.PlayEffectAsync("coin.wav");
        await _nav.GoBackAsync();
    }
}
