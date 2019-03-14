using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Support.V4.Content;
using System.IO;
using HelloSap;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Util;
using Xamarin.Forms;

namespace HelloSap.Droid
{
    [Activity(Label = "Stotram", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly int REQUEST_STORAGE = 0;
        static readonly string TAG = "MainActivity";

        Android.Views.View layout;

        protected override void OnCreate(Bundle bundle)
        {
            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
            {
             
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            this.StoragePermission();

            LoadApplication(new HelloSap.App());
        }

        private void StoragePermission()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted)
            {
                // We have permission, go ahead and use the camera.
                this.CopyDatabasesFromAsset();
            }
            else
            {
                // Storage permission is not granted. If necessary display rationale & request.
                this.StoragePermissionRationale();
            }
        }

        private void CopyDatabasesFromAsset()
        {
            //string databaseFileName = Helpers.Settings.DatabaseName;
            string[] databseFileNames = Resources.GetStringArray(Resource.Array.databaseName);

            foreach (string databaseFileName in databseFileNames)
            {
                Helper.CopyDatabaseFile(databaseFileName);
            }
        }

        private void StoragePermissionRationale()
        {
            //SetContentView(Resource.Layout.activity_main);
            this.layout = new Android.Views.View(this.BaseContext);

            var activity = (Activity)Forms.Context;
            var view = activity.FindViewById(Android.Resource.Id.Content);

            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage))
            {
                // Provide an additional rationale to the user if the permission was not granted
                // and the user would benefit from additional context for the use of the permission.
                // For example if the user has previously denied the permission.
                Log.Info(TAG, "Displaying camera permission rationale to provide additional context.");

                var requiredPermissions = new String[] { Manifest.Permission.WriteExternalStorage };
                Snackbar.Make(view,
                               "Database file need to be copied on the storage.",
                               Snackbar.LengthIndefinite)
                        .SetAction("OK",
                                   new Action<Android.Views.View>(delegate (Android.Views.View obj) {
                                       ActivityCompat.RequestPermissions(this, requiredPermissions, REQUEST_STORAGE);
                                   }
                        )
                ).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage }, REQUEST_STORAGE);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            var activity = (Activity)Forms.Context;
            var view = activity.FindViewById(Android.Resource.Id.Content);

            if (requestCode == REQUEST_STORAGE)
            {
                // Received permission result for camera permission.
                Log.Info(TAG, "Received response for Storage permission request.");

                // Check if the only required permission has been granted
                if (grantResults.Length == 1 && grantResults[0] == Permission.Granted)
                {
                    // Camera permission has been granted, preview can be displayed
                    Log.Info(TAG, "Storage permission has now been granted. Showing preview.");
                    Snackbar.Make(view, "Storage permission granted.", Snackbar.LengthShort).Show();
                    CopyDatabasesFromAsset();
                }
                else
                {
                    Log.Info(TAG, "Storage permission was NOT granted.");
                    Snackbar.Make(layout, "Storage permission was NOT granted.", Snackbar.LengthShort).Show();
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }
    }
}

