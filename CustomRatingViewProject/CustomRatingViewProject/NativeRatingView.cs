using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomRatingViewProject
{
    public class NativeRatingView : View
    {
        private const int maxRateNum = 5;
        private const int minRateNum = 1;


        public static readonly BindableProperty RateNumberProperty = BindableProperty.Create("RateNumber", typeof(int), typeof(NativeRatingView), maxRateNum);
        public static readonly BindableProperty RatingColorProperty = BindableProperty.Create("RatingColor", typeof(Color), typeof(NativeRatingView), Color.Red);

        public int GetMaxRating { get => maxRateNum; }
        public int GetMinRating { get => minRateNum; }

        public int RateNumber
        {
            get => (int)GetValue(RateNumberProperty);
            set => SetValue(RateNumberProperty, value);
        }

        public Color RatingColor
        {
            get => (Color)GetValue(RatingColorProperty);
            set => SetValue(RateNumberProperty, value);
        }
    }
}
