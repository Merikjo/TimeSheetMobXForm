using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TimeSheetMXF
{
    public partial class App : Application
    {
        public App()
        {

            //InitializeComponent();
            // The root page of your application
            //Mahdollistetaan siirtyminen toiselle sivulle
            //Video 3./4
            //InitializeComponent();

            //MainPage = new MainPage();

            MainPage = new NavigationPage(new EmployeePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
