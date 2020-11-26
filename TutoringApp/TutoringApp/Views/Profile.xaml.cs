using System;
using Xamarin.Forms;
using System.Collections.Generic;
using TutoringApp.ViewModels;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : BaseContentPage
    {
        ProfileVM profileVM;

        public ICommand EditEducationCommand;

        public ICommand EditCourseCommand;
        public ICommand saveUserCommand;

        public Profile()
        {
            //TODO Modify to get this value from a higher level
            profileVM = new ProfileVM();

            EditEducationCommand = profileVM.EditEducationCommand;
            EditCourseCommand = profileVM.EditCourseCommand;
            saveUserCommand = profileVM.saveUserCommand;

            BindingContext = profileVM;
            profileVM.Navigation = Navigation;
            InitializeComponent();

            //set sizing for circular picture
            pictureSize = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * (0.23); 
            radius = pictureSize / 2;

            userPicture.HeightRequest = pictureSize;
            userPicture.WidthRequest = pictureSize;
            userPicture.CornerRadius = (float)radius;

            EditLabelSize = (int)(pictureSize / 3);
            editLabel.HeightRequest = EditLabelSize;
            editLabelShadow.HeightRequest = EditLabelSize;
            editLabelShadow.WidthRequest = pictureSize;


            double deviceWidthUnits = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            double deviceHeightUnits = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;


            saveButton.HeightRequest = deviceHeightUnits * .1; //10 percent height for button
            saveButton.WidthRequest = deviceWidthUnits * .4; //40 percent width
            saveButton.CornerRadius = (int)(deviceHeightUnits * .05); //

            emptyStack.HeightRequest = deviceHeightUnits * .1;

            ZoomLabel.WidthRequest = deviceWidthUnits * .45;
            ZoomEditor.WidthRequest = deviceWidthUnits * .55;
        }
        public double pictureSize { get; set; }

        public int EditLabelSize { get; set; }

        public double radius { get; set; }
        private void EducationList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            EditEducationCommand.Execute(e.Item);
        }


        private void CourseList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            EditCourseCommand.Execute(e.Item);

            skillsList.SelectedItem = null;

        }

        private void saveButton_Clicked(object sender, EventArgs e)
        {
            profileVM.saveUserCommand.Execute(e);
        }
    }
}