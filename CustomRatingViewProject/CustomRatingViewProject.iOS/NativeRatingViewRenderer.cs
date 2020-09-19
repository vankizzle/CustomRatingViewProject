using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using CustomRatingViewProject;
using CustomRatingViewProject.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NativeRatingView), typeof(NativeRatingViewRenderer))]
namespace CustomRatingViewProject.iOS
{
    public class NativeRatingViewRenderer : ViewRenderer<NativeRatingView, UIView>
    {
        UIView myview;
        protected override void OnElementChanged(ElementChangedEventArgs<NativeRatingView> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                myview = new UIView() { 
                    BackgroundColor = Element.BackgroundColor.ToUIColor(),
                    AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
                };
                SetNativeControl(myview);
            }
        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var height = rect.Height;
            var width = rect.Width;

            var radius = (float)((height / 2) * 0.8);
            var margin = ((width / Element.GetMaxRating) - (2 * radius)) / 2;

            float space = (float)margin;
            var y = height / 2;



            using (var context = UIGraphics.GetCurrentContext())
            {


                UIColor ratingColor = Element.RatingColor.ToUIColor();
                ratingColor.SetStroke();
                ratingColor.SetFill();

                //to test
                int cnt;
                for (cnt = 1; cnt <= Element.RateNumber; cnt++)
                {
                    CGPath pathRating1 = new CGPath();
                    pathRating1.AddArc(radius + space, y, radius, 0, 2.0f * (float)Math.PI, true);
                    space += radius * 2 + ((float)margin * 2);
                    context.AddPath(pathRating1);
                    context.DrawPath(CGPathDrawingMode.FillStroke);
                }

                UIColor.Gray.SetStroke();
                UIColor.Gray.SetFill();
                for (; cnt <= Element.GetMaxRating; cnt++)
                {
                    CGPath pathUnRated1 = new CGPath();
                    pathUnRated1.AddArc(radius + space, y, radius, 0, 2.0f * (float)Math.PI, true);
                    space += radius * 2 + ((float)margin * 2);
                    context.AddPath(pathUnRated1);
                    context.DrawPath(CGPathDrawingMode.FillStroke);
                }
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == NativeRatingView.RateNumberProperty.PropertyName ||
               e.PropertyName == NativeRatingView.RatingColorProperty.PropertyName)
                this.SetNeedsDisplay();
        }
    }
}