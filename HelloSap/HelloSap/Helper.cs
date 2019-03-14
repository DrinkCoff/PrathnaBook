using Android;
using Android.Content.PM;
using Android.Support.V4.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HelloSap
{
    public static class Helper
    {
        public static void CopyDatabaseFile(string databaseFileName)
        {
            string databaseFilePath = Helpers.Settings.GetDatabasePath(databaseFileName);

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
        }

        public static bool CheckPermission(Android.Content.Context context)
        {
            bool permission = (ContextCompat.CheckSelfPermission(context, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted);
            return permission;

        }
    }
}
