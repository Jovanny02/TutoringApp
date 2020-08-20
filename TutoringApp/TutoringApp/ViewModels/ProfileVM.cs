using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using TutoringApp.Models;
using TutoringApp.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace TutoringApp.ViewModels
{
    class ProfileVM : BaseVM
    {
        public ProfileVM()
        {
           

            AverageRating = 4.7;
            //creates circular picture
            pictureSize = (DeviceDisplay.MainDisplayInfo.Width * 0.09);
            radius = pictureSize / 2;
            Console.WriteLine("Radius: " + radius + "  Size: " + pictureSize);

            //NOTE ASSUMPTION IS THAT LAST ELEMENT WILL HAVE HIGHEST KEY VALUE. ASSUMPTION IS NEEDED FOR SAVE COMMAND TO WORK PROPERLY
            //set sample education sections
            EducationSections.Add(new EducationSection{ Major = "Computer Engineering", fromYear = 2017, toYear = 2021, University = "University of Florida", key=0 });
            EducationSections.Add(new EducationSection { Major = "Transfer Degree", fromYear = 2015, toYear = 2017, University = "Florida State College At Jacksonville", key=1 });
            EducationListHeight = EducationSections.Count() * 80;

            //add sample schedule sections
            ScheduleSections.Add(new ScheduleTile { day = DayOfWeek.Monday });
            ScheduleSections.Add(new ScheduleTile { day = DayOfWeek.Tuesday });
            ScheduleSections.Add(new ScheduleTile { day = DayOfWeek.Wednesday });
            ScheduleSections.Add(new ScheduleTile { day = DayOfWeek.Thursday });
            ScheduleSections.Add(new ScheduleTile { day = DayOfWeek.Friday });
            ScheduleSections.Add(new ScheduleTile { day = DayOfWeek.Saturday });
            ScheduleSections.Add(new ScheduleTile { day = DayOfWeek.Sunday });
        }

        #region commands
        public ICommand editBioCommand => new Command(() =>
        {
            IsBioReadOnly = !IsBioReadOnly;
            IsBioEditing = !IsBioEditing;
            Console.WriteLine("isBioReadOnly: " + IsBioReadOnly);
            Console.WriteLine("isBioEditing: " + IsBioEditing);
        });

        public ICommand saveEducationCommand => new Command(() =>
        {

            //checks if key still holds initial value, meaning new item was added
            if (newEducationSection.key == int.MaxValue)
            {

                //NOTE ASSUMPTION IS THAT LAST ELEMENT WILL HAVE HIGHEST KEY VALUE. ASSUMPTION IS NEEDED FOR SAVE COMMAND TO WORK PROPERLY
                newEducationSection.key = EducationSections.Last().key + 1;
                EducationSections.Add(newEducationSection);
                EducationListHeight += 80;
            }
            else
            {
                for (int i = 0; i < EducationSections.Count; i++)
                {
                    //finds education section with matching key and overwrites it
                    if (EducationSections[i].key == newEducationSection.key)
                    {
                        EducationSections[i] = newEducationSection;
                        break;
                    }
                }

            }

            newEducationSection = new EducationSection();
            Navigation.PopAsync();
        });
        public ICommand addEducationCommand => new Command(() =>
        {
            newEducationSection = new EducationSection();

            EducationDetails details = new EducationDetails();
            details.BindingContext = newEducationSection;
            details.SaveCommand = saveEducationCommand;
            Navigation.PushAsync(details);
        });

        public ICommand EditEducationCommand => new Command((object selectedSection) =>
        {
            // Used for comparison in saveEducationCommand
            newEducationSection = (EducationSection)selectedSection;

            EducationDetails details = new EducationDetails();
            details.SaveCommand = saveEducationCommand;
            details.BindingContext = newEducationSection;
            Navigation.PushAsync(details);
        });
        #endregion



        #region properties


        private Double pictureSize;
        public Double PictureSize
        {
            get { return pictureSize; }
        }

        private Double radius;
        public Double Radius
        {
            get { return radius; }
        }

        public Double AverageRating { get; set; }
        //Used to allow listview resizing after adding elements to education section 
        private int educationListHeight { get; set; }
        public int EducationListHeight { get { return educationListHeight; } set { educationListHeight = value; onPropertyChanged(); } }

        private EducationSection newEducationSection { get; set; } = new EducationSection();

        public ObservableCollection<EducationSection> EducationSections { get; } = new ObservableCollection<EducationSection>();

        public ObservableCollection<ScheduleTile> ScheduleSections { get; } = new ObservableCollection<ScheduleTile>();

        public string ratingLabel { get { return string.Format("{0:0.0}", Math.Truncate(AverageRating * 10) / 10); } }
        public string pictureSrc { get; private set; } = "user.png";
        public string Biography { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nascetur ridiculus mus mauris vitae ultricies. " +
            "Turpis cursus in hac habitasse platea dictumst quisque sagittis. Sed arcu non odio euismod. Amet tellus cras adipiscing enim eu turpis egestas pretium. Morbi tincidunt augue interdum velit euismod in. Id donec" +
            " ultrices tincidunt arcu non sodales neque. Risus feugiat in ante metus dictum. Vel fringilla est ullamcorper eget nulla facilisi etiam. Adipiscing elit duis tristique sollicitudin nibh sit amet commodo nulla. Lorem ipsum";

        private bool isBioReadOnly { get; set; } = true;
        public bool IsBioReadOnly { get { return isBioReadOnly; } set { isBioReadOnly = value; onPropertyChanged(); } }
        private bool isBioEditing { get; set; } = false;
        public bool IsBioEditing { get { return isBioEditing; } set { isBioEditing = value; onPropertyChanged(); } }
        #endregion
    }
}
