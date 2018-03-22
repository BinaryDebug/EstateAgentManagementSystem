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
using Android.Locations;
using Android.Views.InputMethods;

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

            searchbutton.Click += Searchbutton_Click; //Search Google Maps with chosen criteria

            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }

        }

        private void Searchbutton_Click(object sender, EventArgs e)
        {
            EditText searchCriteriaText = FindViewById<EditText>(Resource.Id.searchCriteriaText);
            Geocoder searchGeocoder;
            Address searchAddress;
            LatLng searchLocation;
            if (searchCriteriaText.Text != "")
            {
                try
                {
                    //Hide the keyboard when the search button is clicked
                    InputMethodManager inputManager = (InputMethodManager) this.GetSystemService(InputMethodService);
                    inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                    searchGeocoder = new Geocoder(this);
                    searchAddress = searchGeocoder.GetFromLocationName(searchCriteriaText.Text, 1).ToList()
                        .LastOrDefault();
                    searchLocation = new LatLng(searchAddress.Latitude, searchAddress.Longitude);
                    GMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(searchLocation, 17));
                    GMap.Clear();
                    MarkerOptions options = new MarkerOptions()
                        .SetPosition(searchLocation)
                        .SetTitle(searchCriteriaText.Text);
                    GMap.AddMarker(options);

                }
                catch (NullReferenceException)
                {
                    Toast.MakeText(ApplicationContext, "Error: Unable to locate, please try again", ToastLength.Long).Show();
                }
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.GMap = googleMap;

            GMap.UiSettings.ZoomControlsEnabled = true;

            LatLng latLng = new LatLng(Convert.ToDouble(53.381129), Convert.ToDouble(-1.470085)); //Sheffield
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latLng, 15);
            GMap.MoveCamera(camera);

            MarkerOptions options = new MarkerOptions()
                .SetPosition(latLng)
                .SetTitle("Sheffield");

            GMap.AddMarker(options);
        }

    }
}