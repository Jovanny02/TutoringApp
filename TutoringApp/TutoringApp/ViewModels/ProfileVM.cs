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
using Xamarin.Forms.Internals;

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

            //NOTE: need to change to programatically tell the number of different sections and skills in each one
            SkillListHeight = 280;
            initSkills();
        }

        private void initSkills()
        {

            SkillSection ProgLanguages = new SkillSection()
            {   
                new Skill() {sectionTitle= "Programming Languages", skillName = "C#"},
                new Skill() {sectionTitle= "Programming Languages", skillName = "Java"},
                new Skill() {sectionTitle= "Programming Languages", skillName = "C++" }
             };
            ProgLanguages.sectionTitle = "Programming Languages";

            SkillSection languages = new SkillSection()
            {
                new Skill() {sectionTitle= "Languages", skillName = "English"},
                new Skill() {sectionTitle= "Languages", skillName = "Spanish"}
            };
            languages.sectionTitle = "Languages";

            Skills = new ObservableCollection<SkillSection>()
            {
                ProgLanguages ,
                languages
            };
        }


        #region commands
        public ICommand editBioCommand => new Command(() =>
        {
            IsBioReadOnly = !IsBioReadOnly;
            IsBioEditing = !IsBioEditing;
        });

        public ICommand saveEducationCommand => new Command(() =>
        {

            //checks if key still holds initial value, meaning new item was added
            if (newEducationSection.key == int.MaxValue)
            {

                //NOTE ASSUMPTION IS THAT LAST ELEMENT WILL HAVE HIGHEST KEY VALUE. ASSUMPTION IS NEEDED FOR SAVE COMMAND TO WORK PROPERLY
                if (EducationSections.Any())
                    newEducationSection.key = EducationSections.Last().key + 1;
                else
                    newEducationSection.key = 0;

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
            details.hideDelete();
            Navigation.PushAsync(details);
        });

        public ICommand DeleteEducationCommand => new Command(() =>
        {
            EducationSections.Remove(newEducationSection);
            Navigation.PopAsync();

            EducationListHeight -= 80;
        });
        public ICommand EditEducationCommand => new Command((object selectedSection) =>
        {
            // Used for comparison in saveEducationCommand
            newEducationSection = (EducationSection)selectedSection;

            EducationDetails details = new EducationDetails();
            details.SaveCommand = saveEducationCommand;
            details.deleteCommand = DeleteEducationCommand;
            details.BindingContext = newEducationSection;
            Navigation.PushAsync(details);
        });

        public ICommand EditSkillCommand => new Command((object selectedSkill) =>
        {
            newSkill = (Skill)selectedSkill;
            oldSkill = (Skill)selectedSkill;
            SkillDetails skillDetails = new SkillDetails(false);
            skillDetails.BindingContext = newSkill;
            skillDetails.deleteCommand = deleteSkillCommand;
            skillDetails.saveCommand = saveSkillCommand;

            Navigation.PushAsync(skillDetails);

            isEditingSkill = true;
        });


        public ICommand saveSkillCommand => new Command(() =>
        {
            if (isEditingSkill)
            {
                for (int i = 0; i < Skills.Count; i++)
                {
                    if (Skills[i].sectionTitle == oldSkill.sectionTitle)
                    {
                        //finds element matching old element and overwrites it with new element
                        for(int kk=0; kk < Skills[i].skills.Count; kk++)
                        {
                            if (Skills[i].skills[kk] == oldSkill)
                            {
                                Skills[i].skills[kk] = newSkill;
                                break;
                            }                                
                        }
                        //clear new object 
                        newSkill = null;
                        oldSkill = null;
                        Navigation.PopAsync();
                        return;
                    }
                }
            }
            else
            {
                //if sectionTitle already exists, add skill under this
                for (int i = 0; i < Skills.Count; i++)
                {
                    if (Skills[i].sectionTitle == newSkill.sectionTitle)
                    {
                        Skills[i].Add(newSkill);
                        newSkill = null;
                        Navigation.PopAsync();

                        SkillListHeight += 40;
                        return;
                    }
                }
                //if no section title exists, add it and add new skill to it
                Skills.Add(new SkillSection() { sectionTitle = newSkill.sectionTitle });
                Skills[Skills.Count - 1].Add(newSkill);
                newSkill = null;
                SkillListHeight += 80;
                Navigation.PopAsync();
            }
        });

        public ICommand addSkillCommand => new Command(() =>
        {
            SkillDetails skillDetails = new SkillDetails(true);
            newSkill = new Skill();
            skillDetails.BindingContext = newSkill;
            skillDetails.saveCommand = saveSkillCommand;
            skillDetails.initSelectedIndex();
            Navigation.PushAsync(skillDetails);

            isEditingSkill = false;
        });

        public ICommand deleteSkillCommand => new Command(() =>
        {
            for(int i = 0; i < Skills.Count; i++)
            {
                if (Skills[i].sectionTitle == newSkill.sectionTitle)
                {
                    Skills[i].deleteSkill(newSkill);
                    SkillListHeight -= 40;

                    if (Skills[i].skills.Count == 0)
                    {
                        //if section is empty, the section is removed and list size is shrunken
                        Skills.RemoveAt(i);
                        SkillListHeight -= 40;
                    }

                }
                    
            }          

            //clear newSkill
            newSkill = null;
            Navigation.PopAsync();
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

        private int skillListHeight { get; set; }
        public int SkillListHeight { get { return skillListHeight; } set { skillListHeight = value; onPropertyChanged(); } }

        //skill objects used for adding and editing
        private Skill newSkill { get; set; } = new Skill();
        private Skill oldSkill { get; set; } = new Skill();
        private EducationSection newEducationSection { get; set; } = new EducationSection();

        public ObservableCollection<EducationSection> EducationSections { get; } = new ObservableCollection<EducationSection>();

        public ObservableCollection<ScheduleTile> ScheduleSections { get; } = new ObservableCollection<ScheduleTile>();

        public ObservableCollection<SkillSection> Skills { get; set; } = new ObservableCollection<SkillSection>();

        private int requestedPay { get; set; } = 15;
        public string RequestedPay { 
            get { return requestedPay.ToString(); }  
            set {
                int tempPay;
                Int32.TryParse( value.Replace(".", String.Empty) , out tempPay);
                //do not notify property changed if value is same (prevents looping issue)
                if (tempPay == requestedPay)
                    return;

                if (tempPay > 300)  //set min and max value to tutor pay               
                    requestedPay = 300;               
                else if(tempPay < 0)               
                    requestedPay = 0;              
                else             
                    requestedPay = tempPay;
                
                onPropertyChanged(); } 
                }
        public string ratingLabel { get { return string.Format("{0:0.0}", Math.Truncate(AverageRating * 10) / 10); } }
        public string pictureSrc { get; private set; } = "user.png";
        public string Biography { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nascetur ridiculus mus mauris vitae ultricies. " +
            "Turpis cursus in hac habitasse platea dictumst quisque sagittis. Sed arcu non odio euismod. Amet tellus cras adipiscing enim eu turpis egestas pretium. Morbi tincidunt augue interdum velit euismod in. Id donec" +
            " ultrices tincidunt arcu non sodales neque. Risus feugiat in ante metus dictum. Vel fringilla est ullamcorper eget nulla facilisi etiam. Adipiscing elit duis tristique sollicitudin nibh sit amet commodo nulla. Lorem ipsum";

        private bool isEditingSkill { get; set; } = false;
        private bool isBioReadOnly { get; set; } = true;
        public bool IsBioReadOnly { get { return isBioReadOnly; } set { isBioReadOnly = value; onPropertyChanged(); } }
        private bool isBioEditing { get; set; } = false;
        public bool IsBioEditing { get { return isBioEditing; } set { isBioEditing = value; onPropertyChanged(); } }
        #endregion
    }
}
