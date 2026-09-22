namespace TickTackToe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Pakime MainPage NavigationPage sisse, et lehtede vahetus töötaks
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
