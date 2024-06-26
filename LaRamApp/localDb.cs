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
using SQLite;
using System.IO;
using Android.Util;

namespace LaRamApp
{
    public class localDb
    {
        SQLiteConnection db;

        public localDb()
        {
            this.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),"sessionDb.db"));
            
            if(db.GetTableInfo("Sessions").Count==0)
            {
                db.CreateTable<SessionDb>();
            }
        }

        public void LogSession(string sessionId)
        {
            SessionDb s = new SessionDb();
            s.sessionId = sessionId;
            db.Insert(s);
        }
        public SessionDb GetSession()
        {
            var sess = from s in db.Table<SessionDb>()
                       select s;
            return sess.LastOrDefault();
        }
        public SessionDb GetSession(string sessionId)
        {
            var sess = from s in db.Table<SessionDb>()
                        where s.sessionId.Equals(sessionId)
                        select s;
            return sess.FirstOrDefault();
        }
        public void delSession()
        {
            db.Delete<SessionDb>(GetSession().Id);
        }
    }

    [Table("Sessions")]
    public class SessionDb
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string sessionId { get; set; }
    }
}