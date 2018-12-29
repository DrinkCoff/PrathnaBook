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
            string dbName = "stotraTest.db3";
            string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

            List<Stotra> values = null;

            if (File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);

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
            label.FontSize = 16.0;
            
            label.HorizontalTextAlignment = TextAlignment.Center;

            stack.Children.Add(label);

            scroll.Content = stack;
            this.Content = scroll;
        }
    }
}
