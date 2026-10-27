using RahaHaldur.ViewModels;

namespace RahaHaldur.Views;

public partial class EditPage : ContentPage
{
    public EditPage(EditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
