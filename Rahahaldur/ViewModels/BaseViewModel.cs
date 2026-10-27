using System.ComponentModel;

namespace RahaHaldur.ViewModels;

// Kõigi ViewModelite ühine alus.
// Kui mõni väärtus muutub, kutsume OnPropertyChanged ja ekraan uuendab end.
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
