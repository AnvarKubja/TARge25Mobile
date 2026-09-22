namespace TARge25Maui;

public partial class StartPage : ContentPage
{
    VerticalStackLayout vst;
    ScrollView sv;

    public List<ContentPage> Lehed = new List<ContentPage>()
    {
        new TextPage(),
        new FigurePage(),
        new TimerPage(),
        new DateTimePage(),
        new StepperSliderPage(),
        new RgbPage(),
        new Pop_Up_Page(),
        new GridPage()
    };

    public List<string> Lehenimed = new List<string>()
    {
        "Tekst",
        "Kujundus",
        "Taimer",
        "Kuupäev",
        "Slaider",
        "RGB",
        "PopUp",
        "Grid"
    };

    public StartPage()
    {
        vst = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 20
        };

        for (int i = 0; i < Lehed.Count; i++)
        {
            int index = i;

            Button nupp = new Button
            {
                Text = Lehenimed[index],
                FontSize = 36,
                FontFamily = "Socafe",
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.Black,
                CornerRadius = 10,
                HeightRequest = 60
            };

            vst.Add(nupp);

            nupp.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(Lehed[index]);
            };
        }

        // Loome punase testnupu
        Button nulliNupp = new Button
        {
            Text = "Nulli seaded (Testimiseks)",
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            Margin = new Thickness(0, 30, 0, 0) // Jätame veidi tühja ruumi üles
        };

        // Mis juhtub nupule vajutades?
        nulliNupp.Clicked += async (sender, e) =>
        {
            // Kustutame seadne mälust meie spetsiifilise võtme
            Preferences.Default.Remove("EsimeneKäivitamine");

            // Anname tagasisidet, et nullimine õnnestus
            await DisplayAlertAsync("Edukalt nullitud", "Mälu on tühjendatud. Kui sa lehe uuesti avad, käitub äpp nagu täiesti uus!", "OK");
        };

        // Ärge unustage nuppu oma Layouti (nt vst või stackLayout) lisada!
        vst.Add(nulliNupp);
        sv = new ScrollView { Content = vst };
        Content = sv;

        sv = new ScrollView
        {
            Content = vst
        };

        Content = sv;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        bool onEsimeneStart = Preferences.Default.Get("EsimeneKäivitamine", true);

        if (onEsimeneStart)
        {
            bool vastus = await DisplayAlertAsync("Tere tulemast!",
                "Tundub, et avasid selle rakenduse esimest korda. Kas soovid näha lühikest juhendit?",
                "Jah, palun",
                "Ei, saan ise hakkama");
            
            if (vastus)
            {
                await DisplayAlertAsync("Juhend",
                    "Siin on lühike juhend: vali menüüst sovib teema ja uuri, kuidas elemendid töötavad!",
                    "Selge");
            }

            Preferences.Default.Set("EsimeneKäivitamine", false);
        }
    }
}