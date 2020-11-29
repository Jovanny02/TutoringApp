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
    public class HelpVM 

    {
       public ObservableCollection<QA> QAview { get; set; }

        public HelpVM() 
        { 
            QAview= new ObservableCollection<QA>
                    {
                        new QA { Questions = "Do i need to be a student at UF to use this app?", Answers = "Yes since all the help provided relates to courses at UF" },
                        new QA { Questions = "Will i get a refund if i cancel a class? ", Answers = "No, you will be charged a non refundable fee at the time of the reservation " },
                        new QA {Questions= "Are these meeting available in person?", Answers= "For the moment we only provide online meetings"}

                    };

        }
            

            private bool IsValidUri(String uri)
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
      

