using System;
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
        Domain.DTOPlayer activePlayer;
        private bool isPlaying = false;
        string language;
        private SoundHelper soundHelper;

        public Login()
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\retro-videogame.mp3");
            if (Thread.CurrentThread.CurrentCulture.Name == "en")
            {
                language = "Ingles";
                imgTitleEnglish.Visibility = Visibility.Visible;
                imgTtleSapanish.Visibility = Visibility.Collapsed;
            }
            else
            {
                language = "Español";
                imgTtleSapanish.Visibility = Visibility.Visible;
                imgTitleEnglish.Visibility = Visibility.Collapsed;
            }
        }

        private void PlayBackgroundMusic()
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\retro-videogame.mp3");
        }

        private async void ClickLogin(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");

            btnLogin.Visibility = Visibility.Hidden;
            txtLoadingLabel.Visibility = Visibility.Visible;
            txtLoadingDots.Visibility = Visibility.Visible;
            frMessage.Visibility = Visibility.Visible;
            
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
                    brdGrayBackground.Visibility = Visibility.Visible;
                    var warningPage = new PageWarning(Properties.Resources.lbTitleWarning, Properties.Resources.tbMessageWarning);
                    frMessage.Content = warningPage;
                    warningPage.MessageClosed += (s, args) =>
                    {

                        txtLoadingDots.Visibility = Visibility.Collapsed;
                        brdGrayBackground.Visibility = Visibility.Collapsed;
                        frMessage.Visibility = Visibility.Collapsed;
                        brdGrayBackground.Visibility = Visibility.Collapsed;
                    };
                    return;
                }
                SRIPlayerManagement.DTOPlayer playerLogin = null;

                client = new SRIPlayerManagement.PlayerManagementClient();

                try
                {
                    if (client.AuthenticateLogin(username, password) == null)
                    {
                        var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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
                    playerLogin = await Task.Run(() => client.AuthenticateLogin(username, password));
                }
                catch (System.ServiceModel.EndpointNotFoundException ex)
                {
                    var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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
                catch (Exception ex)
                {
                    brdGrayBackground.Visibility = Visibility.Visible;
                    var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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

                    brdGrayBackground.Visibility = Visibility.Visible;

                    var successPage = new PageSuccess(Properties.Resources.lbTitleLogin, Properties.Resources.lbLoginMessage);

                    successPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed;
                        brdGrayBackground.Visibility = Visibility.Collapsed;

                        var home = new PageHome(activePlayer, language);
                        NavigationService?.Navigate(home);

                    };
                    frMessage.Content = successPage;
                }
                else
                {
                    brdGrayBackground.Visibility = Visibility.Visible;
                    var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                brdGrayBackground.Visibility = Visibility.Visible;
                var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                brdGrayBackground.Visibility = Visibility.Visible;
                var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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
            }
            catch (TimeoutException ex)
            {
                brdGrayBackground.Visibility = Visibility.Visible;
                var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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
            }
            catch (Exception ex)
            {
                brdGrayBackground.Visibility = Visibility.Visible;
                var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
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
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var registerPage = new PageCreateProfile();
            NavigationService?.Navigate(registerPage);
        }

        private void BtnClickLanguage(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var selectLanguagePage = new PageSelectLanguage();
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

        private void PauseMusicClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PauseBackgroundMusic();
        }
    }
}
