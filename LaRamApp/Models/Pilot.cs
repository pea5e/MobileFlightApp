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
    public class Pilot
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public string Email { get; set; }

        
        public string Password { get; set; }

        public ICollection<Session> Sessions { get; } = new List<Session>();

        public ICollection<Checking> Checkings { get; } = new List<Checking>();



    }
}