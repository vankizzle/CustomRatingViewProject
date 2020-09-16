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

        protected override void OnElementChanged(ElementChangedEventArgs<NativeRatingView> e)
        {
            base.OnElementChanged(e);
        }

        public override void DrawLayer(CALayer layer, CGContext context)
        {
            base.DrawLayer(layer, context);
            //set up drawing attributes

          

            var height = layer.Bounds.Height;
            var width = layer.Bounds.Width;

            var radius = (float)((height / 2) * 0.8);
            var margin = ((width / Element.GetMaxRating) - (2 * radius)) / 2;
            //var margin = ((width - (Element.GetMaxRating * radius)) / 5);
            float space = (float)margin;
            var y = height / 2;

            //create geometry
            // Draw background circle
            CGPath pathRating = new CGPath();
            CGPath pathUnRated = new CGPath();
            pathRating.AddArc(radius + space, y, radius, 0, 2.0f * (float)Math.PI, true);

            UIColor ratingColor = Element.RatingColor.ToUIColor();
            ratingColor.SetStroke();
            ratingColor.SetFill();

            //to test
            //int cnt;
            //for(cnt = 1; cnt <= Element.RateNumber; cnt++)
            //{
            //    CGPath pathRating1 = new CGPath();
            //    pathRating1.AddArc(radius + space, y, radius, 0, 2.0f * (float)Math.PI, true);
            //    space += radius * 2 + ((float)margin * 2);
            //    context.AddPath(pathRating);
            //    context.DrawPath(CGPathDrawingMode.Stroke);
            //}

            //UIColor.Gray.SetStroke();
            
            //for (; cnt <= Element.GetMaxRating; cnt++)
            //{
            //    CGPath pathUnRated1 = new CGPath();
            //    pathUnRated1.AddArc(radius + space, y, radius, 0, 2.0f * (float)Math.PI, true);
            //    space += radius * 2 + ((float)margin * 2);
            //    context.AddPath(pathUnRated1);
            //    context.DrawPath(CGPathDrawingMode.Stroke);
            //}

            for (int i = 1; i <= Element.GetMaxRating; i++)
            {
                if (i <= Element.RateNumber)
                {
                    pathRating.AddArc(radius + space, y, radius, 0, 2.0f * (float)Math.PI, true);
                }
                else
                {
                    UIColor.Gray.SetStroke();
                    pathUnRated.AddArc(radius + space, y, radius, 0, 2.0f * (float)Math.PI, true);
                }

                space += radius * 2 + ((float)margin * 2);
            }

            context.AddPath(pathRating);
            context.DrawPath(CGPathDrawingMode.Stroke);

            context.AddPath(pathUnRated);
            context.DrawPath(CGPathDrawingMode.Stroke);

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