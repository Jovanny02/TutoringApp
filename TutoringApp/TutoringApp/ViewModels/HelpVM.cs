using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using TutoringApp.Views;
using System.Diagnostics;
using Xamarin.Essentials;
using TutoringApp.Models;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace TutoringApp.ViewModels
{
    class HelpVM 

    {
       public List<QAGroups> QAview = new List<QAGroups>
                    {
                        new QAGroups("FAQ Student", "FAQS")
                            {
                                 new QA { Questions = "Do i need to be a student to be a student at UF to use the app", Answers = "Yes these course are only available at UF and this app only support the university" }
                            },


                        new QAGroups("FAQ tutors ", "FAQT")
                        {
                            new QA { Questions = "Will i get refund if i cancel a session", Answers = "No, if u cancel a charge in your own violation you will not get payed" }
                        }


                    };
        

            

            private Boolean IsValidUri(String uri)
            {
                try
                {
                    new Uri(uri);
                    return true;
                }
                catch
                {
                    return false;
                }
            }


            public ICommand ClickCommand => new Command<string>(async (url) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        if (!url.Trim().StartsWith("http", StringComparison.OrdinalIgnoreCase))
                        {
                            url = "http://" + url;
                        }
                        if (IsValidUri(url))
                        {
                            await Browser.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            });


         }

   }
      

