using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Com.Lilarcor.Cheeseknife;
using System;
using Xamarin_Android_LangChanger.Helpers;

namespace Xamarin_Android_LangChanger
{
    [Activity(Label = "Xamarin_Android_LangChanger", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {

        [InjectView(Resource.Id.titleTextView)] TextView mTitleTextView;
        [InjectView(Resource.Id.descTextView)] TextView mDescTextView;
        [InjectView(Resource.Id.aboutTextView)] TextView mAboutTextView;
        [InjectView(Resource.Id.toTRButton)] Button mToTRButton;
        [InjectView(Resource.Id.toENButton)] Button mToENButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            // Use Cheeseknife to inject all attributed view
            // fields and events. For an activity injection,
            // simply pass in the reference to this activity.
            Cheeseknife.Inject(this);

            SetTitle(Resource.String.main_activity_toolbar_title);

        }


        [InjectOnClick(Resource.Id.toTRButton)]
        public void onChangeToTRClicked(object sender, EventArgs e)
        {
            UpdateViews("pl");
        }

        [InjectOnClick(Resource.Id.toENButton)]
        public void onChangeToENClicked(object sender, EventArgs e)
        {
            UpdateViews("en");
        }

        private void UpdateViews(string languageCode)
        {
            Context context = LanguageHelper.SetLanguage(this, languageCode);
            //var resources = context.Resources;

            //mTitleTextView.SetText(Resource.String.main_activity_title);
            //mDescTextView.SetText(Resource.String.main_activity_desc);
            //mAboutTextView.SetText((Resource.String.main_activity_about));
            //mToTRButton.SetText((Resource.String.main_activity_to_tr_button));
            //mToENButton.SetText((Resource.String.main_activity_to_en_button));

            //SetTitle((Resource.String.main_activity_toolbar_title));

            Relaunch(this);
        }

        public void Relaunch(Activity activity)
        {
            Intent intent = new Intent(activity, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTask);
            intent.AddFlags(ActivityFlags.NewTask);
            activity.StartActivity(intent);
            activity.Finish();
        }
    }
}

