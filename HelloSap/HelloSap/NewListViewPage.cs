using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HelloSap
{
    class NewListViewPage : ContentPage
    {
        ToolbarItem toolbarItemSearch;
        ToolbarItem settings;

        public NewListViewPage(bool searchMode, string searchTerm)
        {
            toolbarItemSearch = new ToolbarItem
            (
                "Search",
                "search.png",
                () =>
                {
                    Navigation.PushAsync(new SearchPage());
                }
            );

            settings = new ToolbarItem
            (
                "Settings",
                "settings.png",
                () =>
                {
                    Navigation.PushAsync(new SettingsPage());
                }
            );

            //this.toolbarItem.Icon = "android:id / search_mag_icon";
            this.ToolbarItems.Add(toolbarItemSearch);
            this.ToolbarItems.Add(settings);

            Label header = this.PopulateHeader();

            ListView listView = this.PopulateList(InitializeStotras());
            listView.ItemSelected += OnItemSelected;

            Title = "मुख्य पृष्ठः";

            this.ContentUpdate(header, listView);
        }

        public void ContentUpdate(Label header, ListView listView)
        {
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    //searchBar,
                    listView
                }
            };
        }



        public Label PopulateHeader()
        {
            return new Label
            {
                Text = "स्तोत्रम्",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };
        }


        public ListView PopulateList(List<StotraInternal> stotras)
        {
            return new ListView
            {
                ItemsSource = stotras,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    //BoxView boxView = new BoxView();
                    //boxView.Color =  Color.Orange;

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Center,
                            Children =
                            {
                                //boxView,
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
        }

        public List<StotraInternal> InitializeStotras()
        {
            List<StotraInternal> stotras = new List<StotraInternal>();
            string databaseFileName = Helpers.Settings.DatabaseName;
            string databaseFilePath = Helpers.Settings.GetDatabasePath();

            if (!File.Exists(databaseFilePath))
            {
                using (BinaryReader br = new BinaryReader(Android.App.Application.Context.Assets.Open(databaseFileName)))
                {
                    using (
                        BinaryWriter bw = new BinaryWriter(new FileStream(databaseFilePath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }

            if (File.Exists(path: databaseFilePath))
            {
                var db = new SQLiteConnection(databaseFilePath);
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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as StotraInternal;
            //listView.SelectedItem = false;
            if (item != null)
            {
                await Navigation.PushAsync(new StotraPage(item.Name));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;

            Label header = this.PopulateHeader();

            ListView listView = this.PopulateList(InitializeStotras());
            listView.ItemSelected += OnItemSelected;

            Title = "मुख्य पृष्ठः";

            this.ContentUpdate(header, listView);
        }
    }
    
}
