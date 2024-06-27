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
using System.Diagnostics;
using Android.Content;

namespace LaRamApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {

        Button check, logout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            check = FindViewById<Button>(Resource.Id.checkin);
            logout = FindViewById<Button>(Resource.Id.logout);
            localDb Db;
            Db = new localDb();
            logout.Click += delegate
            {
                Db.delSession();
                StartActivity(new Intent(this, typeof(Login)));
            };
            check.Click += delegate
            {
                StartActivity(new Intent(this, typeof(Locate)));
            };
        }

	}
}
