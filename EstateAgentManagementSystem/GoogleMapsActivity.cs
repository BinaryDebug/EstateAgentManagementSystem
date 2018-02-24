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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GoogleMapsView);
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

        }

        public void OnMapReady(GoogleMap googleMap)
        {
            MarkerOptions markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(16.03, 108));
            markerOptions.SetTitle("My Position");
            googleMap.AddMarker(markerOptions);

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
        }

    }
}