using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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

namespace LaRamApp
{
    [Activity(Label = "Planning des vols", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Planning : AppCompatActivity
    {

        private RecyclerView recyclerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Destination);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            SessionDTO sessionDTO = new SessionDTO();
            sessionDTO.SessionId = new localDb().GetSession().sessionId;

            getVols(sessionDTO);

        }

        public async void getVols(SessionDTO sessionDTO)
        {
            using (var client = new HttpClient())
            {
                string uri = "http://10.0.2.2:5207";

                var json = JsonConvert.SerializeObject(sessionDTO);
                var d = new StringContent(json, Encoding.UTF8, "application/json");
                
                var result = await client.PostAsync(uri + "/api/Flights", d);
                string resultContent = await result.Content.ReadAsStringAsync();
                List<Flight> flights;
                try
                {
                     flights = JsonConvert.DeserializeObject<List<Flight>>(resultContent);
                }
                catch (Exception ex) {
                    flights = new List<Flight>
                    {
                        JsonConvert.DeserializeObject<Flight>(resultContent)
                    };
                }
                FlightAdapter adapter = new FlightAdapter(flights);
                recyclerView.SetAdapter(adapter);

                LinearLayoutManager layoutManager = new LinearLayoutManager(this);

                recyclerView.SetLayoutManager(layoutManager);
            }
        }
    }
}