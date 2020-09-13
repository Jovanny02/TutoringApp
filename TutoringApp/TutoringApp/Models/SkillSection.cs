using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace TutoringApp.Models
{
    class SkillSection : ObservableCollection<Skill>
    {
        public string sectionTitle { get; set; }

         public ObservableCollection<Skill> skills => this;

        public void deleteSkill (Skill skill)
        {
            skills.Remove(skill);
        }



    }

    class Skill
    {
        public string sectionTitle { get; set; }
        public string skillName { get; set; }
    }


}
