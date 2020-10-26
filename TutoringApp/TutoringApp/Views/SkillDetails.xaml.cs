﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TutoringApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkillDetails : BaseContentPage
    {
        public ICommand deleteCommand;

        public ICommand saveCommand;

        public List<string> depatmentSections = new List<string>();

        
        public SkillDetails(bool isNewSkill)
        {
            
            InitializeComponent();
            initPicker();

            if(isNewSkill)
                titleLabel.Text = "Add Course";

            deleteButton.IsVisible = !isNewSkill;
            skillSectionPicker.IsEnabled = isNewSkill;                   
        }
        //Work around to set a starting value
        public void initSelectedIndex ()
        {
            skillSectionPicker.SelectedIndex = 0;
        }

        private void initPicker()
        {
            //TODO: add a comprehensive list as a source of skills
            depatmentSections.AddRange(new List<string> { "ECE", "MAE" , "CISE" });
            skillSectionPicker.ItemsSource = depatmentSections;
        }


        private void save_Clicked(object sender, EventArgs e)
        {
            if (skillEntry.Text == "" || skillEntry.Text == null)
            {
                errorLabel.Text = "Course name can not be empty";
                return;
            }

            saveCommand.Execute(sender);
        }

        private void delete_Clicked(object sender, EventArgs e)
        {
            deleteCommand.Execute(sender);
        }


    }
}