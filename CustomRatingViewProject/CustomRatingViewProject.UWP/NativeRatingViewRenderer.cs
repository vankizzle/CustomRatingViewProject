using CustomRatingViewProject;
using CustomRatingViewProject.UWP;
using System;
using System.ComponentModel;
using System.Drawing;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(NativeRatingView), typeof(NativeRatingViewRenderer))]

namespace CustomRatingViewProject.UWP
{
    public class NativeRatingViewRenderer : ViewRenderer<NativeRatingView,FrameworkElement>
    {
   
        public NativeRatingViewRenderer()
        {
            
        }

        private void DrawRating(object sender, object e)
        {
            var a = ((RenderingEventArgs)e).RenderingTime;
           
        }

        

        protected override void OnElementChanged(ElementChangedEventArgs<NativeRatingView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                CompositionTarget.Rendering -= new EventHandler<object>(DrawRating);
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    
                    Grid myViewBox = new Grid();              
                    SetNativeControl(myViewBox);
                   
                    // Instantiate the native control and assign it to the Control property with
                    // the SetNativeControl method
                }
                CompositionTarget.Rendering += new EventHandler<object>(DrawRating);
                Control.Loaded += new RoutedEventHandler(ControlLoaded);
               
                // Configure the control and subscribe to event handlers
            }
            
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            InitializeView();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private void InitializeView()
        {
            var height = Control.Height;
            var width = Control.Width;
            Element.BackgroundColor = System.Drawing.Color.Black;
         
          
        }    
    }
}
