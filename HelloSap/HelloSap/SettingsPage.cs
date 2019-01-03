using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HelloSap
{
    public class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            string langugageSettings = Helpers.Settings.LanguageSettings;

            uint fontSizeSettings = Helpers.Settings.FontSizeSettings;

            Picker languagePicker = new Picker();

            languagePicker.Items.Add("संस्कृत (Sanskrit)");
            languagePicker.Items.Add("ગુજરાતી (Gujarati)");
            languagePicker.Items.Add("English");

            languagePicker.Title = "Langugage";

            languagePicker.SelectedIndex = 0;
            languagePicker.SelectedIndexChanged += LanguagePicker_SelectedIndexChanged;
                // += this.LanguagePickerSelectedIndexChanged;



            Picker fontSizePicker = new Picker();

            fontSizePicker.Items.Add("14");
            fontSizePicker.Items.Add("16");
            fontSizePicker.Items.Add("20");

            fontSizePicker.Title = "Font Size";
            fontSizePicker.SelectedIndex = 0;
            fontSizePicker.SelectedIndexChanged += FontSizePicker_SelectedIndexChanged;


            Content = new StackLayout
            {
                Children = {
                    languagePicker,
                    fontSizePicker
                }
            };
        }

        private void FontSizePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            //Method call every time when picker selection changed.
            var selectedValue = picker.SelectedItem.ToString();

            Helpers.Settings.FontSizeSettings = Convert.ToUInt32(selectedValue);
        }

        private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            //Method call every time when picker selection changed.
            var selectedValue = picker.SelectedItem.ToString();

            Helpers.Settings.LanguageSettings = selectedValue;
        }
    }
}