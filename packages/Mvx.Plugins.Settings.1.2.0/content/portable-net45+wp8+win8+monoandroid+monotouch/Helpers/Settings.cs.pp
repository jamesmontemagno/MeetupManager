// Helpers/Settings.cs
using Cirrious.CrossCore;
using Refractored.MvxPlugins.Settings;

namespace $rootnamespace$.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Simply setup your settings once when it is initialized.
        /// </summary>
        private static ISettings m_Settings;
        private static ISettings AppSettings
        {
            get
            {
                return m_Settings ?? (m_Settings = Mvx.GetSingleton<ISettings>());
            }
        }

#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static string SettingsDefault = string.Empty;

#endregion

 
	public static string GeneralSettings
        {
            get
            {
		return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                //if value has changed then save it!
		if (AppSettings.AddOrUpdateValue(SettingsKey, value))
                    AppSettings.Save();
            }
        }

    }
}