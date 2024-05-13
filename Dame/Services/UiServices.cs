using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Dame.Services
{
    static class UiServices
    {
        public static BitmapImage HoverImage(string image_path)
        {
            string new_path = "/assets/" + Path.GetFileName(image_path).Replace(".png","_hover.png");
            return new BitmapImage(new Uri(new_path, UriKind.Relative));
        }
        public static BitmapImage DefaultImage(string image_path)
        {
            string new_path = "/assets/" + Path.GetFileName(image_path).Replace("_hover.png", ".png");
            return new BitmapImage(new Uri(new_path, UriKind.Relative));
        }

    }
}
