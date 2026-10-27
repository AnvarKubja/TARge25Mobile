using System.Collections.ObjectModel;
using System.Windows.Input;
using RahaHaldur.Models;
using RahaHaldur.Services;

namespace RahaHaldur.ViewModels;

// Drag & Drop leht: lohista kulu kategooria peale, et kategooriat muuta.
public class SortViewModel : BaseViewModel
{
    private readonly DatabaseService _db;
    private readonly AudioService _audio;
    private readonly LocalizationService _loc;

    private MoneyEntry? _dragged; // kulu, mida parasjagu lohistatakse
    private ObservableCollection<MoneyEntry> _expenses = new();

    public ObservableCollection<MoneyEntry> Expenses
    {
        get => _expenses;
        set => SetProperty(ref _expenses, value);
    }

    // Kutsutakse, kui kasutaja hakkab kulu lohistama
    public ICommand DragStartCommand { get; }

    // Kutsutakse, kui kulu visatakse kategooria peale (parameeter = kategooria nimi)
    public ICommand DropCommand { get; }

    public SortViewModel(DatabaseService db, AudioService audio, LocalizationService loc)
    {
        _db = db;
        _audio = audio;
        _loc = loc;

        DragStartCommand = new Command<MoneyEntry>(entry => _dragged = entry);

        DropCommand = new Command<string>(async category =>
        {
            if (_dragged == null)
                return;

            _dragged.Category = category;
            await _db.SaveAsync(_dragged);
            _dragged = null;

            await _audio.PlayEffectAsync("click.wav");
            await LoadAsync();
        });

        _loc.LanguageChanged += async () => await LoadAsync();
    }

    public async Task LoadAsync()
    {
        var list = (await _db.GetAllAsync()).Where(e => !e.IsIncome).ToList();

        foreach (var entry in list)
            entry.CategoryText = _loc["Cat_" + entry.Category];

        Expenses = new ObservableCollection<MoneyEntry>(list);
    }
}
