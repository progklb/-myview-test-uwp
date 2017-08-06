using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Animations;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using MyView.Common;

namespace MyView
{
    /// <summary>
    /// The main interface of the app. This displays cycling images to the user.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region PROPERTIES
        private int TransitionDuration { get; set; } = 2000;
        #endregion


        #region VARIABLES
        /// Provides images for display on this page
        SlideshowController m_Slideshow;

        /// A cache of the two image views for displaying images to the user
        Image[] m_Images;
        /// The index of the currently display image view.
        int m_CurrentImageIdx;
        #endregion


        #region CONSTRUCTOR
        public MainPage()
        {
            this.InitializeComponent();

            m_Slideshow = new SlideshowController();
        }
        #endregion


        #region INHERITED METHODS
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Cache elements
            m_Images = new Image[] { ImagePrimary, ImageSecondary };
            ImagePrimary.Opacity = 0;
            ImageSecondary.Opacity = 0;

            // Start image provider
            m_Slideshow.Start();
            m_Slideshow.OnImageCycled += OnImageCycled;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            m_Slideshow.Stop();
        }
        #endregion


        #region IMAGE CYCLING
        /// <summary>
        /// Displays the provided image by crossfading the current and new image.
        /// </summary>
        /// <param name="image"></param>
        void OnImageCycled(UnsplashImage image)
        {
            int oldImage = m_CurrentImageIdx;
            m_CurrentImageIdx = (++m_CurrentImageIdx % 2);

            Fade(m_Images[oldImage], -1).ConfigureAwait(false);
            Fade(m_Images[m_CurrentImageIdx], 1).ConfigureAwait(false);

            Debug.WriteLine("Changing to image: {0}", image != null ? image.ImagePath : "null");
        }

        /// <summary>
        /// Fades the opacity of the provided image to fully opaque or fully transparent. 
        /// </summary>
        /// <param name="image">Image to adjust opacity of.</param>
        /// <param name="fadeDir">Positive values fade in. Negative values fade out.</param>
        /// <returns></returns>
        async Task Fade(Image image, int fadeDir)
        {
            double currentOpacity = image.Opacity;
            double targetOpacity = fadeDir > 0 ? 1 : 0;

            int timeStep = 1000 / 30;
            double opacityStep = ((double)1 / 30) / (TransitionDuration / 1000);

            while (currentOpacity != targetOpacity)
            {
                currentOpacity += fadeDir > 0 ? opacityStep : -opacityStep;
                currentOpacity = Clamp(currentOpacity, 0, 1);
                image.Opacity = currentOpacity;

                await Task.Delay(timeStep);
            }
        }

        /// <summary>
        /// Clamps a value between the min and maximum specified values, returning the clamp value.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="min">Minimum allowable value.</param>
        /// <param name="max">Maximum allowable value.</param>
        /// <returns></returns>
        double Clamp(double value, double min, double max)
        {
            value = Math.Max(value, min);
            value = Math.Min(value, max);
            return value;
        }
        #endregion
    }
}
