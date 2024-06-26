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
    public class Airport
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}