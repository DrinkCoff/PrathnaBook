using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HelloSap
{
    public class SearchPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        ListView listView;
        SearchBar searchBar;
        private List<StotraInternal> stotras;
        private ICommand _searchCommand;

        public SearchPage()
        {
            stotras = InitializeStotras();
            searchBar = new SearchBar
            {
                Placeholder = "Enter search term",
            };

            searchBar.Focus();

            searchBar.SearchButtonPressed += SearchBar_SearchButtonPressed;
            searchBar.SearchCommand = SearchCommand;
            searchBar.TextChanged += SearchBar_TextChanged;

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

            this.Content = new StackLayout
            {
                Children =
                {
                    searchBar,
                    listView
                }
            };
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                listView.ItemsSource = stotras;
            }

            else
            {
                listView.ItemsSource = stotras.Where(x => x.Name.StartsWith(e.NewTextValue));
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>((text) =>
                {
                    // The text parameter can now be used for searching.
                    
                    if (string.IsNullOrEmpty(text))
                    {
                        listView.ItemsSource = stotras;
                    }

                    else
                    {
                        listView.ItemsSource = stotras.Where(x => x.Name.StartsWith(text));
                    }

                }));
            }
        }

        async private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private List<StotraInternal> InitializeStotras()
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