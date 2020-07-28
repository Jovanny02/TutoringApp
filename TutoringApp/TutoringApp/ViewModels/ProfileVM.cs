using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TutoringApp.ViewModels
{
    class ProfileVM : BaseVM
    {
        public ProfileVM()
        {


            pictureSize = (DeviceDisplay.MainDisplayInfo.Width * 0.09);
            radius = pictureSize / 2;
            Console.WriteLine("Radius: " + radius + "  Size: " + pictureSize);

            AverageRating = 3.73365;
        }


        private Double pictureSize;
        public Double PictureSize
        {
            get { return pictureSize; }
        }

        private Double radius;
        public Double Radius
        {
            get { return radius; }
        }

        public Double AverageRating { get; set; }

        // public string ratingLabel { get { return (string.Format("{0:0.0}", Math.Truncate(AverageRating * 10) / 10) + "/5.0");  } }
        public string ratingLabel { get { return string.Format("{0:0.0}", Math.Truncate(AverageRating * 10) / 10); } }
        public string pictureSrc { get; private set; } = "user.png";
    }
}
