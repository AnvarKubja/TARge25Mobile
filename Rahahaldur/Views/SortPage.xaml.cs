using RahaHaldur.ViewModels;

namespace RahaHaldur.Views;

public partial class SortPage : ContentPage
{
    private readonly SortViewModel _vm;

    public SortPage(SortViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}
