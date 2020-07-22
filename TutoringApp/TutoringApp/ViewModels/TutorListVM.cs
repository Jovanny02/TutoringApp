﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text;
using TutoringApp.Models;
using TutoringApp.Views;

namespace TutoringApp.ViewModels
{
    class TutorListVM : BaseVM
    {
        public TutorListVM()
        {
            TutorList = new ObservableCollection<TutorInfo>();

            PerformSearchCommand = new Command<string>((string query) =>
            {
                //TODO create search call
                SearchQuery = query;
            
            });
            //adds inital items to list
            addTutors();
        }

        private int numTutorsDisplayed { get; set; } = 0;
        private string searchQuery { get; set; }
        public ICommand PerformSearchCommand { protected set; get; }
        public string SearchQuery
        {
            get { return searchQuery; }
            set { searchQuery = value; onPropertyChanged(); } //needed for binded Items to see changes appear on view
        }

        public ObservableCollection<TutorInfo> TutorList { get; private set;} 

        //placeholder to add more tutors to list
        private void addTutors()
        {
            List<string> topicsList = new List<string>();
            topicsList.Add("EEL3701");
            topicsList.Add("C#");
            topicsList.Add("COP4701");


            for (int i = 0; i < 10; i++, numTutorsDisplayed++)
            {
                TutorInfo info = new TutorInfo();
                info.tutorName = "Limp Shrimp";
                info.tutorTopics = topicsList;
                info.shortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
                info.displayPosition = numTutorsDisplayed;
                TutorList.Add(info);
            }


        }

        //used to add more tutors to list to display on View
        public void LoadTutors(object item)
        {
            TutorInfo tutor = (TutorInfo)item;

            if (tutor.displayPosition == TutorList.Count -1)
            {
                addTutors();
                onPropertyChanged();
            }
        }


    }
}