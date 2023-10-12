using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidGallery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        string _PinCode;
        public RegPage()
        {
            InitializeComponent();
            _PinCode = Preferences.Get("Password", String.Empty); //Получает значение по указанному ключу.
            if (_PinCode != string.Empty)
            {
                HeaderLabel.Text = "Введите 4-х значный PIN-код для регистации:";
            }
        }

        private void WelcomeMethod(object sender, EventArgs e)
        {
            string enterPwd = Password.Text;
            if (_PinCode == string.Empty)
            {
                Preferences.Set("Password", enterPwd); //Задает значение для указанного ключа.
                
            }
            else
            {
                if (_PinCode != Password.Text)
                {
                    WarningMessageLabel.Text = "Неверный ПИН-код";
                    return;
                }
            }
            Navigation.PushAsync(new GalleryPage());
        }

        /// <summary>
        /// Метод, который выводит предупреждение если пин-код не соответствует 4 символам
        /// </summary>
        private void PasswordChanging(object sender, TextChangedEventArgs e)
        {
            if (Password.Text.Length != 4 )
            {
                WarningMessageLabel.Text = "Введите 4-значный ПИН-код";
                EnterButton.IsEnabled = false;
            }
            else
            {
                EnterButton.IsEnabled = true;
                WarningMessageLabel.Text = string.Empty;
            }
        }
    }
}
