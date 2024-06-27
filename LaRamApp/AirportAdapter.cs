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

namespace LaRamApp
{
    public class AirportAdapter : RecyclerView.Adapter
    {
        private List<Airport> items;

        public AirportAdapter(List<Airport> items)
        {
            this.items = items;
        }

        // A view holder class that holds a reference to a text view
        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView code;
            public TextView city;
            public TextView country;
            public LinearLayout line;

            public MyViewHolder(View itemView) : base(itemView)
            {
                code = itemView.FindViewById<TextView>(Resource.Id.code);
                city = itemView.FindViewById<TextView>(Resource.Id.city);
                country = itemView.FindViewById<TextView>(Resource.Id.country);
                line = itemView.FindViewById<LinearLayout>(Resource.Id.airport);
            }
        }

        // A method that creates a view holder and inflates a layout for each item
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Airport, parent, false);
            MyViewHolder viewHolder = new MyViewHolder(itemView);
            return viewHolder;
        }

        // A method that binds the data to the view holder
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myHolder = holder as MyViewHolder;
            Airport item = items[position];
            myHolder.code.Text = item.Code;
            myHolder.city.Text = item.City;
            myHolder.country.Text = item.Country;
            myHolder.line.Click += delegate
            {
                Intent i = new Intent(Application.Context, typeof(Destination));
                Bundle b = new Bundle();
                b.PutString("code", item.Code);
                i.PutExtras(b);
                i.AddFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(i);
            };
        }
        public override int ItemCount
        {
            get { return items.Count; }
        }

    }
}