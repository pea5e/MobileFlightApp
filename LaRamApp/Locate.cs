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
using System.Threading.Tasks;
using static Android.Content.ClipData;
using static AndroidX.RecyclerView.Widget.RecyclerView;


namespace LaRamApp
{
    [Activity(Label = "Depart", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Locate : AppCompatActivity
    {

        private RecyclerView recyclerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Locate);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            getAirports();
        }

        public async void getAirports()
        {
            using (var client = new HttpClient())
            {
                string uri = "http://10.0.2.2:5207";
                var result = await client.GetStringAsync(uri + "/api/Airports");

                List<Airport> airports = JsonConvert.DeserializeObject<List<Airport>>(result);
                AirportAdapter adapter = new AirportAdapter(airports);
                Log.Info("airports", airports.Count.ToString());
                recyclerView.SetAdapter(adapter);

                LinearLayoutManager layoutManager = new LinearLayoutManager(this);

                recyclerView.SetLayoutManager(layoutManager);
            }
        }

    }
}