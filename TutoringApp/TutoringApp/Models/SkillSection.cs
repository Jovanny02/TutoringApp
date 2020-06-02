using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    class SkillSection : List<Skill>
    {
        public string sectionTitle { get; set; }

        public List<Skill> skills => this;
    }

    class Skill
    {
        public string skillName { get; set; }
    }


}
