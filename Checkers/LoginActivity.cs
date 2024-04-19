using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using System;


namespace Checkers
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);

            auth = FirebaseAuth.Instance;

            EditText editTextUsername = FindViewById<EditText>(Resource.Id.editTextUsername);
            EditText editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            EditText editTextNickname = FindViewById<EditText>(Resource.Id.editTextNickname);

            Button buttonLogin = FindViewById<Button>(Resource.Id.buttonLogin);
            buttonLogin.Click += async (sender, e) =>
            {
                string email = editTextUsername.Text;
                string password = editTextPassword.Text;
                string nickname = editTextNickname.Text;

                try
                {
                    var result = await auth.SignInWithEmailAndPasswordAsync(email, password);

                    FirebaseUser user = result.User;

                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();
                }
                catch (FirebaseAuthInvalidUserException)
                {
                    Toast.MakeText(this, "Invalid email or password", ToastLength.Short).Show();
                }
                catch (FirebaseAuthInvalidCredentialsException)
                {
                    Toast.MakeText(this, "Invalid email or password", ToastLength.Short).Show();
                }
                catch (FirebaseNetworkException)
                {
                    Toast.MakeText(this, "Network error", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Authentication failed: " + ex.Message, ToastLength.Short).Show();
                }
            };

            // Check if user is already signed in
            if (auth.CurrentUser != null)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            }
        }
    }
}