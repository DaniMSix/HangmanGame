using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Views.Utils;

namespace Views.Pages
{
    public partial class Login : Page
    {
        private SRIPlayerManagement.IPlayerManagement client = new SRIPlayerManagement.PlayerManagementClient();
        private Domain.DTOPlayer activePlayer;
        private string language;

        public Login()
        {
            InitializeComponent();
            SetLanguage(Thread.CurrentThread.CurrentUICulture.Name);
        }


        public Login(string language)
        {
            InitializeComponent();
            this.language = language;
            SetLanguage(language);
            // No se carga aquí, se asume que los recursos se cargan en el archivo xaml
        }

        private void SetLanguage(string language)
        {
            if (language == "Español")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
        }

        private async void ClickLogin(object sender, RoutedEventArgs e)
        {
            btnLogin.Visibility = Visibility.Hidden;
            txtLoadingLabel.Visibility = Visibility.Visible;
            txtLoadingDots.Visibility = Visibility.Visible;

            try
            {
                string username = txtUser.Text;
                string password = psdPassword.Password;

                var playerLogin = await Task.Run(() => client.AuthenticateLogin(username, password));

                if (playerLogin != null)
                {
                    activePlayer = new Domain.DTOPlayer()
                    {
                        IdPlayer = playerLogin.IdPlayer,
                        Username = playerLogin.Username,
                        Name = playerLogin.Name,
                        Birthday = playerLogin.Birthdate,
                        Phonenumber = playerLogin.Phonenumber,
                        Email = playerLogin.Email,
                        Score = playerLogin.Score
                    };

                    txtLoadingLabel.Visibility = Visibility.Collapsed;
                    txtLoadingDots.Visibility = Visibility.Hidden;

                    // Mostrar el fondo gris semitransparente
                    brdGrayBackground.Visibility = Visibility.Visible;

                    // Crear una instancia de SuccessPage con el título y contenido deseado
                    var successPage = new PageSuccess(Properties.Resources.lbTitleLogin, Properties.Resources.lbLoginMessage);

                    // Suscribirse al evento MessageClosed de SuccessPage
                    successPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed; // Ocultar la animación de carga
                        brdGrayBackground.Visibility = Visibility.Collapsed; // Ocultar el fondo gris
                        var home = new PageHome(activePlayer, language);
                        NavigationService?.Navigate(home);
                    };

                    // Asignar SuccessPage al contenido del Frame
                    frMessage.Content = successPage;
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnLogin.Visibility = Visibility.Visible;
                txtLoadingLabel.Visibility = Visibility.Collapsed;
                txtLoadingDots.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtClicRegister(object sender, RoutedEventArgs e)
        {
            var registerPage = new PageCreateProfile();
            NavigationService?.Navigate(registerPage);
        }

        private void BtnClickLanguage(object sender, RoutedEventArgs e)
        {
            var selectLanguagePage = new PageSelectLanguage(frMessage, frHome);
            frMessage.Navigate(selectLanguagePage);
        }
    }
}
