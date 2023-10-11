using AndroidGallery.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Plugin.Media;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;

namespace AndroidGallery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        Image img;
        public ObservableCollection<ImgData> imgDatas; //= new List<ImgData>();
        public int count = 0;
        string path = @"/storage/emulated/0/DCIM/Camera/";
        public GalleryPage()
        {
            imgDatas = new ObservableCollection<ImgData>();
            InitializeComponent();
            //LookingFor.Clicked += (s, e) => FillGrid();
            FillGrid();
        }
        void FillGrid()
        {

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (var dir in dirInfo.GetFileSystemInfos("*.jpg"))
            {
               imgDatas.Add(new ImgData ( dir.Name, dir.FullName, dir.CreationTime ));
                count++;
            }
            pictures.ItemsSource = imgDatas;
            //DisplayAlert("Уведомление", String.Format("количество файлов {0} шт.",dirInfo.GetFiles().Length.ToString()), "ОK");
        }
        private void ViewPicrure(object sender, EventArgs e)
        {
            if (pictures.SelectedItem is null)
                return;

            Navigation.PushAsync(new ImageViewerPage(pictures.SelectedItem as ImgData));
        }

        /// <summary>
        /// Метод для использования камеры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void PhotoPlease(object sender, EventArgs e) //не рабочий
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                });

                // для примера сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
                //newFile.Dispose(); // Освободить ресурсы файла
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }


            
        }

        /// <summary>
        /// Метод для удаления изображения
        /// </summary>
        async void DeleteImage(object sender, EventArgs e)
        {
            if (pictures.SelectedItem is null)
                return;
            ImgData pic = pictures.SelectedItem as ImgData;
            var answer = await DisplayAlert("Внимание!", $"Удалить {pic.FileName}", "Да", "Нет");

            if (answer == false)
            {
                return;
            }

            try
            {
                if (File.Exists(pic.FilePath))
                {
                    File.Delete(pic.FilePath);
                }
                imgDatas.Remove(pic);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка!", ex.Message, "OK");
            }
        }
    }
}
////await Navigation.PushAsync(new myDeviceListPage());