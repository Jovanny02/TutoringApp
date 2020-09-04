using System;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Models;

namespace TutoringApp.ViewModels
{
    class ResumeVM : BaseVM
    {
        public  ResumeVM()
        {
            jobs.Add(new WorkTile
            {
                companyName = "Jacksonville Electric Authority",
                fromDate = "May 2019",
                toDate = "Aug 2019",
                iconSrc = "JEA.png",
                jobTitle = "Software Development Intern",
                location = "Jacksonville, FL",
                jobDescription = "• Designed and maintained ASP.NET applications using C# for use by employees " + Environment.NewLine +
                "• Developed database procedures, packages, and triggers in Oracle SQL Developer using Oracle SQL " + Environment.NewLine +
                "• Created and updated in-house ASP.NET web pages " + Environment.NewLine +
                "• Used Agile methodology to ensure high quality software delivery " + Environment.NewLine +
                "Skills / Technologies: C#, ASP.NET, Oracle SQL, SQL Developer, Visual Studio, Agile"
            });

            jobs.Add(new WorkTile
            {
                companyName = "Independent Contractor",
                fromDate = "Oct 2012",
                toDate = "Present",
                iconSrc = "whistle.png",
                jobTitle = "Grade 8 Soccer Referee",
                location = "Jacksonville, FL",
                jobDescription = "Base Job Description"
            });

            jobs.Add(new WorkTile
            {
                companyName = "Chick-Fil-A",
                fromDate = "Jun 2016",
                toDate = "Jul 2017",
                iconSrc = "CFA.png",
                jobTitle = "Team Member",
                location = "Jacksonville, FL",
                jobDescription = "Base Job description"
            });

            education.Add(new EducationTile
            {
                University = "University of Florida",
                fromDate = "Aug 2017",
                toDate = "Present",
                iconSrc = "UF.png",
                Major = "Bachelor of Science in Computer Engineering",
                location = "Gainesville, FL",
                degreeDescription = " "
            });

            education.Add(new EducationTile
            {
                University = "Florida State College at Jacksonville",
                fromDate = "Aug 2015",
                toDate = "May 2017",
                iconSrc = "FSCJ.jfif",
                Major = "Associate of Arts",
                location = "Jacksonville, FL",
                degreeDescription = " Degree Focus "
            });
            //TODO: FIX THIS TO MAKE IT PRETTIER *************************************************************
            //List<skill> progLang = new List<skill>();
            //progLang.Add(new skill{ skillName = "C++"});
            //progLang.Add(new skill { skillName = "C#" });
            //progLang.Add(new skill { skillName = "Java" });
            //progLang.Add(new skill { skillName = "C" });
            //progLang.Add(new skill { skillName = "Oracle SQL" });
/*
            SkillSection ProgLanguages = new SkillSection()
            {
                new Skill() {skillName = "C++"},
                new Skill() {skillName = "C#"},
                new Skill() {skillName = "Java"},
                new Skill() {skillName = "C#"}
            };
            ProgLanguages.sectionTitle = "Programming Languages";

            //List<skill> Lang = new List<skill>();
            //Lang.Add(new skill { skillName = "English" });
            //Lang.Add(new skill { skillName = "Spanish" });

            SkillSection languages = new SkillSection()
            {
                new Skill() {skillName = "English"},
                new Skill() {skillName = "Spanish"}
            };
            languages.sectionTitle = "Languages";

            skillSections = new List<SkillSection>()
            {
                ProgLanguages,
                languages
            };*/
        }
        private List<WorkTile> jobs = new List<WorkTile>();
        private List<EducationTile> education = new List<EducationTile>();
        private List<SkillSection> skillSections = new List<SkillSection>();

        private string aboutText = "I am currently a Junior at the University of Florida majoring in Computer Engineering. I have experience developing software in the .NET environment and developing database software. I am looking to expand my hardware and software knowledge while providing value to an organization using my current skills and ability to adapt and learn quickly.";
        public List<WorkTile> Jobs { get { return jobs; } }
        public List<EducationTile> Education { get { return education; } }
        public string AboutText { get { return aboutText; } }

        public List<SkillSection> Skills { get { return skillSections; } }


    }
}
