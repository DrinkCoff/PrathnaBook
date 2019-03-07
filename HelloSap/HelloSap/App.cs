using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;
//using Microsoft.Office.Interop.Word;

namespace HelloSap
{
	public class App : Application
	{
		public App ()
		{
            //MainPage = new HelloSap.MainPage();

            MainPage = new NavigationPage(new NewListViewPage(false, ""));

            // new ListViewPage();

            //         new ContentPage {
            //             Content = new StackLayout {
            //                 VerticalOptions = LayoutOptions.Center,
            //                 Children =
            //                 {       new Label {
            //				HorizontalTextAlignment = TextAlignment.Center,
            //				Text = "Welcome to Xamarin Forms!"
            //			}
            //		}
            //	}
            //};
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
