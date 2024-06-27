using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using LaRamApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using static Xamarin.Essentials.Platform;

namespace LaRamApp
{
    [Activity(Label = "Destination", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Destination : AppCompatActivity
    {

        private RecyclerView recyclerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Destination);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            getDestinations(Intent.Extras.GetString("code"));
           
        }

        public async void getDestinations(string code)
        {
            using (var client = new HttpClient())
            {
                string uri = "http://10.0.2.2:5207";
                var result = await client.GetStringAsync(uri + "/api/Checkings/destinations/"+code);

                List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(result);
                FlightAdapter adapter = new FlightAdapter(flights);
                recyclerView.SetAdapter(adapter);

                LinearLayoutManager layoutManager = new LinearLayoutManager(this);

                recyclerView.SetLayoutManager(layoutManager);
            }
        }

    }
}