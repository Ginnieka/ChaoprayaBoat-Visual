using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ChaoprayaBoat.iOS
{
    internal static class ImageSourceExtensions
    {
        internal static IImageSourceHandler GetHandler(this ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }
    }
}
