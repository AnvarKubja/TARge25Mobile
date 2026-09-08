namespace TARge25Maui;

public partial class TimerPage : ContentPage
{
    public TimerPage()
    {
        InitializeComponent();
    }

    bool on_off = true;

    private async void ShowTime()
    {
        while (on_off)
        {
            timer_btn.Text = DateTime.Now.ToString("T");
            await Task.Delay(1000);
        }
    }

    private void timer_btn_Clicked(object sender, EventArgs e)
    {
        if (on_off)
        {
            on_off = false;
        }
        else
        {
            on_off = true;
            ShowTime();
        }
    }
}
