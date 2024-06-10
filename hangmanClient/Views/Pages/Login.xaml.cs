using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Views.Utils;

namespace Views.Pages
{
    public partial class Login : Page
    {
        private SRIPlayerManagement.IPlayerManagement client = new SRIPlayerManagement.PlayerManagementClient();
        private bool isPlaying = false;
        private string username;
        private string password;
        Domain.DTOPlayer newPlayeraActive;

        public Login()
        {
            InitializeComponent();
        }

        private void ClicPlayMusicButtonClick(object sender, RoutedEventArgs e)
        {
            btnMusic.Opacity = 0.2;
            if (!isPlaying)
            {
                isPlaying = true;
            }
            else
            {
                isPlaying = false;
            }
        }

        private async void ClickLogin(object sender, RoutedEventArgs e)
        {
            btnLogin.Visibility = Visibility.Hidden;
            txtLoadingLabel.Visibility = Visibility.Visible;
            txtLoadingDots.Visibility = Visibility.Visible;
            try
            {
                username = txtUser.Text;
                password = psdPassword.Password;

                var playerLogin = await Task.Run(() => client.AuthenticateLogin(username, password));

                if (playerLogin != null)
                {
                    newPlayeraActive = new Domain.DTOPlayer()
                    {
                        IdPlayer = playerLogin.IdPlayer,
                        Username = playerLogin.Username,
                        Name = playerLogin.Name,
                        Birthdate = playerLogin.Birthdate,
                        Phonenumber = playerLogin.Phonenumber,
                        Email = playerLogin.Email,
                        Score = playerLogin.Score
                    };

                    txtLoadingLabel.Visibility = Visibility.Collapsed;
                    txtLoadingDots.Visibility = Visibility.Hidden;
                    // Mostrar el fondo gris semitransparente
                    brdGrayBackground.Visibility = Visibility.Visible;

                    // Crear una instancia de SuccessPage con el título y contenido deseado
                    var successPage = new SuccessPage("¡Éxito!", "Inicio de sesión exitoso.");

                    // Suscribirse al evento MessageClosed de SuccessPage
                    successPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed; // Ocultar la animación de carga
                        brdGrayBackground.Visibility = Visibility.Collapsed; // Ocultar el fondo gris
                        var home = new Home(newPlayeraActive);
                        if (NavigationService != null)
                        {
                            NavigationService.Navigate(home);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error al intentar mostrar la ventana");
                        }
                    };


                    // Asignar SuccessPage al contenido del Frame
                    frMessage.Content = successPage;
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas.", "Inicio de sesión fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error durante la autenticación: " + ex.Message);
            }
            finally
            {
            }
        }

        private void TxtClicRegister(object sender, MouseButtonEventArgs e)
        {
            // Crear una instancia de la página de registro (suponiendo que se llama RegisterPage)
            var registerPage = new CreateProfile();

            // Navegar a la página de registro
            NavigationService.Navigate(registerPage);
        }

    }
}
