using Xamarin.Forms;
using Foundation;
using UIKit;
using CustomRenderers.ios;
using Xamarin.Forms.Platform.iOS;
using TutoringApp.Views.CustomRenders;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace CustomRenderers.ios
{
    class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;             

            }
        }
    }
}