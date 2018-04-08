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
using Java.Util.Concurrent.Atomic;
using Newtonsoft.Json;
using SQLite;
using Environment = System.Environment;

namespace EstateAgentManagementSystem
{
    [Activity(Label = "Editor", Theme = "@style/Theme.DesignDemo", Icon = "@drawable/ic_launcher")]
    class AddToScheduleActivity : Activity
    {
        private Schedule selectedSchedule;
        private EditText clientNameEditText;
        private EditText phoneNumberEditText;
        private EditText addressEditText;
        private EditText dateEditText;
        private EditText timeEditText;
        private EditText propertyTypeEditText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddToScheduleView);


            Button btnSaveButton = FindViewById<Button>(Resource.Id.saveButton);
            btnSaveButton.Click += saveButton;

            clientNameEditText = FindViewById<EditText>(Resource.Id.clientNameEditText);
            phoneNumberEditText = FindViewById<EditText>(Resource.Id.phoneNumberEditText);
            addressEditText = FindViewById<EditText>(Resource.Id.addressEditText);
            dateEditText = FindViewById<EditText>(Resource.Id.dateEditText);
            timeEditText = FindViewById<EditText>(Resource.Id.timeEditText);
            propertyTypeEditText = FindViewById<EditText>(Resource.Id.propertyTypeEditText);

            try
            {
                selectedSchedule = JsonConvert.DeserializeObject<Schedule>(Intent.GetStringExtra("Schedule"));
            }
            catch (Exception)
            {
                selectedSchedule = null;
            }

            if (selectedSchedule != null)
            {
                TextView lbl = FindViewById<TextView>(Resource.Id.textView1);
                lbl.Text = "Edit schedule";
                clientNameEditText.Text = selectedSchedule.ClientName;
                phoneNumberEditText.Text = selectedSchedule.ClientNumber;
                addressEditText.Text = selectedSchedule.ClientAddress;
                dateEditText.Text = selectedSchedule.Date;
                timeEditText.Text = selectedSchedule.Time;
                propertyTypeEditText.Text = selectedSchedule.PropertyInfo;
            }

        }

        private void saveButton(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "EstateAgentDB.db3");
            var db = new SQLiteConnection(dbPath);

            if (selectedSchedule == null) //User must have chosen to add new entry to schedule
            {
                db.CreateTable<Schedule>();
                Schedule mySchedule = new Schedule(clientNameEditText.Text, phoneNumberEditText.Text, addressEditText.Text, dateEditText.Text, timeEditText.Text, propertyTypeEditText.Text);
                db.Insert(mySchedule);
                saveAndFinish();
            }
            else if (selectedSchedule != null)
            {
                selectedSchedule.ClientAddress = addressEditText.Text;
                selectedSchedule.ClientName = clientNameEditText.Text;
                selectedSchedule.ClientNumber = phoneNumberEditText.Text;
                selectedSchedule.Date = dateEditText.Text;
                selectedSchedule.Time = timeEditText.Text;
                selectedSchedule.PropertyInfo = propertyTypeEditText.Text;

                db.Delete<Schedule>(selectedSchedule.Id);
                db.Insert(selectedSchedule);
                saveAndFinish();
            }





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