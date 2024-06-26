using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaRamApp.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Ref { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrive { get; set; }
        public string Plane { get; set; }
        public Session SessionPilot { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        
        public float TotalHeures { get; set; }
    }
}