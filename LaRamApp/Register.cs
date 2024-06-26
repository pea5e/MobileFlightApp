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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using LaRamApp.Models;
using static Java.Util.Jar.Attributes;

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
            name = FindViewById<EditText>(Resource.Id.name);
            email = FindViewById<EditText>(Resource.Id.emailRegister);
            password = FindViewById<EditText>(Resource.Id.passwordRegister);
            confirmPassword = FindViewById<EditText>(Resource.Id.confirmPassword);
            register = FindViewById<Button>(Resource.Id.Register);
            switchTo = FindViewById<TextView>(Resource.Id.SwitchLogIn);

            switchTo.Click += delegate
            {
                StartActivity(new Intent(this,typeof(Login)));
            };

            register.Click += delegate
            {
                String mail = email.Text;
                String pass = password.Text;
                String conf = confirmPassword.Text;
                String nom = name.Text;
                if (nom == null || nom == "")
                    Toast.MakeText(this, "nom Obligaroire", ToastLength.Short).Show();
                else if (mail == null || mail == "")
                    Toast.MakeText(this, "Mail Obligaroire", ToastLength.Short).Show();
                else if (pass == null || pass == "")
                    Toast.MakeText(this, "Mot de passe Obligaroire", ToastLength.Short).Show();
                else if (pass != conf)
                    Toast.MakeText(this, "Mot de passes pas coherents", ToastLength.Short).Show();
                else
                {
                    PilotDTO p = new PilotDTO();
                    p.email = mail;
                    p.name = nom;
                    p.id = 0;
                    p.password = pass;
                    Logged(p);

                }
            };

        }

        public async void Logged(PilotDTO p)
        {
            using (var client = new HttpClient())
            {
                string uri = "http://10.0.2.2:5207";

                var json = JsonConvert.SerializeObject(p);
                var d = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(uri + "/api/Pilots/register", d);
                string resultContent = await result.Content.ReadAsStringAsync();

                //handling the answer  
                var pilot = JsonConvert.DeserializeObject<Pilot>(resultContent);
                if (pilot.Email != null)
                {
                    StartActivity(new Intent(this, typeof(Login)));
                    return;
                }
                Toast.MakeText(this, "Email utilise", ToastLength.Short).Show();

            }
        }

    }

    public class PilotDTO
    {
        public int id {get; set;}
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

}