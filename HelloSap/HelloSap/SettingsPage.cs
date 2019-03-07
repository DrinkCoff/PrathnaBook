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

            languagePicker.SelectedIndex = languagePicker.Items.IndexOf(langugageSettings);
            languagePicker.SelectedIndexChanged += LanguagePicker_SelectedIndexChanged;
                // += this.LanguagePickerSelectedIndexChanged;



            Picker fontSizePicker = new Picker();

            fontSizePicker.Items.Add("14");
            fontSizePicker.Items.Add("16");
            fontSizePicker.Items.Add("20");

            fontSizePicker.Title = "Font Size";
            fontSizePicker.SelectedIndex = fontSizePicker.Items.IndexOf(fontSizeSettings.ToString());
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

            string databaseName = "stotra-Sanskrit.db3";
            switch (selectedValue)
            {
                case "संस्कृत (Sanskrit)":
                    databaseName = "stotra-Sanskrit.db3";
                    break;
                case "ગુજરાતી (Gujarati)":
                    databaseName = "stotra-Gujarati.db3";
                    break;
                case "English":
                    databaseName = "stotra-English.db3";
                    break;
                default:
                    break;
            }

            Helpers.Settings.DatabaseName = databaseName;
            Helpers.Settings.LanguageSettings = selectedValue;
        }

        protected override bool OnBackButtonPressed()
        {
            //Some logics here

            Navigation.PushAsync(new NavigationPage(new NewListViewPage(false, "")));

            return base.OnBackButtonPressed();
        }
    }
}