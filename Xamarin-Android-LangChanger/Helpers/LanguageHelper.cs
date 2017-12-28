using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Preferences;
using Java.Util;

namespace Xamarin_Android_LangChanger.Helpers
{
    public class LanguageHelper
    {
        private static string SELECTED_LANGUAGE = "Language.Helper.Selected.Language";

        // returns Context having application default locale for all activities
        public static Context OnAttach(Context context)
        {
            string lang = GetPersistedData(context, Locale.Default.Language);
            return SetLanguage(context, lang);
        }

        // sets application locale with default locale persisted in preference manager on each new launch of application and
        // returns Context having application default locale
        public static Context OnAttach(Context context, string defaultLanguage)
        {
            string lang = GetPersistedData(context, defaultLanguage);
            return SetLanguage(context, lang);
        }

        // returns language code persisted in preference manager
        public static string GetLanguage(Context context)
        {
            return GetPersistedData(context, Locale.Default.Language);
        }

        // persists new language code change in preference manager and updates application default locale
        // returns Context having application default locale
        public static Context SetLanguage(Context context, string language)
        {
            Persist(context, language);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                return UpdateResources(context, language);
            }

            return UpdateResourcesLegacy(context, language);
        }

        // returns language code persisted in preference manager
        public static string GetPersistedData(Context context, string defaultLanguage)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            return preferences.GetString(SELECTED_LANGUAGE, defaultLanguage);
        }

        // persists new language code in preference manager
        private static void Persist(Context context, string language)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = preferences.Edit();

            editor.PutString(SELECTED_LANGUAGE, language);
            editor.Apply();
        }

        // For android device versions above Nougat (7.0)
        // updates application default locale configurations and
        // returns new Context object for the current Context but whose resources are adjusted to match the given Configuration
        //@TargetApi(Build.VERSION_CODES.N)
        private static Context UpdateResources(Context context, string language)
        {

            Locale locale = new Locale(language);

            Configuration configuration = context.Resources.Configuration;

            LocaleList localeList = new LocaleList(locale);
            Android.OS.LocaleList.Default = localeList;
            configuration.Locales = localeList;

            return context.CreateConfigurationContext(configuration);
        }

        // For android device versions below Nougat (7.0)
        // updates application default locale configurations and
        // returns new Context object for the current Context but whose resources are adjusted to match the given Configuration
        private static Context UpdateResourcesLegacy(Context context, string language)
        {

            Java.Util.Locale locale = new Java.Util.Locale(language);

            Java.Util.Locale.Default = locale;

            Resources resources = context.Resources;

            Configuration configuration = resources.Configuration;
            configuration.Locale = locale;

#pragma warning disable CS0618 // 'Resources.UpdateConfiguration(Configuration, DisplayMetrics)' is obsolete: 'deprecated'
            resources.UpdateConfiguration(configuration, resources.DisplayMetrics);
#pragma warning restore CS0618 // 'Resources.UpdateConfiguration(Configuration, DisplayMetrics)' is obsolete: 'deprecated'

            return context;
        }
    }
}