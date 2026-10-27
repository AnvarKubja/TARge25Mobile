using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RahaHaldur.ViewModels;

// Kõigi ViewModelite ühine alus. Teavitab vaadet, kui mõni väärtus muutub.
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Muudab välja väärtust ja teavitab. Tagastab true, kui väärtus tõesti muutus.
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(name);
        return true;
    }
}
