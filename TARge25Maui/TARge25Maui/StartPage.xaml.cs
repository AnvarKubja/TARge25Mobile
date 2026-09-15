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
        new TreePage()
    };

    public List<string> Lehenimed = new List<string>()
    {
        "Tekst",
        "Kujundus",
        "Taimer",
        "Kuupäev",
        "Slaider",
        "RGB",
        "Puu"
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

        sv = new ScrollView
        {
            Content = vst
        };

        Content = sv;
    }
}