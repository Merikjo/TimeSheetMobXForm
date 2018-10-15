using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;

namespace TimeSheetMXF.Droid
{

    //public static class App
    //{
    //    //public static Java.IO.File _file;
    //    //public static Java.IO.File _dir;
    //    //public static Android.Graphics.Bitmap bitmap;
    //}


    [Activity(Label = "TimeSheetMXF", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
    ILocationListener //lisätään luokan periytyvyyttä
    {
        public static Android.Locations.LocationManager LocationManager;

        //public static MainActivity AndroidMainActivity;

        public object GoogleApiAvailability { get; private set; }
        public object ConnectionResult { get; private set; }

 


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
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
            TimeSheetMXF.Models.GpsLocationModel.Latitude = location.Latitude;
            TimeSheetMXF.Models.GpsLocationModel.Longitude = location.Longitude;
            TimeSheetMXF.Models.GpsLocationModel.Altitude = location.Altitude;
        }


        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}