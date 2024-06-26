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
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;
using Android.Util;
using System.Text;
using LaRamApp.Models;
using System.Threading.Tasks;


namespace LaRamApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class Login : AppCompatActivity
    {

        EditText email;
        EditText password;
        Button login;
        TextView switchTo;
        localDb Db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);
            Db = new localDb();
            SessionDb session = Db.GetSession();
            if ( session != null && session.sessionId != null && session.sessionId != "")
            {
                isLogged(session.sessionId);
            }
            email = FindViewById<EditText>(Resource.Id.email);
            password = FindViewById<EditText>(Resource.Id.password);
            login = FindViewById<Button>(Resource.Id.login);
            switchTo = FindViewById<TextView>(Resource.Id.SwitchSignUp);

            switchTo.Click += delegate
            {
                StartActivity(new Intent(this, typeof(Register)));
            };

            login.Click += delegate
            {
                String mail = email.Text;
                String pass = password.Text;
                if (mail == null || mail == "")
                    Toast.MakeText(this, "Mail Obligaroire", ToastLength.Short).Show();
                else if (pass == null || pass == "")
                    Toast.MakeText(this, "Mot de passe Obligaroire", ToastLength.Short).Show();
                else {
                    Logged(mail, pass);
                    
                }
            };

        }

        public async void Logged(string email, string password)
        {
            using (var client = new HttpClient())
            {
                string uri = "http://10.0.2.2:5207";
                var data = new LoginDTO();
                data.Email = email;
                data.Password = password;

                var json = JsonConvert.SerializeObject(data);
                var d = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(uri+"/api/Pilots/login", d);
                string resultContent = await result.Content.ReadAsStringAsync();

                //handling the answer  
                var session = JsonConvert.DeserializeObject<Session>(resultContent);
                if(session.SessionId!=null)
                {
                    Db.LogSession(session.SessionId);
                    StartActivity(new Intent(this, typeof(MainActivity)));
                    return;
                    
                }
                Toast.MakeText(this, "Bad Credentials", ToastLength.Short).Show();

            }
        }

        public async void isLogged(string sessionId)
        {
            using (var client = new HttpClient())
            {
                string uri = "http://10.0.2.2:5207";
                //var result = await client.GetStringAsync(uri + "/api/Airports");
                var data = new SessionDTO();
                data.SessionId = sessionId;

                var json = JsonConvert.SerializeObject(data);
                var d = new StringContent(json, Encoding.UTF8, "application/json");
                //Toast.MakeText(this, result, ToastLength.Short).Show();
                /*{
                    new KeyValuePair<string, string>("Email", email),
                    new KeyValuePair<string, string>("Password", password)
                });*/
                var result = await client.PostAsync(uri + "/api/Sessions", d);
                string resultContent = await result.Content.ReadAsStringAsync();

                //handling the answer  
                var pilot = JsonConvert.DeserializeObject<Pilot>(resultContent);
                
                if(pilot.Email != null)
                    StartActivity(new Intent(this, typeof(MainActivity)));
                //Log.Info("Session", session.SessionId);

            }
        }


    }

    public class LoginDTO
    {
        public String Email { get; set; }
        public String Password { get; set; }
    }

    public class SessionDTO
    {
        public string SessionId { get; set; }
    }

}