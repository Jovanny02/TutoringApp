using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Runtime.Serialization;
//using System.Xml.Serialization;
//using System.Text.Json;

namespace TutoringApp.Models
{
    [JsonObject(IsReference = true)]
    public class SkillSection : ObservableCollection<Skill>
    {
        public SkillSection()
        {
            //skills = new ObservableCollection<Skill>();
        }
       // [XmlElementAttribute("SectionTitle")]
        public string SectionTitle { get; set; }
        //[XmlElementAttribute("Skills")]

        public ObservableCollection<Skill> skills => this;
        //public ObservableCollection<Skill> skills { get; set; }

        // public string HelloText { get; set; } = "hello!";

        public string serializedSkills;

        public void serializeSkills()
        {
             ObservableCollection<Skill> temp = skills;
            serializedSkills = JsonConvert.SerializeObject(temp);
            return;
        }


        public void deleteSkill (Skill skill)
        {
            skills.Remove(skill);
        }

       // public void Add (Skill skill)
       // {
       //     skills.Add(skill);
       // }

    }

    public class Skill
    {
        public Skill()
        {

        }
       // [XmlElementAttribute("sectionTitle")]
        public string sectionTitle { get; set; }
        //[XmlElementAttribute("skillName")]

        public string skillName { get; set; }
    }


}
