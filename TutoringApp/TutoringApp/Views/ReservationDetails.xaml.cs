using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using TutoringApp.ViewModels;
using System.Windows.Input;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationDetails : BaseContentPage
    {
        public ReservationDetails(bool isStudentReservation, bool canSubmitRating, string labelText)
        {
            InitializeComponent();

            double pictureSize = (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) * (.18); 
            double radius = pictureSize / 2;
            isStudent = isStudentReservation;
            //if it is a student reservation, show the tutor's information
            if (isStudentReservation)
            {
                tutorPicture.HeightRequest = pictureSize;
                tutorPicture.WidthRequest = pictureSize;
                tutorPicture.CornerRadius = (float)radius;
                tutorName.IsVisible = true;
                tutorPicture.IsVisible = true;

                if (labelText == "Completed" || labelText == "Payment Received")
                {
                    statusLabel.Text = "Session Completed";
                    submitRating.IsVisible = true;
                }
                else if (labelText == "Awaiting Review")
                {
                    statusLabel.Text = "Submit A Review";
                    submitRating.IsVisible = true;
                }
                else if (labelText == "In Progress")
                {
                    statusLabel.Text = "Session In Progress";
                }
                else if (labelText == "Upcoming")
                {
                    statusLabel.Text = "Upcoming Session";
                }
                else
                {
                    statusLabel.Text = labelText;
                }

            }
            else
            {
                studentPicture.HeightRequest = pictureSize;
                studentPicture.WidthRequest = pictureSize;
                studentPicture.CornerRadius = (float)radius;

                studentName.IsVisible = true;
                studentPicture.IsVisible = true;

                if (labelText == "Payment Received")
                {
                    statusLabel.Text = "Session Completed";
                }
                else if (labelText == "Completed")
                {
                    statusLabel.Text = "Accept Payment Now!";
                    submitButton.IsVisible = true;
                    submitButton.Text = "Initiate Payment";

                }
                else if(labelText == "In Progress")
                {
                    statusLabel.Text = "Session In Progress";
                }
                else if (labelText == "Upcoming")
                {
                    statusLabel.Text = "Upcoming Session";
                }
                else
                {
                    statusLabel.Text = labelText;
                }

            }


            if (canSubmitRating)
            {
                submitButton.IsVisible = true;
                submitRating.IsVisible = true;
            }

        }

        public ICommand submitRatingCommand;

        public ICommand initiatePayment;

        private bool IsValidUri(string uri)
        {
            try
            {
                new Uri(uri);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool isStudent { get; set; }
        async private void zoomLink_Tapped(object sender, EventArgs e)
        {
            string zoomLink = zoomLinkLabel.Text;
            try
            {
                if (!string.IsNullOrEmpty(zoomLink))
                {
                    if (!zoomLink.Trim().StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        zoomLink = "http://" + zoomLink;
                    }
                    if (IsValidUri(zoomLink))
                    {
                        await Browser.OpenAsync(new Uri(zoomLink), BrowserLaunchMode.SystemPreferred);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {

            if (isStudent)
            {
                //run submit command on ReservationListVM
                submitRatingCommand.Execute(this.BindingContext);
            }
            else
            {
                initiatePayment.Execute(this.BindingContext);
            }
        }
    }




}