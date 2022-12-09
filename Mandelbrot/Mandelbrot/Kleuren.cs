using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
    public static class KleurOmzetten
    {
        public static (int rood, int groen, int blauw) hslNaarRgb(double hue, double saturation, double brightness) // Deze functie zet hue, saturation en brightness om in rgb waardes. De formules zijn gevonden op https://www.rapidtables.com/convert/color/hsl-to-rgb.html#:~:text=HSL%20to%20RGB%20conversion%20formula%20When%200%20%E2%89%A4,1%7C%29%20m%3D%20L-%20C%2F2%20%28R%2CG%2CB%29%20%3D%20%28%28R%27%2Bm%29%C3%97255%2C%20%28G%27%2Bm%29%C3%97255%2C%28B%27%2Bm%29%C3%97255%29.
        {
            double c = (1 - Math.Abs(2 * brightness - 1)) * saturation;
            double x = c * (1 - Math.Abs(hue / 60 % 2 - 1));
            double m = brightness - c / 2;

            double rood = 0, groen = 0, blauw = 0;

            if (0 <= hue && hue < 60)
            {
                rood = (c + m) * 255;
                groen = (x + m) * 255;
                blauw = (0 + m) * 255;
            }
            else if (60 <= hue && hue < 120) 
            {
                rood = (x + m) * 255;
                groen = (c + m) * 255;
                blauw = (0 + m) * 255;
            }
            else if (120 <= hue && hue < 180)
            {
                rood = (0 + m) * 255;
                groen = (c + m) * 255;
                blauw = (x + m) * 255;
            }
            else if (180 <= hue && hue < 240)
            {
                rood = (0 + m) * 255;
                groen = (x + m) * 255;
                blauw = (c + m) * 255;
            }
            else if (240 <= hue && hue < 300)
            {
                rood = (x + m) * 255;
                groen = (0 + m) * 255;
                blauw = (c + m) * 255;
            }
            else if (300 <= hue && hue < 360)
            {
                rood = (c + m) * 255;
                groen = (0 + m) * 255;
                blauw = (x + m) * 255;
            }

            return (Convert.ToInt32(rood), Convert.ToInt32(groen), Convert.ToInt32(blauw));
        }
    }
}