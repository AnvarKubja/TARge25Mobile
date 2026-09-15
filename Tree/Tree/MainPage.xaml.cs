namespace Tree;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Kogu puu pöörleb alumise keskpunkti ümber
        Tree.AnchorX = 0.5;
        Tree.AnchorY = 1.0;
    }

    private async void OnActionClicked(object sender, EventArgs e)
    {
        var picker = this.FindByName<Picker>("ActionPicker");
        var info = this.FindByName<Label>("InfoLabel");
        var stepper = this.FindByName<Stepper>("SpeedStepper");

        if (picker.SelectedIndex == -1)
        {
            info.Text = "⚠️ Palun vali tegevus!";
            return;
        }

        string action = picker.SelectedItem?.ToString() ?? "";
        int speed = (int)stepper.Value;

        switch (action)
        {
            case "Kasva":
                await GrowTree(speed);
                break;

            case "Õitse":
                await BloomTree(speed);
                break;

            case "Värise":
                await ShakeTree(speed);
                break;

            case "Langeta":
                await CutDownTree(speed);
                break;
        }
    }

    private async Task GrowTree(int speed)
    {
        var info = this.FindByName<Label>("InfoLabel");

        info.Text = "🌱 Puu kasvab!";

        await Tree.ScaleTo(1.3, (uint)speed);

        info.Text = "🌳 Puu on suuremaks kasvanud!";
    }

    private async Task BloomTree(int speed)
    {
        var leaves = this.FindByName<Frame>("TreeLeaves");
        var flowers = this.FindByName<Label>("Flowers");
        var info = this.FindByName<Label>("InfoLabel");

        info.Text = "🌸 Puu õitseb!";

        leaves.BackgroundColor = Colors.HotPink;

        flowers.Opacity = 0;
        flowers.IsVisible = true;

        await flowers.FadeTo(1, (uint)speed);

        info.Text = "🌸 Puu õitseb!";
    }

    private async Task ShakeTree(int speed)
    {
        var info = this.FindByName<Label>("InfoLabel");

        info.Text = "💨 Puu väriseb tuules!";

        await Tree.TranslateTo(-20, 0, (uint)(speed / 4));
        await Tree.TranslateTo(20, 0, (uint)(speed / 2));
        await Tree.TranslateTo(-15, 0, (uint)(speed / 2));
        await Tree.TranslateTo(15, 0, (uint)(speed / 2));
        await Tree.TranslateTo(0, 0, (uint)(speed / 4));

        info.Text = "🍃 Tuul vaibus.";
    }

    private async Task CutDownTree(int speed)
    {
        var datePicker = this.FindByName<DatePicker>("VirtualDatePicker");
        var timePicker = this.FindByName<TimePicker>("VirtualTimePicker");
        var info = this.FindByName<Label>("InfoLabel");

        // Kontrollime kuud
        int month = datePicker.Date?.Month ?? DateTime.Now.Month;

        bool isWinter =
            month == 12 ||
            month == 1 ||
            month == 2;

        // Kontrollime kellaaega
        TimeSpan selectedTime =
            timePicker.Time ?? TimeSpan.Zero;

        bool isDaytime =
            selectedTime >= new TimeSpan(8, 0, 0) &&
            selectedTime <= new TimeSpan(17, 0, 0);

        if (!isWinter || !isDaytime)
        {
            info.Text =
                "❌ Puud ei tohi praegu langetada! " +
                "Langetada võib ainult talvel ja valgel ajal (08:00–17:00).";

            return;
        }

        info.Text = "🪓 Puu langetatakse!";

        // Kogu puu kukub korraga:
        // tüvi + lehed + lilled
        Tree.AnchorX = 0.5;
        Tree.AnchorY = 1.0;

        await Tree.RotateTo(90, (uint)speed);

        info.Text = "🪵 Puu on langetatud.";
    }

    private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        var leaves = this.FindByName<Frame>("TreeLeaves");

        if (leaves != null)
        {
            leaves.Opacity = e.NewValue;
        }
    }

    private async void OnResetClicked(object sender, EventArgs e)
    {
        var leaves = this.FindByName<Frame>("TreeLeaves");
        var flowers = this.FindByName<Label>("Flowers");
        var info = this.FindByName<Label>("InfoLabel");

        // Tagasi algasendisse
        await Task.WhenAll(
            Tree.RotateTo(0, 500),
            Tree.ScaleTo(1.0, 500),
            Tree.TranslateTo(0, 0, 500)
        );

        // Taastame lehtede algse välimuse
        leaves.BackgroundColor = Colors.ForestGreen;
        leaves.Opacity = 1;

        // Peidame õied
        flowers.Opacity = 0;
        flowers.IsVisible = false;

        info.Text = "🌳 Puu on algasendis.";
    }

}
