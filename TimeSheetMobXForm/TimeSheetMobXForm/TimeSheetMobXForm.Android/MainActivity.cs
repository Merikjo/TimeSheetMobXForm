using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;


namespace TimeSheetMobXForm.Droid
{
    [Activity(Label = "TimeSheetMobXForm", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        ILocationListener //lisätään luokan periytyvyyttä
    {
        public static Android.Locations.LocationManager LocationManager;

        public object GoogleApiAvailability { get; private set; }
        public object ConnectionResult { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            // käynnistetään GPS-paikannus
            try
            {
                LocationManager = GetSystemService("location") as LocationManager;
                string Provider = LocationManager.GpsProvider;

                if (LocationManager.IsProviderEnabled(Provider))
                {
                    LocationManager.RequestLocationUpdates(Provider, 2000, 1, this);
                }
            }
            //viheenhallinta deguggaamista varten:
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void OnLocationChanged(Location location)
        {
            TimeSheetMobXForm.Models.GpsLocationModel.Latitude =location.Latitude;
            TimeSheetMobXForm.Models.GpsLocationModel.Longitude =location.Longitude;
            TimeSheetMobXForm.Models.GpsLocationModel.Altitude =location.Altitude;
        }

 
        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}

