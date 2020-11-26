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
        public ReservationDetails(bool isStudentReservation, bool canSubmitRating, bool isComplete, bool awaitingReview)
        {
            InitializeComponent();

            double pictureSize = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * (.3); 
            double radius = pictureSize / 2;

            //if it is a student reservation, show the tutor's information
            if (isStudentReservation)
            {
                tutorPicture.HeightRequest = pictureSize;
                tutorPicture.WidthRequest = pictureSize;
                tutorPicture.CornerRadius = (float)radius;
                tutorName.IsVisible = true;
                tutorPicture.IsVisible = true;

                if (isComplete)
                {
                    submitLabel.IsVisible = true;
                    submitLabel.Text = "Session Completed!";
                    submitRating.IsVisible = true;
                }
            }
            else
            {
                studentPicture.HeightRequest = pictureSize;
                studentPicture.WidthRequest = pictureSize;
                studentPicture.CornerRadius = (float)radius;

                studentName.IsVisible = true;
                studentPicture.IsVisible = true;

                if (isComplete)
                {
                    submitLabel.IsVisible = true;
                    submitLabel.Text = "Session Completed!";
                }
                else if (awaitingReview)
                {
                    submitLabel.IsVisible = true;
                    submitLabel.Text = "Awaiting Review";
                }

            }


            if (canSubmitRating)
            {
                submitButton.IsVisible = true;
                submitLabel.IsVisible = true;
                submitRating.IsVisible = true;
            }

        }

        public ICommand submitRatingCommand;

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
            double value = submitRating.Value;

            Console.WriteLine(value);

            //run submit command on ReservationListVM
            submitRatingCommand.Execute(this.BindingContext);
        }
    }




}