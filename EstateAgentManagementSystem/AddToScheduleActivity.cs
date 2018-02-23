using System;
using System.Collections.Generic;
using System.IO;
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

namespace EstateAgentManagementSystem
{
    [Activity(Label = "Editor", Theme = "@style/Theme.DesignDemo", Icon = "@drawable/ic_launcher")]
    class AddToScheduleActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddToScheduleView);


            Button btnSaveButton = FindViewById<Button>(Resource.Id.saveButton);
            btnSaveButton.Click += saveButton;

        }

        private void saveButton(object sender, EventArgs e)
        {
            EditText clientNameEditText = FindViewById<EditText>(Resource.Id.clientNameEditText);
            EditText phoneNumberEditText = FindViewById<EditText>(Resource.Id.phoneNumberEditText);
            EditText addressEditText = FindViewById<EditText>(Resource.Id.addressEditText);
            EditText dateEditText = FindViewById<EditText>(Resource.Id.dateEditText);
            EditText timeEditText = FindViewById<EditText>(Resource.Id.timeEditText);
            EditText propertyTypeEditText = FindViewById<EditText>(Resource.Id.propertyTypeEditText);


            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "EstateAgentDB.db3");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Schedule>();
            Schedule mySchedule = new Schedule(clientNameEditText.Text, phoneNumberEditText.Text, addressEditText.Text, dateEditText.Text, timeEditText.Text, propertyTypeEditText.Text);
            db.Insert(mySchedule);
        }

        private void saveAndFinish()
        {
            Finish();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        public override void OnBackPressed()
        {
            saveAndFinish();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                saveAndFinish();
            }
            return false;
        }
    }
}