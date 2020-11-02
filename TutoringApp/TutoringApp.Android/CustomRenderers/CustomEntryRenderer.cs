using System;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TutoringApp.Views.CustomRenders;
using CustomRenderers.Droid;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace CustomRenderers.Droid
{
    [Obsolete]
    class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Android.Graphics.Drawables.GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);

                //ADD SUPPORT FOR NUMERIC KEYBOARD
                if ((e.NewElement as CustomEntry).Keyboard == Keyboard.Numeric)
                {
                    Control.SetRawInputType(InputTypes.ClassNumber);
                }
            }
        }
    }

}
