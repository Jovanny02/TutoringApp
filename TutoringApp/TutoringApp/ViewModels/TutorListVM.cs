using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text;
using TutoringApp.Models;
using TutoringApp.Views;
using System.Text.Json;
using TutoringApp.Services;
using Acr.UserDialogs;

namespace TutoringApp.ViewModels
{
    class TutorListVM : BaseVM
    {
        public TutorListVM(string searchClass)
        {


            PerformSearchCommand.Execute(searchClass);
        }

        private int numTutorsDisplayed { get; set; } = 0;
        private string searchQuery { get; set; }
        public ICommand PerformSearchCommand => new Command<string>(async (searchClass) =>
        {
            UserDialogs.Instance.ShowLoading("Searching For Tutors");
            List<TutorInfo> tempList = await WebAPIServices.getTutors(searchClass);
            UserDialogs.Instance.HideLoading();
            if(tempList != null)
            {
                TutorList = new ObservableCollection<TutorInfo>(tempList);
            }
            else
            {
                TutorList = null;
            }


            //properties changed
            onPropertyChanged(nameof(TutorList));
            onPropertyChanged(nameof(isTextVisible));
        });

        public bool isTextVisible { get { return (TutorList == null || TutorList.Count < 1); } } 
        public string SearchQuery
        {
            get { return searchQuery; }
            set { searchQuery = value; onPropertyChanged(); } //needed for binded Items to see changes appear on view
        }

        public ObservableCollection<TutorInfo> TutorList { get; private set;}

        //placeholder to add more tutors to list
        private void addTutors()
        {

            //for (int i = 0; i < 10; i++, numTutorsDisplayed++)
            //{
            //    TutorInfo info = new TutorInfo();
            //    info = JsonSerializer.Deserialize<TutorInfo>(App.Current.Properties["CurrentUser"] as string);
            //    info.displayPosition = numTutorsDisplayed;
            //    TutorList.Add(info);
            //}


        }

        //used to add more tutors to list to display on View
        public ICommand LoadTutorsCommand => new Command(() =>
        {
            // TutorInfo tutor = (TutorInfo)item;

            // if (tutor.displayPosition == TutorList.Count -1)
            // {
          //  addTutors();
           // onPropertyChanged(nameof(TutorList));
            //  }
        });


    }
}
