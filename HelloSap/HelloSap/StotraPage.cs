using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using SQLite;
using System.IO;

namespace HelloSap
{
    class StotraPage : ContentPage 
    {
       public  StotraPage(string stotraName)
        {
            List<Stotra> values = null;

            if (File.Exists(Helpers.Settings.GetDatabasePath()))
            {
                var db = new SQLiteConnection(Helpers.Settings.GetDatabasePath());

                values = db.Query<Stotra>(@"select * from Stotras where name = '" + stotraName + @"'");
            }

            var scroll = new ScrollView();

            var stack = new StackLayout();

            Label label = new Label();

            if (values != null)
            {
                if (values.Count == 1)
                {
                    label.Text = values[0].Content;
                }
                else
                {
                    label.Text = @"Error" + System.Environment.NewLine + "Not Available";
                }
            }
            else
            {
                label.Text = @"Error" + System.Environment.NewLine + "Not Available";
            }
            label.FontSize = Helpers.Settings.FontSizeSettings;
            
            label.HorizontalTextAlignment = TextAlignment.Center;

            stack.Children.Add(label);

            Title = stotraName;

            scroll.Content = stack;
            this.Content = scroll;
        }
    }
}
