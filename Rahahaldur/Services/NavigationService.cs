namespace RahaHaldur.Services;

// Lehtede vahetamine ja dialoogid. Nii ei pea ViewModel ise UI-d puutuma.
public class NavigationService
{
    public Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null)
    {
        if (parameters == null)
            return Shell.Current.GoToAsync(route);

        return Shell.Current.GoToAsync(route, parameters);
    }

    public Task GoBackAsync()
    {
        return Shell.Current.GoToAsync("..");
    }

    public Task<bool> ConfirmAsync(string title, string message, string yes, string no)
    {
        return Shell.Current.DisplayAlert(title, message, yes, no);
    }

    public Task AlertAsync(string title, string message, string ok)
    {
        return Shell.Current.DisplayAlert(title, message, ok);
    }
}
