﻿using SQLite;
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

        public ListView ListView { get { return listView; } }

        ListView listView;
        SearchBar searchBar;
        List<StotraInternal> stotras;
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
                "",
                () =>
                {
                    Navigation.PushAsync(new SettingsPage());
                }
            );

            //this.toolbarItem.Icon = "android:id / search_mag_icon";
            this.ToolbarItems.Add(toolbarItemSearch);
            this.ToolbarItems.Add(settings);

            Label header = new Label
            {
                Text = "स्तोत्रम्",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };


            stotras = InitializeStotras();
            
            listView = new ListView
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

            listView.ItemSelected += OnItemSelected;

            Title = "मुख्य पृष्ठः";

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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as StotraInternal;
            listView.SelectedItem = false;
            if (item != null)
            {
                await Navigation.PushAsync(new StotraPage(item.Name));
            }
        }
    }
    
}
