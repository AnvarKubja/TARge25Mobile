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
    private void ClickedOn(object sender, EventArgs e)
    {
        lightOn = true;

        RedLight.Fill = Colors.Red;
        YellowLight.Fill = Colors.Yellow;
        GreenLight.Fill = Colors.Green;

        StatusLabel.Text = "Vali valgus";
    }

    // VÄLJA
    private void ClickedOff(object sender, EventArgs e)
    {
        lightOn = false;

        TurnOff();

        StatusLabel.Text = "Lülita esmalt foor sisse";
    }

    // PUNANE
    private void RedLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        StatusLabel.Text = "Seisa";
    }

    // KOLLANE
    private void YellowLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        StatusLabel.Text = "Valmista";
    }

    // ROHELINE
    private void GreenLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        StatusLabel.Text = "Sõida";
    }

    private void TurnOff()
    {
        RedLight.Fill = Colors.Gray;
        YellowLight.Fill = Colors.Gray;
        GreenLight.Fill = Colors.Gray;
    }
}