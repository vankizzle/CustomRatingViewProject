using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CustomRatingViewProject;
using CustomRatingViewProject.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NativeRatingView), typeof(NativeRatingViewRenderer))]
namespace CustomRatingViewProject.Droid
{
    public class NativeRatingViewRenderer : ViewRenderer<NativeRatingView, Android.Views.View>
    {
        public NativeRatingViewRenderer(Context context) : base(context)
        {
            SetWillNotDraw(false);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NativeRatingView> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == NativeRatingView.RateNumberProperty.PropertyName ||
                e.PropertyName == NativeRatingView.RatingColorProperty.PropertyName)
                this.Invalidate();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            DrawRatings(canvas);
        }

        private void DrawRatings(Canvas c)
        {

            var SelectedPaint = new Paint
            {
                Color = Element.RatingColor.ToAndroid(),
                StrokeWidth = 1,
                AntiAlias = false,

            };

            var DeselectedPaint = new Paint
            {
                Color = Android.Graphics.Color.Black,
                StrokeWidth = 3,
                AntiAlias = false,
            };

            DeselectedPaint.SetStyle(Paint.Style.Stroke);

            var height = Height;
            var width = Width;

            var radius = (float)((height / 2) * 0.8);
            var margin = ((width / Element.GetMaxRating) - (2 * radius)) / 2;
            //var margin = ((width - (Element.GetMaxRating * radius)) / 5);
            float space = (float)margin;

            var y = height / 2;

            for (int i = 1; i <= Element.GetMaxRating; i++)
            {
                if (i <= Element.RateNumber)
                {
                    c.DrawCircle(radius + space, y, radius, SelectedPaint);
                    c.DrawCircle(radius + space, y, radius, DeselectedPaint);
                }
                else
                {
                    c.DrawCircle(radius + space, y, radius, DeselectedPaint);
                }

                space += radius * 2 + ((float)margin * 2);
            }
        }
    }
}