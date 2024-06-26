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
    public class Session
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public Pilot Pilot { get; set; }
        public ICollection<Flight> Flights { get; set; }

        public Checking Checkings { get; set; }
    }
}