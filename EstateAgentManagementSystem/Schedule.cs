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

namespace EstateAgentManagementSystem
{
    class Schedule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string ClientNumber { get; set; }

        public string ClientAddress { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string PropertyInfo { get; set; }
        public Schedule()
        {
        }

        public Schedule(string ClientName, string ClientNumber, string ClientAddress, string Date, string Time, string PropertyInfo)
        {
            this.ClientName = ClientName;
            this.ClientNumber = ClientNumber;
            this.ClientAddress = ClientAddress;
            this.Date = Date;
            this.Time = Time;
            this.PropertyInfo = PropertyInfo;
        }

        public override string ToString()
        {
            return "Name: " + ClientName + "\nAddress: " + ClientAddress + "\nDate: " + Date + "\nTime:" + Time;
        }
    }
}