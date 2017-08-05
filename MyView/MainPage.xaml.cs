using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ImageProvider m_Provider;

        int m_CurrentImage;
        Image[] m_Images;

        public MainPage()
        {
            this.InitializeComponent();

            m_Provider = new ImageProvider();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            m_Images = new Image[] { ImagePrimary, ImageSecondary };

            m_Provider.CycleTime = 3000;
            m_Provider.Start();
            m_Provider.OnImageCycled += OnImageCycled;
        }

        void OnImageCycled(UnsplashImage image)
        {
            int oldImage = m_CurrentImage;
            m_CurrentImage = (++m_CurrentImage % 2);

            m_Images[oldImage].Opacity = 0;
            m_Images[m_CurrentImage].Opacity = 1;
        }
    }
}
