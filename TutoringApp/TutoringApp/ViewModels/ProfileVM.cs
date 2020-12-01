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
using System.Text.Json;
using Acr.UserDialogs;
using System.IO;
using TutoringApp.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Linq;

namespace TutoringApp.ViewModels
{
    class ProfileVM : BaseVM
    {
        public ProfileVM()
        {
            //check if a current User exists
            if (!App.Current.Properties.ContainsKey("CurrentUser"))
            {
            #if (DEBUG)
                initTestUser();
            #endif
            }
                

            getUserInfo();

           // IsHeaderEdited = false;
        }

        private void initTestUser()
        {          

            //NOTE ASSUMPTION IS THAT LAST ELEMENT WILL HAVE HIGHEST KEY VALUE. ASSUMPTION IS NEEDED FOR SAVE COMMAND TO WORK PROPERLY
            //set sample education sections
            profileUser.EducationSections.Add(new EducationSection { Major = "Computer Engineering", fromYear = 2017, toYear = 2021, University = "University of Florida", key = 0 });
            profileUser.EducationSections.Add(new EducationSection { Major = "Transfer Degree", fromYear = 2015, toYear = 2017, University = "Florida State College At Jacksonville", key = 1 });


          //  Course EEL4712 = new Course("ECE", "EEL4712");
           // Course CIS4930 = new Course("CISE", "CIS4930");
           // Course COP4600 = new Course("CISE", "COP4600");


          //  string temp = JsonSerializer.Serialize(EEL4712);

         //   profileUser.Courses.Add(EEL4712);
           // profileUser.Courses.Add(CIS4930);
           // profileUser.Courses.Add(COP4600);


            //biography
            profileUser.Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nascetur ridiculus mus mauris vitae ultricies. " +
            "Turpis cursus in hac habitasse platea dictumst quisque sagittis. Sed arcu non odio euismod. Amet tellus cras adipiscing enim eu turpis egestas pretium. Morbi tincidunt augue interdum velit euismod in. Id donec" +
            " ultrices tincidunt arcu non sodales neque. Risus feugiat in ante metus dictum. Vel fringilla est ullamcorper eget nulla facilisi etiam. Adipiscing elit duis tristique sollicitudin nibh sit amet commodo nulla. Lorem ipsum";

            //rating
            profileUser.AverageRating = 4.0;

            profileUser.name = "Jovanny Vera";

            profileUser.shortBio = "Senior Studing Computer Engineering at UF";

            profileUser.ScheduleSections = new List<ScheduleTile>() {
            new ScheduleTile { day = DayOfWeek.Monday },
            new ScheduleTile { day = DayOfWeek.Tuesday },
            new ScheduleTile { day = DayOfWeek.Wednesday },
            new ScheduleTile { day = DayOfWeek.Thursday },
            new ScheduleTile { day = DayOfWeek.Friday },
            new ScheduleTile { day = DayOfWeek.Saturday },
            new ScheduleTile { day = DayOfWeek.Sunday }
            };

            profileUser.isTutor = true;

            profileUser.UFID = 54817581;
            //serialize object as string and save to properties
            // 
            string userString = JsonSerializer.Serialize(profileUser);
            App.Current.Properties.Add("CurrentUser", userString);
        }

        private void getUserInfo()
        {
            profileUser = JsonSerializer.Deserialize<User>(App.Current.Properties["CurrentUser"] as string);
            

            //education sections
            EducationListHeight = profileUser.EducationSections.Count() * EducationHeight;
            EducationSections = new ObservableCollection<EducationSection>(profileUser.EducationSections);
            //skills sections
            Courses = new ObservableCollection<Course>(profileUser.Courses);
            CourseListHeight = 0;
            //determine skills height
            //  for (int i = 0; i < Courses.Count; i++)
            // {
            //SkillListHeight += (CourseHeight + Skills[i].skills.Count()*CourseHeight); 
            CourseListHeight += Courses.Count() * CourseHeight;
          //  }
            //init biography
            Biography = profileUser.Biography;

            //init rating
            AverageRating = profileUser.AverageRating;

            //init pay
            requestedPay = profileUser.requestedPay;

            ScheduleSections = new ObservableCollection<ScheduleTile>(profileUser.ScheduleSections);

            name = profileUser.name;

            shortBio = profileUser.shortBio;

            pictureSrc = profileUser.pictureSrc;

            isTutor = profileUser.isTutor;

            zoomLink = profileUser.zoomLink;
        }

        private async System.Threading.Tasks.Task saveUserAsync()
        {
           // IsBioReadOnly = true;
            IsBioEditing = false;

            //Pre save checks

            if(name == null || name == String.Empty)
            {
                UserDialogs.Instance.Alert("Save Failed: Name cannot be empty", null, null);
                return;
            }
            else if(isTutor && 
                (ZoomLink == null || 
                ZoomLink == string.Empty || 
                !Uri.IsWellFormedUriString(ZoomLink, UriKind.Absolute) ||
                !ZoomLink.Contains("https://ufl.zoom.us") )){ //TODO add more extensive checks for zoom link
                UserDialogs.Instance.Alert("Save Failed: Invalid zoom link", null, null);
                return;
            }
            else if(isTutor && requestedPay < 1)
            {
                UserDialogs.Instance.Alert("Save Failed: Requested pay must be greater than $1", null, null);
                return;
            }
            else if (isTutor && (Courses == null || Courses.Count < 1) )
            {
                UserDialogs.Instance.Alert("Save Failed: You must have at least one course", null, null);
                return;
            }



            //save all user properties
            profileUser.EducationSections = EducationSections.ToList<EducationSection>();
            profileUser.Courses = Courses.ToList<Course>();
            profileUser.Biography = Biography;
            profileUser.requestedPay = requestedPay;
            profileUser.ScheduleSections = ScheduleSections.ToList<ScheduleTile>();
            profileUser.name = name;
            profileUser.shortBio = shortBio;
            profileUser.isTutor = isTutor;
            profileUser.zoomLink = zoomLink.Trim();

            bool didSave = false;
            string userString = JsonSerializer.Serialize(profileUser);
            //update current user in properties and save 
            try
            {
                UserDialogs.Instance.ShowLoading("Saving");
                didSave =  await WebAPIServices.updateUser(userString);
                UserDialogs.Instance.HideLoading();

            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Saved Failed", null, null);
                Console.WriteLine(e.Message);
                return;
            }

            if (!didSave)
            {
                UserDialogs.Instance.Alert("Saved Failed", null, null);
                return;
            }

            if (App.Current.Properties.ContainsKey("CurrentUser"))
                App.Current.Properties["CurrentUser"] = userString;
            else
                App.Current.Properties.Add("CurrentUser", userString);

            //TODO: Add a services call that saves object in database
            App.Current.SavePropertiesAsync();
            UserDialogs.Instance.Alert("Saved Successfully!", null, null);

        }

        #region commands
        public ICommand editBioCommand => new Command(() =>
        {
            IsBioEditing = !IsBioEditing;
        });

        public ICommand saveUserCommand => new Command(async () => {
            await saveUserAsync();
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

                EducationSections.Add(new EducationSection(newEducationSection));
                EducationListHeight += EducationHeight;
            }
            else
            {
                for (int i = 0; i < EducationSections.Count; i++)
                {
                    //finds education section with matching key and overwrites it
                    if (EducationSections[i].key == newEducationSection.key)
                    {
                        EducationSections[i] = new EducationSection(newEducationSection);
                        break;
                    }
                }

            }
            //sort List by toYear high to low
            List<EducationSection> tempSections = EducationSections.ToList();
            tempSections.Sort(delegate (EducationSection c1, EducationSection c2) { return -1 * c1.toYear.CompareTo(c2.toYear); });
            EducationSections = new ObservableCollection<EducationSection>(tempSections);

            //reset keys for all Education Sections
            for(int kk = 0; kk < EducationSections.Count; kk++)
            {
                EducationSections[kk].key = kk;
            }


            onPropertyChanged(nameof(EducationSections));
            newEducationSection = new EducationSection();
            //saveUser();
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

            EducationListHeight -= EducationHeight;
            //saveUser();
        });
        public ICommand EditEducationCommand => new Command((object selectedSection) =>
        {
            // Used for comparison in saveEducationCommand
            newEducationSection = new EducationSection((EducationSection)selectedSection);

            EducationDetails details = new EducationDetails();
            details.SaveCommand = saveEducationCommand;
            details.deleteCommand = DeleteEducationCommand;
            details.BindingContext = newEducationSection;
            Navigation.PushAsync(details);
        });

        public ICommand EditCourseCommand => new Command((object selectedCourse) =>
        {
            newCourse = new Course { departmentTitle = ((Course)selectedCourse).departmentTitle, courseName = ((Course)selectedCourse).courseName };
            oldCourse = new Course { departmentTitle = ((Course)selectedCourse).departmentTitle, courseName = ((Course)selectedCourse).courseName };
            SkillDetails skillDetails = new SkillDetails(false);
            skillDetails.BindingContext = newCourse;
            skillDetails.deleteCommand = deleteCourseCommand;
            skillDetails.saveCommand = saveCourseCommand;

            Navigation.PushAsync(skillDetails);

            isEditingCourse = true;
        });

        public ICommand saveCourseCommand => new Command(() =>
        {
        if (isEditingCourse)
        {
                //employee => employee.LastName.Equals(somename, StringComparison.Ordinal)
            //int index = profileUser.Courses.IndexOf(oldCourse);
            int index = profileUser.Courses.FindIndex(listCourse => listCourse.courseName.Equals(oldCourse.courseName) && listCourse.departmentTitle.Equals(oldCourse.departmentTitle));
            if (profileUser.Courses.FindIndex(listCourse => listCourse.courseName.Equals(newCourse.courseName) && listCourse.departmentTitle.Equals(newCourse.departmentTitle)) > -1)
            {
                UserDialogs.Instance.Alert("Course Already Exist", null, null);
                return;
            }
            if (index < 0)
            {
                Console.WriteLine("ERROR incorrect index");
                return;
            }

            profileUser.Courses[index] = new Course
            {
                departmentTitle = newCourse.departmentTitle,
                courseName = newCourse.courseName
            };

            profileUser.Courses.Sort(delegate (Course c1, Course c2) { return c1.courseName.CompareTo(c2.courseName); });
            Courses = new ObservableCollection<Course>(profileUser.Courses);

            onPropertyChanged(nameof(Courses));

            //clear new object 
            newCourse = null;
            oldCourse = null;                
            }
            else
            {
                if (profileUser.Courses.Contains(newCourse))
                {
                    UserDialogs.Instance.Alert("Course Already Exist", null, null);
                    return;
                }
                profileUser.Courses.Add(new Course
                {
                    departmentTitle = newCourse.departmentTitle,
                    courseName = newCourse.courseName
                });

                profileUser.Courses.Sort(delegate (Course c1, Course c2) { return c1.courseName.CompareTo(c2.courseName); });
                Courses = new ObservableCollection<Course>(profileUser.Courses);


                CourseListHeight += CourseHeight;
                newCourse = null;
                oldCourse = null;
                onPropertyChanged(nameof(Courses));

            }


            //saveUser();
            Navigation.PopAsync();
        });

        public ICommand addCourseCommand => new Command(() =>
        {
            SkillDetails skillDetails = new SkillDetails(true);
            newCourse = new Course("","");
            skillDetails.BindingContext = newCourse;
            skillDetails.saveCommand = saveCourseCommand;
            skillDetails.initSelectedIndex();
            Navigation.PushAsync(skillDetails);

            isEditingCourse = false;
        });

        public ICommand deleteCourseCommand => new Command(() =>
        {
            //int index = Courses.FindIndex(listCourse => listCourse.courseName.Equals(oldCourse.courseName) && listCourse.departmentTitle.Equals(oldCourse.departmentTitle));
            int temp = Courses.IndexOf(oldCourse);
            Courses.Remove(oldCourse);
           // Courses.RemoveAt(index);
            CourseListHeight -= CourseHeight;
            //clear newCourse
            newCourse = null;
            //saveUser();
            Navigation.PopAsync();
        });
        public ICommand selectPictureCommand  => new Command(async () =>
        {
            try
            {        
                Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
                if (stream != null)
                {
                    //note could add service call Web API to delete previous picture


                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription("test.png", stream),
                     //   PublicId = "Users/" + profileUser.UFID.ToString(),
                        UploadPreset = "rzvpyvwl",
                        Unsigned = true
                    };

                    Account account = new Account("gatoraid"); 
                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                    //Set a default height as 800 pixels (width will be 800 since aspect ratio is 1:1) so that image will fit in all areas and will just be shrunk to fit
                    profileUser.pictureSrc = uploadResult.Url.ToString().Replace("upload/", "upload/h_800,ar_1:1,c_fill,g_auto,r_max/").Replace("http", "https");
                     
                    pictureSrc = profileUser.pictureSrc;
                    //saveUser();
                }


            }
            catch(Exception e)
            {

                Console.WriteLine(e.Message);
            }

        });

        #endregion

        #region properties
        private const int CourseHeight = 40;
        private const int EducationHeight = 80;

        public User profileUser = new User();

        public double AverageRating { get; 
            set; } //IN USER
        //Used to allow listview resizing after adding elements to education section 
        private int educationListHeight { get; set; }
        public int EducationListHeight { get { return educationListHeight; } set { educationListHeight = value; onPropertyChanged(); } }

        private int courseListHeight { get; set; }
        public int CourseListHeight { get { return courseListHeight; } set { courseListHeight = value; onPropertyChanged(); } }

        //skill objects used for adding and editing
        private Course newCourse { get; set; } = new Course("","");
        private Course oldCourse { get; set; } = new Course("","");
        private EducationSection newEducationSection { get; set; } = new EducationSection();

        public ObservableCollection<EducationSection> EducationSections { get; set; } = new ObservableCollection<EducationSection>(); //IN USER

        public ObservableCollection<ScheduleTile> ScheduleSections { get; set; } = new ObservableCollection<ScheduleTile>(); //IN USER

        public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>(); //IN USER

        private int requestedPay { get; set; } //IN USER
        public string RequestedPay { 
            get { return "$" + requestedPay.ToString(); }  
            set {
                int tempPay;
                value = value.Replace("$", String.Empty);
                Int32.TryParse(value.Replace(".", String.Empty), out tempPay);
                    
                //do not notify property changed if value is same (prevents looping issue)
                if (tempPay == requestedPay)
                    return;

                if (tempPay > 300)  //set min and max value to tutor pay               
                    requestedPay = 300;               
                else if(tempPay < 0)               
                    requestedPay = 0;              
                else             
                    requestedPay = tempPay;

                //saveUser();
                onPropertyChanged(); } 
                }
        public string ratingLabel { get { return string.Format("{0:0.0}", Math.Truncate(AverageRating * 10) / 10); } }
        private string PictureSrc { get; set; }
        public string pictureSrc { get { return PictureSrc; } set { PictureSrc = value; onPropertyChanged(); } } 
        public string Biography { get; set; }
        private bool IsTutor { get; set; } 
        public bool isTutor { get { return IsTutor; } set { IsTutor = value; onPropertyChanged(); } }

        private string ZoomLink { get; set; } = "";
        public string zoomLink { get { return ZoomLink; } set { ZoomLink = value; onPropertyChanged(); } }
        private string Name { get; set; }

        public string name { get { return Name; } set {
                if (Name == value)
                    return;
                Name = value; 
                //IsHeaderEdited = true; 
            } }
        private string ShortBio { get; set; }

        public string shortBio { get { return ShortBio; } 
            set {
                if (ShortBio == value)
                    return;
                ShortBio = value;
              //  IsHeaderEdited = true;
            } }


        private bool isEditingCourse { get; set; } = false;
        /*   private bool isBioReadOnly { get; set; } = true;
           public bool IsBioReadOnly { get { return isBioReadOnly; } set { isBioReadOnly = value; onPropertyChanged(); } }

           private bool isHeaderEdited { get; set; } = false;
           public bool IsHeaderEdited { get { return isHeaderEdited; } 
               set { isHeaderEdited = value; 
                   onPropertyChanged(); } }
           */
        private bool isBioEditing { get; set; } = false;
        public bool IsBioEditing { get { return isBioEditing; } set { isBioEditing = value; onPropertyChanged(); } }
#endregion
    }
}
