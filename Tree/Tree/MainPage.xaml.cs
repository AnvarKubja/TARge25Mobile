namespace Tree;

public partial class MainPage : ContentPage
{
    // Puu praegune suurus (1.0 = algsuurus)
    private double suurus = 1.0;

    public MainPage()
    {
        InitializeComponent();

        TreeRoot.AnchorX = 0.5;
        TreeRoot.AnchorY = 1.0;

        // Virtuaalne aeg algab tänasest
        VirtualDatePicker.Date = DateTime.Today;
        VirtualTimePicker.Time = DateTime.Now.TimeOfDay;

        UuendaMaastikku();
    }

    private async void OnActionClicked(object sender, EventArgs e)
    {
        // Maastik uueneb vastavalt valitud kuupäevale ja kellaajale
        UuendaMaastikku();

        string tegevus = ActionPicker.SelectedItem as string ?? "";
        uint kestus = (uint)SpeedStepper.Value;

        if (tegevus == "Kasva")
        {
            InfoLabel.Text = "Puu kasvab!";

            suurus = suurus + 0.1;
            await TreeRoot.ScaleTo(suurus, kestus);
        }
        else if (tegevus == "Õitse")
        {
            InfoLabel.Text = "Puu õitseb!";

            // Lehestik muutub roosaks ja õied tulevad nähtavale
            MuudaLehtedeVarv(Color.FromArgb("#E27DA8"));

            Flowers.Opacity = 0;
            Flowers.IsVisible = true;
            await Flowers.FadeTo(1, kestus);
        }
        else if (tegevus == "Värise")
        {
            InfoLabel.Text = "Puu väriseb tuules!";

            await TreeRoot.TranslateTo(-20, 0, kestus / 4);
            await TreeRoot.TranslateTo(20, 0, kestus / 4);
            await TreeRoot.TranslateTo(-10, 0, kestus / 4);
            await TreeRoot.TranslateTo(0, 0, kestus / 4);

            InfoLabel.Text = "Tuul vaibus.";
        }
        else if (tegevus == "Langeta")
        {
            await LangetaPuu(kestus);
        }
        else
        {
            InfoLabel.Text = "Vali kõigepealt tegevus!";
        }
    }

    // Puu langetamine
    private async Task LangetaPuu(uint kestus)
    {
        int kuu = VirtualDatePicker.Date?.Month ?? 1;
        TimeSpan kellaaeg = VirtualTimePicker.Time ?? TimeSpan.Zero;

        bool onTalv = kuu == 12 || kuu == 1 || kuu == 2;
        bool onValge = kellaaeg.Hours >= 8 && kellaaeg.Hours < 17;

        if (!onTalv || !onValge)
        {
            InfoLabel.Text = "Pimedas ja suvel puid ei langetata!";
            return;
        }

        InfoLabel.Text = "Puu langetatakse!";

        // Puu nihkub servale ja kukub siis külili
        await TreeRoot.TranslateTo(-80, 0, kestus / 2);
        await TreeRoot.RotateTo(90, kestus);

        InfoLabel.Text = "Puu on langetatud.";
    }


    private async void OnResetClicked(object sender, EventArgs e)
    {
        await TreeRoot.RotateTo(0, 500);
        await TreeRoot.ScaleTo(1, 500);
        await TreeRoot.TranslateTo(0, 0, 500);

        suurus = 1.0;

        Flowers.IsVisible = false;
        OpacitySlider.Value = 1;

        UuendaMaastikku();
        InfoLabel.Text = "Puu on algasendis.";
    }

    // Slider muudab lehestiku läbipaistvust
    private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        if (CrownMain == null)
            return;

        CrownMain.Opacity = e.NewValue;
        CrownLeft.Opacity = e.NewValue;
        CrownRight.Opacity = e.NewValue;

        OpacityValueLabel.Text = "Lehestiku läbipaistvus: " + (int)(e.NewValue * 100) + "%";
    }

    // Stepper muudab animatsiooni kestust
    private void OnSpeedChanged(object sender, ValueChangedEventArgs e)
    {
        if (SpeedValueLabel == null)
            return;

        SpeedValueLabel.Text = "Animatsiooni kestus: " + (int)e.NewValue + " ms";
    }


    // Muudab taeva, maapinna ja lehtede värvi kuupäeva ja kellaaja järgi
    private void UuendaMaastikku()
    {
        int kuu = VirtualDatePicker.Date?.Month ?? 1;
        TimeSpan kellaaeg = VirtualTimePicker.Time ?? TimeSpan.Zero;

        // Päev või öö
        bool onPaev = kellaaeg.Hours >= 6 && kellaaeg.Hours < 20;

        if (onPaev)
            Sky.Color = Color.FromArgb("#7FC4EF");
        else
            Sky.Color = Color.FromArgb("#1B2A4A");

        Sun.IsVisible = onPaev;

        // Aastaaeg
        if (kuu == 12 || kuu == 1 || kuu == 2)
        {
            SeasonLabel.Text = "Talv";
            Ground.Color = Color.FromArgb("#E4EDF3");
            MuudaLehtedeVarv(Color.FromArgb("#8A6A4B"));
        }
        else if (kuu >= 3 && kuu <= 5)
        {
            SeasonLabel.Text = "Kevad";
            Ground.Color = Color.FromArgb("#63B04F");
            MuudaLehtedeVarv(Color.FromArgb("#7FCB63"));
        }
        else if (kuu >= 6 && kuu <= 8)
        {
            SeasonLabel.Text = "Suvi";
            Ground.Color = Color.FromArgb("#4E9A41");
            MuudaLehtedeVarv(Color.FromArgb("#3AA54B"));
        }
        else
        {
            SeasonLabel.Text = "Sügis";
            Ground.Color = Color.FromArgb("#9A8443");
            MuudaLehtedeVarv(Color.FromArgb("#D4802A"));
        }
    }

    // Annab kõigile kolmele lehekerale sama värvi
    private void MuudaLehtedeVarv(Color varv)
    {
        CrownMain.BackgroundColor = varv;
        CrownLeft.BackgroundColor = varv;
        CrownRight.BackgroundColor = varv;
    }
}
