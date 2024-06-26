using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Widget;
using Microsoft.Win32;
using Android.Content;

namespace LaRamApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class Register : AppCompatActivity
    {
        EditText name;
        EditText email;
        EditText password;
        EditText confirmPassword;
        Button register;
        TextView switchTo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.RegisterLayout);
            email = FindViewById<EditText>(Resource.Id.emailRegister);
            password = FindViewById<EditText>(Resource.Id.passwordRegister);
            confirmPassword = FindViewById<EditText>(Resource.Id.confirmPassword);
            register = FindViewById<Button>(Resource.Id.Register);
            switchTo = FindViewById<TextView>(Resource.Id.SwitchLogIn);

            switchTo.Click += delegate
            {
                StartActivity(new Intent(this,typeof(Login)));
            };


        }


    }
}