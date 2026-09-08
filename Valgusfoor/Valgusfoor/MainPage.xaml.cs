namespace Valgusfoor;

public partial class MainPage : ContentPage
{
    bool lightOn = true;

    public MainPage()
    {
        InitializeComponent();

        lightOn = true;
        TurnOff();
    }

    // SISSE
    private void ClickedOn(object sender, EventArgs e)
    {
        lightOn = true;

        TurnOff();

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

        TurnOff();

        RedLight.Fill = Colors.Red;
        StatusLabel.Text = "Seisa";
    }

    // KOLLANE
    private void YellowLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        TurnOff();

        YellowLight.Fill = Colors.Yellow;
        StatusLabel.Text = "Valmista";
    }

    // ROHELINE
    private void GreenLight_Tapped(object sender, TappedEventArgs e)
    {
        if (!lightOn)
            return;

        TurnOff();

        GreenLight.Fill = Colors.Green;
        StatusLabel.Text = "Sõida";
    }


    private void TurnOff()
    {
        RedLight.Fill = Colors.Gray;
        YellowLight.Fill = Colors.Gray;
        GreenLight.Fill = Colors.Gray;
    }
}
