using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using LaRamApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Xamarin.Essentials;
using LaRamApp;
using static Android.Provider.ContactsContract.CommonDataKinds;
using System.Net.Http;
using static Android.Content.ClipData;

namespace LaRamApp
{
    public class FlightAdapter : RecyclerView.Adapter
    {
        private List<Flight> items;

        public FlightAdapter(List<Flight> items)
        {
            this.items = items;
        }

        // A view holder class that holds a reference to a text view
        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView refer;
            public TextView plane;
            public TextView destination;
            public TextView depart;
            public TextView arrive;
            public LinearLayout line;

            public MyViewHolder(View itemView) : base(itemView)
            {
                refer = itemView.FindViewById<TextView>(Resource.Id.refer);
                plane = itemView.FindViewById<TextView>(Resource.Id.plane);
                depart = itemView.FindViewById<TextView>(Resource.Id.depart);
                arrive = itemView.FindViewById<TextView>(Resource.Id.arrive);
                destination = itemView.FindViewById<TextView>(Resource.Id.destination);
                line = itemView.FindViewById<LinearLayout>(Resource.Id.line);
            }
        }

        // A method that creates a view holder and inflates a layout for each item
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Flight, parent, false);
            MyViewHolder viewHolder = new MyViewHolder(itemView);
            return viewHolder;
        }

        // A method that binds the data to the view holder
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myHolder = holder as MyViewHolder;
            Flight item = items[position];
            myHolder.refer.Text = item.Ref;
            myHolder.plane.Text = item.Plane;
            myHolder.depart.Text = item.Depart.ToShortTimeString();
            myHolder.arrive.Text = item.Arrive.ToShortTimeString();
            myHolder.destination.Text = item.To.City+" "+ item.To.Country;
            myHolder.line.Click += delegate
            {
                reserver(item.Plane);
            };
        }
        public override int ItemCount
        {
            get { return items.Count; }
        }

        public async void reserver(string Plane)
        {
            using (var client = new HttpClient())
            {
                string uri = "http://10.0.2.2:5207";

                var data = new ReserveDTO();
                data.code = Plane;
                data.sessionId = new localDb().GetSession().sessionId;

                var json = JsonConvert.SerializeObject(data);
                var d = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(uri + "/api/Checkings/Schedule", d);
                Intent i = new Intent(Application.Context, typeof(Planning));
                i.AddFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(i);

            }
        }

    }
    public class ReserveDTO
    {
        public string sessionId { get; set; }
        public string code { get; set; }
    }
}