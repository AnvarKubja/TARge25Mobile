using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

namespace TARge25Maui
{
    public partial class StepperSliderPage : ContentPage
    {
        Label label;
        Stepper stepper;
        Slider slider;
        AbsoluteLayout al;

        public StepperSliderPage()
        {
            label = new Label
            {
                Text = "Liiguta liugurit või stepperit",
                BackgroundColor = Colors.LightGray,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            stepper = new Stepper
            {
                Minimum = 0,
                Maximum = 360,
                Increment = 5,
                Value = 50,
                HorizontalOptions = LayoutOptions.Center
            };

            stepper.ValueChanged += Stepper_Slider_ValueChanged;

            slider = new Slider
            {
                Minimum = 0,
                Maximum = 360,
                Value = 50,
                HorizontalOptions = LayoutOptions.Center,
                MinimumTrackColor = Colors.LightGray,
                MaximumTrackColor = Colors.DarkGray,
                ThumbColor = Colors.Gray,
                WidthRequest = 300
            };

            slider.ValueChanged += Stepper_Slider_ValueChanged;

            List<View> controls = new List<View> { label, stepper, slider };

            for (int i = 0; i < controls.Count; i++)
            {
                double yKoht = 0.2 + i * 0.2;

                AbsoluteLayout.SetLayoutBounds(
                    controls[i],
                    new Rect(0.5, yKoht, 300, 60)
                );

                AbsoluteLayout.SetLayoutFlags(
                    controls[i],
                    AbsoluteLayoutFlags.PositionProportional
                );
            }

            al = new AbsoluteLayout();
            foreach (var control in controls)
            {
                al.Children.Add(control);
            }

            Content = al;

            UuendaVaadet(50);
        }

        private void Stepper_Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
        {
            if (sender == stepper)
            {
                slider.Value = e.NewValue;
            }
            else if (sender == slider)
            {
                stepper.Value = e.NewValue;
            }

            UuendaVaadet(e.NewValue);
        }

        private void UuendaVaadet(double vaartus)
        {
            label.Text = $"Väärtus: {vaartus:F2}";

            label.FontSize = 16 + (vaartus / 15);

            int varviKomp = (int)Math.Max(0, 255 - (vaartus * 0.7));
            BackgroundColor = Color.FromRgb(varviKomp, varviKomp, varviKomp);


            label.TextColor = varviKomp < 128 ? Colors.White : Colors.Black;

            label.Rotation = vaartus;
        }
    }
}
