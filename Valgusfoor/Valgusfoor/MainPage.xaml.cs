namespace Valgusfoor;

public partial class MainPage : ContentPage
{
    bool lightOn = false;

    public MainPage()
    {
        InitializeComponent();

        lightOn = false;
        TurnOff();
        StatusLabel.Text = "Lülita esmalt foor sisse";
    }

    // SISSE
    private async void ClickedOn(object sender, EventArgs e)
    {
        lightOn = true;

        RedLight.Fill = Colors.Red;
        YellowLight.Fill = Colors.Yellow;
        GreenLight.Fill = Colors.Green;

        StatusLabel.Text = "Vali valgus";

        await Task.WhenAll(
            RedLight.ScaleToAsync(1.1, 150),
            YellowLight.ScaleToAsync(1.1, 150),
            GreenLight.ScaleToAsync(1.1, 150)
        );

        await Task.WhenAll(
            RedLight.ScaleToAsync(1.0, 150),
            YellowLight.ScaleToAsync(1.0, 150),
            GreenLight.ScaleToAsync(1.0, 150)
        );
    }

    // VÄLJA
    private async void ClickedOff(object sender, EventArgs e)
    {
        lightOn = false;

        TurnOff();

        StatusLabel.Text = "Lülita esmalt foor sisse";

        await Task.WhenAll(
            RedLight.FadeToAsync(0.5, 150),
            YellowLight.FadeToAsync(0.5, 150),
            GreenLight.FadeToAsync(0.5, 150)
        );

        await Task.WhenAll(
            RedLight.FadeToAsync(1.0, 150),
            YellowLight.FadeToAsync(1.0, 150),
            GreenLight.FadeToAsync(1.0, 150)
        );
    }

    // PUNANE
    private async void RedLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        StatusLabel.Text = "Seisa";

        await AnimateLight(RedLight);
    }

    // KOLLANE
    private async void YellowLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        StatusLabel.Text = "Valmista";

        await AnimateLight(YellowLight);
    }

    // ROHELINE
    private async void GreenLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        StatusLabel.Text = "Sõida";

        await AnimateLight(GreenLight);
    }

    // Tule animatsioon
    private async Task AnimateLight(VisualElement light)
    {
        await Task.WhenAll(
            light.ScaleToAsync(1.2, 150),
            light.FadeToAsync(0.5, 150)
        );

        await Task.WhenAll(
            light.ScaleToAsync(1.0, 150),
            light.FadeToAsync(1.0, 150)
        );
    }

    private void TurnOff()
    {
        RedLight.Fill = Colors.Gray;
        YellowLight.Fill = Colors.Gray;
        GreenLight.Fill = Colors.Gray;

        RedLight.Scale = 1.0;
        YellowLight.Scale = 1.0;
        GreenLight.Scale = 1.0;

        RedLight.Opacity = 1.0;
        YellowLight.Opacity = 1.0;
        GreenLight.Opacity = 1.0;
    }
}
