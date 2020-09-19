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
        NativeRatingView myView;

        CompositionMaskBrush brushSelected;
        CompositionMaskBrush brushUnselected;

        public NativeRatingViewRenderer()
        {

        }

        private void DrawRating(object sender, object e)
        {
            for (int i = 0; i < Element.GetMaxRating; i++)
            {
                if (i < Element.RateNumber)
                {
                    ratings[i].Brush = brushSelected;
                }
                else
                {
                    ratings[i].Brush = brushUnselected;
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
               // Window.Current.SizeChanged += Current_SizeChanged;
                // Configure the control and subscribe to event handlers
            }

        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            InitializeView(myView);
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            myView = (NativeRatingView)sender; //initializing our view after its setup, so it's not null
            InitializeView(myView);
            myView.SizeChanged -= OnSizeChanged;
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e); //we don't check the RatingNumber prop because we draw it once and
                                                      //just change colors accordingly for this task

            if (e.PropertyName == NativeRatingView.WidthProperty.PropertyName) // alternatively we can use the Window SizeChangedEvent
            {                                                                 // to reposition our view contents
                if(myView != null)
                {
                    InitializeView(myView);
                }
               
            }
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

            brushSelected = CreateMaskBrush((int)tickWidth, (int)tickHeight, Element.RatingColor.ToWindowsColor());
            brushUnselected = CreateMaskBrush((int)tickWidth, (int)tickHeight, Windows.UI.Color.FromArgb(80, 120, 117, 108));

            for (int i = 0; i < view.GetMaxRating; i++)
            {
                tick = compositor.CreateSpriteVisual();              
                tick.Size = new Vector2(tickWidth, tickHeight);
                tick.Offset = new Vector3(space, yOffset, 0);
                space += tickWidth + (float)margin;
                root.Children.InsertAtTop(tick);
                ratings.Add(tick);
            }
        }

        private CompositionMaskBrush CreateMaskBrush(int width, int height, Windows.UI.Color rateColor)
        {
            var _maskBrush = compositor.CreateMaskBrush();
            LoadedImageSurface loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/CircleMask.png"), new Windows.Foundation.Size(width, height));
            _maskBrush.Source = compositor.CreateColorBrush(rateColor);
            _maskBrush.Mask = compositor.CreateSurfaceBrush(loadedSurface);
            return _maskBrush;
        }
    }
}
