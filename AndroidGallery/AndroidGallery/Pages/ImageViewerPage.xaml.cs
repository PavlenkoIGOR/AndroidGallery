using AndroidGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidGallery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewerPage : ContentPage
    {
        ImgData pictureInfo;
        public ImageViewerPage(ImgData picture)
        {
            pictureInfo = picture;
            InitializeComponent();
            this.BindingContext = pictureInfo;
        }
    }
}