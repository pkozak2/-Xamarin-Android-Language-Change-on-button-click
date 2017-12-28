using Android.App;
using Android.Content;
using Xamarin_Android_LangChanger.Helpers;

namespace Xamarin_Android_LangChanger
{
    [Activity(Label = "App")]
    public class App : Application
    {
        public override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void AttachBaseContext(Context @base)
        {
            base.AttachBaseContext(LanguageHelper.OnAttach(@base, LanguageHelper.GetLanguage(@base)));
        }
    }
}