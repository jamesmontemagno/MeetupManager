using System.Collections.Generic;
using Cirrious.CrossCore;
using Refractored.MvxPlugins.Settings;

namespace MeetupManager.Portable.Helpers
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

		private const string AccessTokenKey = "access_token";
		private static string AccessTokenDefault = string.Empty;

		private const string RefreshTokenKey = "refresh_token";
		private static string RefreshTokenDefault = string.Empty;

		private const string KeyValidUntilKey = "key_valid";
		private const long KeyValidUntilDefault = 0;

        private const string UserIdKey = "user_id";
        private static string UserIdDefault = string.Empty;

        private const string UserNameKey = "user_name";
        private static string UserNameDefault = string.Empty;

#endregion

 
		public static string AccessToken
        {
            get
            {
				return AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault);
            }
            set
            {
                //if value has changed then save it!
				if (AppSettings.AddOrUpdateValue(AccessTokenKey, value))
                    AppSettings.Save();
            }
        }

		public static string RefreshToken
		{
			get
			{
				return AppSettings.GetValueOrDefault(RefreshTokenKey, RefreshTokenDefault);
			}
			set
			{
				//if value has changed then save it!
				if (AppSettings.AddOrUpdateValue(RefreshTokenKey, value))
					AppSettings.Save();
			}
		}

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault);
            }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(UserIdKey, value))
                    AppSettings.Save();
            }
        }


        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            }
            set
            {
                //if value has changed then save it!
                if (AppSettings.AddOrUpdateValue(UserNameKey, value))
                    AppSettings.Save();
            }
        }

		public static long KeyValidUntil
		{
			get
			{
				return AppSettings.GetValueOrDefault(KeyValidUntilKey, KeyValidUntilDefault);
			}
			set
			{
				//if value has changed then save it!
				if (AppSettings.AddOrUpdateValue(KeyValidUntilKey, value))
					AppSettings.Save();
			}
		}

    }
}
