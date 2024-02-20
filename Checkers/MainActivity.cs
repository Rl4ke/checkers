using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Xamarin.Forms;

namespace Checkers
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(GameManager());
        }
    }
}