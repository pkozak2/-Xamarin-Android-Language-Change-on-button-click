using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Xamarin_Android_LangChanger.Helpers;

namespace Xamarin_Android_LangChanger
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void AttachBaseContext(Context @base)
        {
            base.AttachBaseContext(LanguageHelper.OnAttach(@base));
        }
    }
}