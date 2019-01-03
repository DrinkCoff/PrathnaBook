using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace HelloSap
{
    class MainPage : MasterDetailPage
    {
        NewListViewPage masterPage;

        public MainPage()
        {
            masterPage = new NewListViewPage(false, "");
            Master = masterPage;
            Detail = new NavigationPage(new StotraPage(@"Work In Progress" + System.Environment.NewLine + "Coming Soon..."));

            masterPage.ListView.ItemSelected += OnItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as StotraInternal;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType, item.Name));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
