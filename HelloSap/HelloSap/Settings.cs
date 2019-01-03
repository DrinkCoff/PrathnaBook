// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
{
    private static ISettings AppSettings
    {
        get
        {
            return CrossSettings.Current;
        }
    }

    #region Setting Constants

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;

    private const string FontSizeKey = "fontSizeKey";
    private static readonly uint FontSizeDefault = 16;

        private const string LanguageKey = "languageKey";
        private static readonly string LanguageDefault = "संस्कृत (Sanskrit)";

        #endregion


        public static string GeneralSettings
    {
        get
        {
            return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(SettingsKey, value);
        }
    }

        public static uint FontSizeSettings
        {
            get
            {
                return Convert.ToUInt32(AppSettings.GetValueOrDefault(FontSizeKey, FontSizeDefault));
            }
            set
            {
                AppSettings.AddOrUpdateValue(FontSizeKey, value);
            }
        }

        public static string LanguageSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(LanguageKey, LanguageDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LanguageKey, value);
            }
        }

    }
}