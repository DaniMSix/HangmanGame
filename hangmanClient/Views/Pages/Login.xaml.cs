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
        private string Language;

        public Login()
        {
            InitializeComponent();
        }

        public Login(string language)
        {
            InitializeComponent();
            Console.WriteLine("-------LOGIN----------");
            Console.WriteLine("language "+ language);
            SetLanguage(language);
            Language = language;
        }

        private void SetLanguage(string language)
        {
            var culture = new CultureInfo(language == "Ingles" ? "en" : "");
            Thread.CurrentThread.CurrentUICulture = culture;

            // Actualizar etiquetas y textos en la interfaz de usuario
            lbUser.Content = Properties.Resources.lbUser;
            lbPassword.Content = Properties.Resources.lbPassword;
            btnLogin.Content = Properties.Resources.btnLogin;
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

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                    {
                        imgLabelErrorUser.Visibility = Visibility.Visible;
                        imgLabelErrorPassword.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(username))
                        {
                            imgLabelErrorUser.Visibility = Visibility.Visible;
                            imgLabelErrorPassword.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgLabelErrorUser.Visibility = Visibility.Collapsed;
                            imgLabelErrorPassword.Visibility = Visibility.Visible;
                        }
                    }

                    txtLoadingLabel.Visibility = Visibility.Collapsed;
                    txtLoadingDots.Visibility = Visibility.Hidden;
                    brdGrayBackground.Visibility = Visibility.Visible;
                    frMessage.Visibility = Visibility.Visible;
                    var warningPage = new PageWarning(Properties.Resources.lbTitleWarning, Properties.Resources.tbMessageWarning);
                    frMessage.Content = warningPage;
                    warningPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed; 
                        brdGrayBackground.Visibility = Visibility.Collapsed; 
                        frMessage.Visibility = Visibility.Collapsed;    
                    };
                    return;
                }

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

                    brdGrayBackground.Visibility = Visibility.Visible;

                    var successPage = new PageSuccess(Properties.Resources.lbTitleLogin, Properties.Resources.lbLoginMessage);

                    successPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed; 
                        brdGrayBackground.Visibility = Visibility.Collapsed; 
                        Console.WriteLine("ANTES DE PAGEHOME");
                        Console.WriteLine("Language " + Language);
                        var home = new PageHome(activePlayer, Language);
                        NavigationService?.Navigate(home);

                    };
                    frMessage.Content = successPage;
                }
                else
                {
                    txtLoadingLabel.Visibility = Visibility.Collapsed;
                    txtLoadingDots.Visibility = Visibility.Hidden;
                    brdGrayBackground.Visibility = Visibility.Visible;
                    frMessage.Visibility = Visibility.Visible;
                    var errorPage = new PageError(Properties.Resources.lbTitleErrorCredentials, Properties.Resources.tbMessageErrorCredentials);
                    frMessage.Content = errorPage;
                    errorPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed;
                        brdGrayBackground.Visibility = Visibility.Collapsed;
                        frMessage.Visibility = Visibility.Collapsed;
                        imgLabelErrorUser.Visibility = Visibility.Collapsed;
                        imgLabelErrorPassword.Visibility = Visibility.Collapsed;
                        frMessage.Visibility = Visibility.Collapsed;
                    };
                    return;
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

        private void TxtUserTextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUser.Text.Length > 12)
            {
                txtUser.Text = txtUser.Text.Substring(0, 12);
                txtUser.CaretIndex = txtUser.Text.Length; 
            }
        }

        private void PsdPassworPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox != null && passwordBox.Password.Length > 15)
            {
                passwordBox.Password = passwordBox.Password.Substring(0, 15);
            }
        }

    }
}
