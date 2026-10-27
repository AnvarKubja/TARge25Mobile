using System.Globalization;
using System.Windows.Input;
using RahaHaldur.Models;
using RahaHaldur.Services;

namespace RahaHaldur.ViewModels;

// Lisamise / muutmise lehe loogika.
// IQueryAttributable: nii saame ülevaatelehelt kaasa antud kirje.
public class EditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly DatabaseService _db;
    private readonly NavigationService _nav;
    private readonly AudioService _audio;
    private readonly LocalizationService _loc;

    private MoneyEntry _entry = new();
    private string _pageTitle;
    private string _title = "";
    private string _amountText = "";
    private bool _isIncome;
    private int _categoryIndex;

    public string PageTitle
    {
        get => _pageTitle;
        set => SetProperty(ref _pageTitle, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string AmountText
    {
        get => _amountText;
        set => SetProperty(ref _amountText, value);
    }

    public bool IsIncome
    {
        get => _isIncome;
        set
        {
            if (SetProperty(ref _isIncome, value))
                OnPropertyChanged(nameof(IsExpense));
        }
    }

    // Kategooria valik näidatakse ainult kulude puhul
    public bool IsExpense => !IsIncome;

    public int CategoryIndex
    {
        get => _categoryIndex;
        set
        {
            if (value >= 0) // Picker võib hetkeks anda -1
                SetProperty(ref _categoryIndex, value);
        }
    }

    public List<string> CategoryNames { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public EditViewModel(DatabaseService db, NavigationService nav, AudioService audio, LocalizationService loc)
    {
        _db = db;
        _nav = nav;
        _audio = audio;
        _loc = loc;

        _pageTitle = _loc["AddEntry"];
        CategoryNames = Categories.All.Select(c => _loc["Cat_" + c]).ToList();

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await _nav.GoBackAsync());
    }

    // Shell kutsub seda, kui lehele anti kaasa kirje (muutmine)
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Item", out var value) && value is MoneyEntry entry)
        {
            _entry = entry;
            PageTitle = _loc["EditEntry"];
            Title = entry.Title;
            AmountText = entry.Amount.ToString("0.##", CultureInfo.InvariantCulture);
            IsIncome = entry.IsIncome;
            CategoryIndex = Math.Max(0, Array.IndexOf(Categories.All, entry.Category));
        }
    }

    private async Task SaveAsync()
    {
        var text = AmountText.Replace(',', '.'); // lubame nii koma kui punkti

        if (string.IsNullOrWhiteSpace(Title) ||
            !double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var amount) ||
            amount <= 0)
        {
            await _nav.AlertAsync(_loc["ErrorTitle"], _loc["ErrorMessage"], _loc["Ok"]);
            return;
        }

        _entry.Title = Title.Trim();
        _entry.Amount = amount;
        _entry.IsIncome = IsIncome;
        _entry.Category = IsIncome ? Categories.Income : Categories.All[CategoryIndex];

        await _db.SaveAsync(_entry);
        await _audio.PlayEffectAsync("coin.wav");
        await _nav.GoBackAsync();
    }
}
