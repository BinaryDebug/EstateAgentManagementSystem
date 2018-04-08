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
using Newtonsoft.Json;

namespace EstateAgentManagementSystem
{
    public class AgentScheduleFragment : ListFragment //Agent Schedule view = list of agents schedule, clickable items to view more info
    {
        private static int EDITOR_ACTIVITY_REQUEST = 1001;
        private static int MENU_DELETE_ID = 1002;
        private int currentScheduleId;
        private string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "EstateAgentDB.db3");
        private SQLiteConnection db;


        private List<Schedule> scheduleList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);


            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.AgentScheduleView, null);

            db = new SQLiteConnection(dbPath);

            db.CreateTable<Schedule>();

            Button btnAddToSchedule = view.FindViewById<Button>(Resource.Id.btnAddToSchedule);
            btnAddToSchedule.Click += btnAddToScheduleClick;
            var table = db.Table<Schedule>();

            scheduleList = table.ToList();

            return view;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Schedule scheduleItem = scheduleList[position];
            Intent intent = new Intent(this.Activity, typeof(AddToScheduleActivity));
            intent.PutExtra("Schedule", JsonConvert.SerializeObject(scheduleItem));
            StartActivityForResult(intent, EDITOR_ACTIVITY_REQUEST);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            refreshList();
        }
        private void refreshList()
        {
            RegisterForContextMenu(ListView);
            var table = db.Table<Schedule>();
            scheduleList = table.ToList();
            ArrayAdapter adapter = new ArrayAdapter(this.Context, Android.Resource.Layout.SimpleListItem1, scheduleList);
            ListAdapter = adapter;
        }

        private void btnAddToScheduleClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(AddToScheduleActivity));
            StartActivityForResult(intent, EDITOR_ACTIVITY_REQUEST);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)menuInfo;
            currentScheduleId = (int)info.Id;
            menu.Add(0, MENU_DELETE_ID, 0, Resource.String.delete);
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == MENU_DELETE_ID)
            {
                Schedule schedule = scheduleList[currentScheduleId];
                db.Delete<Schedule>(schedule.Id);
                refreshList();
            }

            return base.OnContextItemSelected(item);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            refreshList();
        }
    }
}