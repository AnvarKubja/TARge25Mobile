namespace TARge25Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;
            // Kui loendur on 5 või rohkem, muuda nupu värvi
            if (count >= 5)
            {
                CounterBtn.BackgroundColor = Colors.Red;
                CounterBtn.TextColor = Colors.White;
            }

            // Loogika 1: Muuda teksti vastavalt arvule
            if (count == 1)
                CounterBtn.Text = $"Vajutatud {count} kord";
            else
                CounterBtn.Text = $"Vajutatud {count} korda";

            // Loogika 2: Pööra pilti iga vajutusega 15 kraadi
            BotImage.Rotation += 15;

            // Loogika 3: Muuda Labeli teksti
            CounterLabel.Text = $"Nuppu on vajutatud kokku: {count}";

            if (count >= 10)
            {
                BotImage.IsVisible = false; // Peidab pildi
                CounterLabel.Text = "Pilt kadus ära! Vajuta Reset.";
            }

            // Genereerime juhusliku värvi (R, G, B)
            var random = new Random();
            var randomColor = Color.FromRgb(
                random.Next(0, 256), // Red
                random.Next(0, 256), // Green
                random.Next(0, 256)  // Blue
            );
            ResetBtn.BackgroundColor = randomColor;

            BotImage.Opacity -= 0.1;
            BotImage.Scale += 0.1;

            if (count % 2 == 0)
            {
                CounterBtn.CornerRadius += 5;
            }

            SemanticScreenReader.Announce(CounterBtn.Text);
        }


        // UUS MEETOD: Reset nupu jaoks
        private void OnResetClicked(object? sender, EventArgs e)
        {
            count = 0;
            CounterBtn.Text = "Vajuta mind";
            CounterLabel.Text = "Alustame uuesti!";
            BotImage.Rotation = 0; // Pilt läheb otseks tagasi
            BotImage.IsVisible = true; // Toob pildi tagasi
            CounterBtn.BackgroundColor = Colors.Blue;
            BotImage.Opacity = 1;
            BotImage.Scale = 1;
            CounterBtn.CornerRadius = 10;


            if (BotImage.HorizontalOptions == LayoutOptions.Start)
            {
                BotImage.HorizontalOptions = LayoutOptions.End;
            }
            else
            {
                BotImage.HorizontalOptions = LayoutOptions.Start;
            }
        }


    }
}
