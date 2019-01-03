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
            string dbName = "stotraTest.db3";
            string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
            // Check if your DB has already been extracted.
            //if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Android.App.Application.Context.Assets.Open(dbName)))
                {
                    using (
                        BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
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
