using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Globalization;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace EstateAgentManagementSystem
{
    [Activity(Label = "Editor", Theme = "@style/NotesEditTheme", Icon = "@drawable/ic_launcher")]
    public class GoogleMapsActivity : Activity, IOnMapReadyCallback
    {
        private GoogleMap GMap;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GoogleMapsView);
            Button searchbutton = FindViewById<Button>(Resource.Id.searchButton);

            searchbutton.Click += Searchbutton_Click;

            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }

        }

        private void Searchbutton_Click(object sender, EventArgs e)
        {
            EditText searchCriteriaText = FindViewById<EditText>(Resource.Id.searchCriteriaText);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
//            MarkerOptions markerOptions = new MarkerOptions();
//            markerOptions.SetPosition(new LatLng(16.03, 108));
//            markerOptions.SetTitle("My Position");
//            googleMap.AddMarker(markerOptions);
//
//            googleMap.TrafficEnabled = true;
//            googleMap.MapType = GoogleMap.MapTypeHybrid;
//            CameraUpdate cam = CameraUpdateFactory.NewLatLngZoom(markerOptions.Position, 15);
//
//            googleMap.MoveCamera(cam);
//            googleMap.UiSettings.ZoomControlsEnabled = true;
//            googleMap.UiSettings.CompassEnabled = true;
//            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());

            this.GMap = googleMap;

            GMap.UiSettings.ZoomControlsEnabled = true;

            LatLng latLng = new LatLng(Convert.ToDouble(13.0291), Convert.ToDouble(80.2083));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latLng, 15);
            GMap.MoveCamera(camera);

            MarkerOptions options = new MarkerOptions()
                .SetPosition(latLng)
                .SetTitle("A title");

            GMap.AddMarker(options);
        }

    }
}