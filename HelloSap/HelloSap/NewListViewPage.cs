using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace HelloSap
{
    class NewListViewPage : ContentPage
    {

        public ListView ListView { get { return listView; } }

        ListView listView;

        public NewListViewPage()
        {
            Label header = new Label
            {
                Text = "Stotra",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            List<StotraInternal> stotras = InitializeStotras();
            
            listView = new ListView
            {
                ItemsSource = stotras,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    BoxView boxView = new BoxView();
                    boxView.Color =  Color.Orange;

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                boxView,
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.Center,
                                    Spacing = 0,
                                    Children =
                                    {
                                        nameLabel
                                    }
                                }
                            }
                        }
                    };
                })
                    
            };

            Title = "Stotra Source";

            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };
        }

        public List<StotraInternal> InitializeStotras()
        {
            List<StotraInternal> stotras = new List<StotraInternal>();

            string dbName = "stotraTest.db3";
            string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

            if (File.Exists(path: dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Stotra>();

                var values = db.Query<Stotra>(@"select * from Stotras");

                if (values.Count > 0)
                {
                    foreach (Stotra stotraInDb in values)
                    {
                        stotras.Add(new StotraInternal(stotraInDb.Name, typeof(StotraPage)));
                    }
                }
            }

            return stotras;
        }
    }
    
}
