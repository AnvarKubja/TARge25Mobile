using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

namespace TARge25Maui
{
    public partial class RgbPage : ContentPage
    {

        Label titleLabel;
        BoxView redBox, greenBox, blueBox, resultBox;
        Slider redSlider, greenSlider, blueSlider;
        Label redValueLabel, greenValueLabel, blueValueLabel;
        Button randomColorButton;
        AbsoluteLayout al;

 
        private bool isAnimating = false;

        public RgbPage()
        {

            titleLabel = new Label
            {
                Text = "RGB mudel",
                FontSize = 28,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Colors.Black
            };

       
            redBox = new BoxView { Color = Colors.Red, CornerRadius = 15 };
            greenBox = new BoxView { Color = Colors.Green, CornerRadius = 15 };
            blueBox = new BoxView { Color = Colors.Blue, CornerRadius = 15 };

   
            redSlider = new Slider { Minimum = 0, Maximum = 255, Value = 0, MinimumTrackColor = Colors.Red };
            greenSlider = new Slider { Minimum = 0, Maximum = 255, Value = 0, MinimumTrackColor = Colors.Green };
            blueSlider = new Slider { Minimum = 0, Maximum = 255, Value = 0, MinimumTrackColor = Colors.Blue };


            redSlider.ValueChanged += OnSliderValueChanged;
            greenSlider.ValueChanged += OnSliderValueChanged;
            blueSlider.ValueChanged += OnSliderValueChanged;


            redValueLabel = new Label { Text = "R: 0", FontSize = 16, HorizontalTextAlignment = TextAlignment.Center };
            greenValueLabel = new Label { Text = "G: 0", FontSize = 16, HorizontalTextAlignment = TextAlignment.Center };
            blueValueLabel = new Label { Text = "B: 0", FontSize = 16, HorizontalTextAlignment = TextAlignment.Center };


            resultBox = new BoxView
            {
                Color = Colors.Black,
                CornerRadius = 25
            };


            randomColorButton = new Button
            {
                Text = "Juhuslik värv",
                FontSize = 16,
                BackgroundColor = Color.FromRgb(81, 43, 212),
                TextColor = Colors.White,
                CornerRadius = 10
            };
            randomColorButton.Clicked += OnRandomColorClickedAsync;

            al = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutBounds(titleLabel, new Rect(0.5, 0.05, 300, 40));
            AbsoluteLayout.SetLayoutFlags(titleLabel, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(titleLabel);

            AbsoluteLayout.SetLayoutBounds(redBox, new Rect(0.15, 0.15, 80, 80));
            AbsoluteLayout.SetLayoutFlags(redBox, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(redBox);

            AbsoluteLayout.SetLayoutBounds(greenBox, new Rect(0.5, 0.15, 80, 80));
            AbsoluteLayout.SetLayoutFlags(greenBox, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(greenBox);

            AbsoluteLayout.SetLayoutBounds(blueBox, new Rect(0.85, 0.15, 80, 80));
            AbsoluteLayout.SetLayoutFlags(blueBox, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(blueBox);

            AbsoluteLayout.SetLayoutBounds(redValueLabel, new Rect(0.5, 0.27, 100, 20));
            AbsoluteLayout.SetLayoutFlags(redValueLabel, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(redValueLabel);

            AbsoluteLayout.SetLayoutBounds(redSlider, new Rect(0.5, 0.31, 320, 40));
            AbsoluteLayout.SetLayoutFlags(redSlider, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(redSlider);

            AbsoluteLayout.SetLayoutBounds(greenValueLabel, new Rect(0.5, 0.37, 100, 20));
            AbsoluteLayout.SetLayoutFlags(greenValueLabel, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(greenValueLabel);

            AbsoluteLayout.SetLayoutBounds(greenSlider, new Rect(0.5, 0.41, 320, 40));
            AbsoluteLayout.SetLayoutFlags(greenSlider, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(greenSlider);

            AbsoluteLayout.SetLayoutBounds(blueValueLabel, new Rect(0.5, 0.47, 100, 20));
            AbsoluteLayout.SetLayoutFlags(blueValueLabel, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(blueValueLabel);

            AbsoluteLayout.SetLayoutBounds(blueSlider, new Rect(0.5, 0.51, 320, 40));
            AbsoluteLayout.SetLayoutFlags(blueSlider, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(blueSlider);

            AbsoluteLayout.SetLayoutBounds(resultBox, new Rect(0.5, 0.78, 320, 200));
            AbsoluteLayout.SetLayoutFlags(resultBox, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(resultBox);

            AbsoluteLayout.SetLayoutBounds(randomColorButton, new Rect(0.5, 0.97, 200, 50));
            AbsoluteLayout.SetLayoutFlags(randomColorButton, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(randomColorButton);

            Content = al;

            UuendaVarvi();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!isAnimating)
            {
                UuendaVarvi();
            }
        }


        private void UuendaVarvi()
        {
            int r = Convert.ToInt32(redSlider.Value);
            int g = Convert.ToInt32(greenSlider.Value);
            int b = Convert.ToInt32(blueSlider.Value);

            redValueLabel.Text = $"R: {r.ToString()}";
            greenValueLabel.Text = $"G: {g.ToString()}";
            blueValueLabel.Text = $"B: {b.ToString()}";

            redBox.Color = Color.FromRgb(r, 0, 0);   
            greenBox.Color = Color.FromRgb(0, g, 0);   
            blueBox.Color = Color.FromRgb(0, 0, b);


            Color uusVarv = Color.FromRgb(r, g, b);
            resultBox.Color = uusVarv;
            titleLabel.TextColor = uusVarv;
        }


        private async void OnRandomColorClickedAsync(object sender, EventArgs e)
        {
            if (isAnimating) return;
            isAnimating = true;

            Random rand = new Random();
            double targetR = rand.Next(0, 256);
            double targetG = rand.Next(0, 256);
            double targetB = rand.Next(0, 256);


            int sammud = 20;
            double sammR = (targetR - redSlider.Value) / sammud;
            double sammG = (targetG - greenSlider.Value) / sammud;
            double sammB = (targetB - blueSlider.Value) / sammud;

            for (int i = 0; i < sammud; i++)
            {
                redSlider.Value += sammR;
                greenSlider.Value += sammG;
                blueSlider.Value += sammB;

                UuendaVarvi();

                await Task.Delay(15);
            }

            redSlider.Value = targetR;
            greenSlider.Value = targetG;
            blueSlider.Value = targetB;
            UuendaVarvi();

            isAnimating = false;
        }
    }
}
