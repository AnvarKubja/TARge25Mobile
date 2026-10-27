using RahaHaldur.Views;

namespace RahaHaldur;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Leht, mis ei ole alumises menüüs, vaid avatakse peale (lisa/muuda)
        Routing.RegisterRoute("EditPage", typeof(EditPage));
    }
}
