namespace RahaHaldur.Services;

// Lehtede vahetamine ja dialoogid. Nii ei pea ViewModel ise UI-d puutuma.
public class NavigationService
{
    public async Task GoToAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    // Küsib jah/ei küsimuse. Tagastab true, kui kasutaja vajutas "jah".
    public async Task<bool> ConfirmAsync(string title, string message, string yes, string no)
    {
        return await Shell.Current.DisplayAlert(title, message, yes, no);
    }

    public async Task AlertAsync(string title, string message, string ok)
    {
        await Shell.Current.DisplayAlert(title, message, ok);
    }
}
