using RahaHaldur.ViewModels;

namespace RahaHaldur.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    // Iga kord, kui leht nähtavale tuleb, laeme andmed uuesti
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}
