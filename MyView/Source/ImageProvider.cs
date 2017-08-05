using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyView
{
    class ImageProvider
    {
        public event Action<UnsplashImage> OnImageCycled = delegate { };

        public bool Running;
        public int CycleTime = 5000;

        public void Start()
        {
            Running = true;

            StartService();
        }

        public void Stop()
        {
            Running = false;
        }

        async Task StartService()
        {
            while (Running)
            {
                OnImageCycled(new UnsplashImage { ImagePath = "example1.jpg" });
                await Task.Delay(CycleTime);
                OnImageCycled(new UnsplashImage { ImagePath = "example2.jpg" });
                await Task.Delay(CycleTime);
            }
        }
    }
}
