﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeSheetMobXForm.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeSheetMobXForm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkAssignmentPage : ContentPage
	{
		public WorkAssignmentPage ()
		{
			InitializeComponent ();

            assignmentList.ItemsSource = new string[] { "" };
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        latitudeLabel.Text = GpsLocationModel.Latitude.ToString("0.000");
                        longitudeLabel.Text = GpsLocationModel.Longitude.ToString("0.000");
                    });
                    await Task.Delay(5000);
                }
            });
        }

        //video 3./4
        public async void LoadWorkAssignments(object sender, EventArgs e)

        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://mobilebackendmvc-api2.azurewebsites.net/");
                string json = await client.GetStringAsync("/api/workassignment");
                string[] assignments = JsonConvert.DeserializeObject<string[]>(json);

                assignmentList.ItemsSource = assignments;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.GetType().Name + ": " + ex.Message;
                assignmentList.ItemsSource = new string[] { errorMessage };
            }
        }
        public async void StartWork(object sender, EventArgs e) //StopWork = rutiini (Clicked Button)
        {
            string assignmentName = assignmentList.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(assignmentName))
            {

                await DisplayAlert("Start Work", "You must select work assignment first.", "OK");

            }

            else

            {
                try
                {
                    WorkAssignmentOperationModel data = new WorkAssignmentOperationModel()
                    {
                        Operation = "Start",
                        AssignmentTitle = assignmentName
                    };

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://mobilebackendmvc-api2.azurewebsites.net/");

                    string input = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(input, Encoding.UTF8, "application/json");


                    HttpResponseMessage message = await client.PostAsync("/api/workassignment", content);
                    string reply = await message.Content.ReadAsStringAsync();
                    bool success = JsonConvert.DeserializeObject<bool>(reply);

                    if (success)
                    {
                        await DisplayAlert("Start Work", "Work has been started.", "Close");
                    }

                    else
                    {
                        await DisplayAlert("Start Work", "Could not start work.", "Close");
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.GetType().Name + ": " + ex.Message;
                    assignmentList.ItemsSource = new string[] { errorMessage };
                }
            }
        }


        public async void StopWork(object sender, EventArgs e) //StopWork = rutiini 
        {
            string assignmentName = assignmentList.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(assignmentName))
            {
                await DisplayAlert("Stop Work", "You must select work assignment first.", "OK");
            }
            else
            {

                try
                {
                    WorkAssignmentOperationModel data = new WorkAssignmentOperationModel()
                    {
                        Operation = "Stop",
                        AssignmentTitle = assignmentName
                    };

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://mobilebackendmvc-api2.azurewebsites.net/");
                    string input = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(input, Encoding.UTF8, "application/json");

                    HttpResponseMessage message = await client.PostAsync("/api/workassignment", content);
                    string reply = await message.Content.ReadAsStringAsync();
                    bool success = JsonConvert.DeserializeObject<bool>(reply);

                    if (success)
                    {
                        await DisplayAlert("Stop Work", "Work has been stopped.", "Close");
                    }

                    else
                    {
                        await DisplayAlert("Stop Work", "Could not stop work.", "Close");
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.GetType().Name + ": " + ex.Message;
                    assignmentList.ItemsSource = new string[] { errorMessage };
                }
            }
        }
	}
}