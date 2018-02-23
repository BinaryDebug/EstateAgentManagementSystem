using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Environment = System.Environment;
using System.IO;
using Android.Support.V7.Widget;

namespace EstateAgentManagementSystem
{
    class AgentScheduleFragment : Fragment //Agent Schedule view = list of agents schedule, clickable items to view more info
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "dbtest3.db3");

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.AgentScheduleView, null);


            Button btnAddToSchedule = view.FindViewById<Button>(Resource.Id.btnAddToSchedule);
            btnAddToSchedule.Click += btnAddToScheduleClick;


            //Button button = view.FindViewById<Button>(Resource.Id.myButton);

            //button.Click += delegate
            //{
            //    var db = new SQLiteConnection(dbPath);

            //    db.CreateTable<Schedule>();

            //    Schedule mySchedule = new Schedule("Testing", "101938893");

            //    db.Insert(mySchedule);
            //};

            //Button getButton = view.FindViewById<Button>(Resource.Id.myGetButton);

            //getButton.Click += delegate
            //{
            //    TextView displayText = view.FindViewById<TextView>(Resource.Id.txtGettedDataYo);
            //    var db = new SQLiteConnection(dbPath);


            //    var table = db.Table<Schedule>();

            //    foreach (var item in table)
            //    {
            //        Schedule mySchedule = new Schedule(item.Name, item.PhoneNumber);
            //        db.Delete(mySchedule);
            //        displayText.Text += mySchedule + "\n";
            //    }
            //};





            return view;
        }

        private void btnAddToScheduleClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(AddToScheduleActivity));
            StartActivityForResult(intent, 1001);
        }
    }
}