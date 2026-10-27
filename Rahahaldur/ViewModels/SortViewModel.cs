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

    private MoneyEntry _dragged; // kulu, mida parasjagu lohistatakse
    private ObservableCollection<MoneyEntry> _expenses = new ObservableCollection<MoneyEntry>();

    public ObservableCollection<MoneyEntry> Expenses
    {
        get => _expenses;
        set
        {
            _expenses = value;
            OnPropertyChanged(nameof(Expenses));
        }
    }

    // Käivitub, kui kasutaja hakkab kulu lohistama
    public ICommand DragStartCommand { get; }

    // Käivitub, kui kulu visatakse kategooria peale (parameeter = kategooria nimi)
    public ICommand DropCommand { get; }

    public SortViewModel(DatabaseService db, AudioService audio, LocalizationService loc)
    {
        _db = db;
        _audio = audio;
        _loc = loc;

        DragStartCommand = new Command<MoneyEntry>(DragStart);
        DropCommand = new Command<string>(Drop);
    }

    // Jätame lohistatava kulu meelde
    private void DragStart(MoneyEntry entry)
    {
        _dragged = entry;
    }

    // Paneme meelde jäetud kulule uue kategooria ja salvestame
    private async void Drop(string category)
    {
        if (_dragged == null)
            return;

        _dragged.Category = category;
        await _db.SaveAsync(_dragged);
        _dragged = null;

        await _audio.PlayEffectAsync("click.wav");
        await LoadAsync();
    }

    // Loeb andmebaasist ainult kulud (sissetulekuid siin ei näidata)
    public async Task LoadAsync()
    {
        List<MoneyEntry> all = await _db.GetAllAsync();
        var expenses = new List<MoneyEntry>();

        foreach (MoneyEntry entry in all)
        {
            if (!entry.IsIncome)
            {
                entry.CategoryText = _loc["Cat_" + entry.Category];
                expenses.Add(entry);
            }
        }

        Expenses = new ObservableCollection<MoneyEntry>(expenses);
    }
}
