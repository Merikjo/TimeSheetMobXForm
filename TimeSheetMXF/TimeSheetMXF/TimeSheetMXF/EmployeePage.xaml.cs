using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace TimeSheetMXF
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmployeePage : ContentPage
	{
		public EmployeePage ()
		{
			InitializeComponent ();
            //BindingContext = new EmployeePage();
            employeeList.ItemsSource = new string[] { "" };
            employeeList.ItemSelected += EmployeeList_ItemSelected;
            //rebuild, kun lisäät uuden listauksen
            customerList.ItemsSource = new string[] { "" };
            customerList.ItemSelected += CustomerList_ItemSelected;
        }

        private async void EmployeeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string employee = employeeList.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(employee))
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://mobilebackendmvc-api2.azurewebsites.net/");
                    string json = await client.GetStringAsync("/api/employee?employeeNames=" + employee);

                    byte[] imageBytes = JsonConvert.DeserializeObject<byte[]>(json);

                    //employeeImage.Source = ImageSource.FromStream(
                    //    () => new MemoryStream(imageBytes));
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.GetType().Name + ": " + ex.Message;
                    employeeList.ItemsSource = new string[] { errorMessage };
                }
            }
        }

        public async void LoadEmployees(object sender, EventArgs e) //EventArgs=tapahtuma-argumentit Sender=olio, joka lähtettää tapahtuman, muoto tulee XAML -kielestä
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://mobilebackendmvc-api2.azurewebsites.net/");
                string json = await client.GetStringAsync("/api/employee");
                string[] employees = JsonConvert.DeserializeObject<string[]>(json);

                employeeList.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.GetType().Name + ": " + ex.Message;

                employeeList.ItemsSource = new string[] { errorMessage };
            }
        }

        private async void ListWorkAssignments(object sender, EventArgs e)
        {
            string employee = employeeList.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(employee))
            {
                await DisplayAlert("List Work", "You must select employee first.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new WorkAssignmentPage());
            }
        }

        private async void CustomerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string customer = customerList.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(customer))
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://mobilebackendmvc-api2.azurewebsites.net/");
                    string json = await client.GetStringAsync("/api/customer?customerNames=" + customer);

                    //byte[] imageBytes = JsonConvert.DeserializeObject<byte[]>(json);

                    //customerImage.Source = ImageSource.FromStream(
                    //    () => new MemoryStream(imageBytes));
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.GetType().Name + ": " + ex.Message;
                    customerList.ItemsSource = new string[] { errorMessage };
                }
            }
        }

        public async void LoadCustomers(object sender, EventArgs e) //EventArgs=tapahtuma-argumentit Sender=olio, joka lähtettää tapahtuman, muoto tulee XAML -kielestä
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://mobilebackendmvc-api2.azurewebsites.net/");
                string json = await client.GetStringAsync("/api/customer");
                string[] customers = JsonConvert.DeserializeObject<string[]>(json);

                customerList.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.GetType().Name + ": " + ex.Message;

                customerList.ItemsSource = new string[] { errorMessage };
            }
        }

        //private async void TakeEmployeePicture(object sender, EventArgs e)
        //{
        //    string employee = employeeList.SelectedItem?.ToString();
        //    if (!string.IsNullOrEmpty(employee))
        //    {
        //        ICamera camera = DependencyService.Get<ICamera>();
        //        camera.TakePicture(employee);
        //    }
        //}
    }
}