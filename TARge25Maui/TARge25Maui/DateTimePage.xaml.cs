using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace TARge25Maui // <-- Muuda see oma projekti nimeks, kui vaja
{
    public partial class DateTimePage : ContentPage
    {
        DatePicker datePicker;
        TimePicker timePicker;
        Label datetimeLabel;
        AbsoluteLayout al;

        public DateTimePage()
        {
            // 1. Luuakse kuupäeva valija (DatePicker)
            datePicker = new DatePicker
            {
                MinimumDate = DateTime.Now.AddDays(-15),
                MaximumDate = DateTime.Now.AddDays(15),
                Date = DateTime.Now,
                HorizontalOptions = LayoutOptions.Center,
                Format = "D"
            };

            datePicker.DateSelected += (sender, e) =>
            {
                UuendaSildiTekst();
            };

            // 2. Luuakse kellaaja valija (TimePicker)
            timePicker = new TimePicker
            {
                Time = DateTime.Now.TimeOfDay,
                HorizontalOptions = LayoutOptions.Center,
                Format = "T"
            };

            timePicker.PropertyChanged += (sender, e) =>
            {
                // Kontrollitakse, et reageeritaks just kellaaja muutumisele
                if (e.PropertyName == nameof(TimePicker.Time))
                {
                    UuendaSildiTekst();
                }
            };

            // 3. Luuakse teksti sildi element (Label)
            datetimeLabel = new Label
            {
                Text = "Vali kuupäev või aeg",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // 4. Paigutatakse elemendid loendisse asukohtade määramiseks
            List<View> controls = new List<View> { datePicker, timePicker, datetimeLabel };

            // MÄÄRAME REEGLID ENNE LAYOUTI LISAMIST: see hoiab ära tühja ekraani vea
            for (int i = 0; i < controls.Count; i++)
            {
                double yKoht = 0.2 + i * 0.2; // Proportsionaalne asukoht ekraanil (0.2, 0.4, 0.6)
                AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                AbsoluteLayout.SetLayoutFlags(controls[i], AbsoluteLayoutFlags.PositionProportional);
            }

            // 5. Luuakse AbsoluteLayout ja lisatakse juba seadistatud elemendid
            al = new AbsoluteLayout();
            foreach (var control in controls)
            {
                al.Children.Add(control);
            }

            // Määratakse vaade lehe sisuks
            Content = al;

            // Uuendatakse teksti sildi algväärtust kohe lehe laadimisel
            UuendaSildiTekst();
        }

        // Abimeetod, mis kuvab sildil korraga nii valitud kuupäeva kui kellaaega
        private void UuendaSildiTekst()
        {
            if (datetimeLabel != null && datePicker != null && timePicker != null)
            {
                datetimeLabel.Text = $"Valitud kuupäev: {datePicker.Date:d}\nValitud kellaaeg: {timePicker.Time:hh\\:mm\\:ss}";
            }
        }
    }
}
