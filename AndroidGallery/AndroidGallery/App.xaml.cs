using AndroidGallery.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidGallery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RegPage());//new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
