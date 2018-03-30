using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Webkit;

namespace EstateAgentManagementSystem
{
    [Activity(Label = "EAMS", Theme = "@style/Theme.DesignDemo", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        #region Menu

        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        #endregion Menu

        private bool createMenuOnce;
        private RightmoveWebViewFragment rm; //used for getting the android back button to work on webpages
        private ZooplaWebViewFragment zp; //used for getting the android back button to work on webpages


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);


            FragmentTransaction ft = this.FragmentManager.BeginTransaction();
            HomeFragment hf = new HomeFragment();
            ft.Replace(Resource.Id.ll, hf);
            ft.Commit();


            #region Menu

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.c_drawer_layout);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.c_nav_view);
            SetupDrawerContent(navigationView);

            View header = navigationView.GetHeaderView(0);
            TextView navheader_username = header.FindViewById<TextView>(Resource.Id.navheader_username);
            navheader_username.Text = "Estate Agent Management System";

            #endregion Menu
        }

        #region Menu

        private void SetupDrawerContent(NavigationView navigationView)
        {
            navigationView.SetCheckedItem(Resource.Id.nav_home);

            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                FragmentTransaction ft = this.FragmentManager.BeginTransaction();
                if (e.MenuItem.ItemId == Resource.Id.nav_agentschedule)
                {
                    AgentScheduleFragment asf = new AgentScheduleFragment();
                    // The fragment will have the ID of Resource.Id.fragment_container.
                    ft.Replace(Resource.Id.ll, asf);
                }
                else if (e.MenuItem.ItemId == Resource.Id.nav_mortgagecalc)
                {
                    MMortgageCalculatorFragment mc = new MMortgageCalculatorFragment();
                    // The fragment will have the ID of Resource.Id.fragment_container.
                    ft.Replace(Resource.Id.ll, mc);
                }
                else if (e.MenuItem.ItemId == Resource.Id.nav_rightmovewebview)
                {
                    rm = new RightmoveWebViewFragment();
                    // The fragment will have the ID of Resource.Id.fragment_container.
                    ft.Replace(Resource.Id.ll, rm);
                }
                else if (e.MenuItem.ItemId == Resource.Id.nav_zooplawebview)
                {
                    zp = new ZooplaWebViewFragment();
                    // The fragment will have the ID of Resource.Id.fragment_container.
                    ft.Replace(Resource.Id.ll, zp);
                }
                else if (e.MenuItem.ItemId == Resource.Id.nav_notes)
                {
                    NotesHomeFragment nn = new NotesHomeFragment();
                    // The fragment will have the ID of Resource.Id.fragment_container.
                    ft.Replace(Resource.Id.ll, nn);
                }
                else if (e.MenuItem.ItemId == Resource.Id.nav_camera)
                {
                    CameraFragment cf = new CameraFragment();
                    // The fragment will have the ID of Resource.Id.fragment_container.
                    ft.Replace(Resource.Id.ll, cf);
                }
                else if (e.MenuItem.ItemId == Resource.Id.nav_googlemaps)
                {
                    StartActivity(typeof(GoogleMapsActivity));
                }
                else if (e.MenuItem.ItemId == Resource.Id.nav_home)
                {
                    HomeFragment hf = new HomeFragment();
                    // The fragment will have the ID of Resource.Id.fragment_container.
                    ft.Replace(Resource.Id.ll, hf);
                }

                // Commit the transaction.
                ft.Commit();
                drawerLayout.CloseDrawers();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (!createMenuOnce) //Not a great way, issues with duplicating the navigation menu, this is a workaround.
            {
                createMenuOnce = true;
                navigationView.InflateMenu(Resource.Menu.menu_main);
            }
            return true;
        }

        #endregion Menu

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            try
            {
                if (keyCode == Keycode.Back && rm.web_view.CanGoBack())
                {
                    rm.web_view.GoBack();
                    return true;
                }
                if (keyCode == Keycode.Back && zp.web_view.CanGoBack())
                {
                    zp.web_view.GoBack();
                    return true;
                }
                return base.OnKeyDown(keyCode, e);
            }
            catch (NullReferenceException)
            {
                try
                {
                    if (keyCode == Keycode.Back && zp.web_view.CanGoBack())
                    {
                        zp.web_view.GoBack();
                        return true;
                    }
                    return base.OnKeyDown(keyCode, e);
                }
                catch (NullReferenceException)
                {
                    return false;
                }

            }

        }
    }
    public class HelloWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
        {
            view.LoadUrl(request.Url.ToString());
            return false;
        }
    }
}