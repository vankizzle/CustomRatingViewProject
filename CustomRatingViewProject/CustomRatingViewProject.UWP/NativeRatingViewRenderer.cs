using CustomRatingViewProject;
using CustomRatingViewProject.UWP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(NativeRatingView), typeof(NativeRatingViewRenderer))]

namespace CustomRatingViewProject.UWP
{
    public class NativeRatingViewRenderer : ViewRenderer<NativeRatingView, FrameworkElement>
    {
        SpriteVisual tick;
        Compositor compositor;
        ContainerVisual root;
        List<SpriteVisual> ratings;

        public NativeRatingViewRenderer()
        {

        }

        private void DrawRating(object sender, object e)
        {
            for (int i = 0; i < Element.GetMaxRating; i++)
            {
                if (i < Element.RateNumber)
                {
                   ratings[i].Brush = compositor.CreateColorBrush(Element.RatingColor.ToWindowsColor());
                }
                else
                {
                    ratings[i].Brush = compositor.CreateColorBrush(Windows.UI.Color.FromArgb(80, 120, 117, 108));
                }
            }

        }



        protected override void OnElementChanged(ElementChangedEventArgs<NativeRatingView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                Windows.UI.Xaml.Media.CompositionTarget.Rendering -= new EventHandler<object>(DrawRating);
            }

            if (e.NewElement != null)
            {
                var myview = (NativeRatingView)e.NewElement;
                if (Control == null)
                {
                    Viewbox myViewBox = new Viewbox();
                    SetNativeControl(myViewBox);
                    //    // Instantiate the native control and assign it to the Control property with
                    //    // the SetNativeControl method
                }
             
                myview.SizeChanged += OnSizeChanged;
                // Configure the control and subscribe to event handlers
            }

        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            var ratingView = (NativeRatingView)sender;
            InitializeView(ratingView);
            ratingView.SizeChanged -= OnSizeChanged;
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private void InitializeView(NativeRatingView view)
        {
            Windows.UI.Xaml.Media.CompositionTarget.Rendering += new EventHandler<object>(DrawRating);
            ratings = new List<SpriteVisual>();

             var hostVisual = ElementCompositionPreview.GetElementVisual(this);
            root = hostVisual.Compositor.CreateContainerVisual();
            ElementCompositionPreview.SetElementChildVisual(this, root);
            compositor = root.Compositor;

            var height = view.Height;
            var width = view.Width;
            var spaceForTicks = width * 0.6;
            var spaceForMargins = width - spaceForTicks;

            var tickWidth = (float)(spaceForTicks / view.GetMaxRating);
            var tickHeight = (float)(height * 0.6);

            var margin = ((spaceForMargins /(Element.GetMaxRating + 1)));

            float space = (float)margin;
            float yOffset = (((float)height - tickHeight) / 2);

            for (int i = 0; i < view.GetMaxRating; i++)
            {
                tick = compositor.CreateSpriteVisual();
                tick.Size = new Vector2(tickWidth, tickHeight);
                tick.Brush = compositor.CreateColorBrush(Element.RatingColor.ToWindowsColor());
                tick.Offset = new Vector3(space, yOffset, 0);
                space += tickWidth + (float)margin;
                root.Children.InsertAtTop(tick);
                ratings.Add(tick);
            }
        }
    }
}
