using Domain;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageCreateProfile : Page
    {
        SRIPlayerManagement.PlayerManagementClient client = new SRIPlayerManagement.PlayerManagementClient();
        private DTOPlayer activePlayer;
        private System.Windows.Controls.Frame homeFrame;
        string language;
        private SoundHelper soundHelper;

        public PageCreateProfile()
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
        }

        public PageCreateProfile(DTOPlayer activePlayer, System.Windows.Controls.Frame homeFrame, string language)
        {
            InitializeComponent();
            this.activePlayer = activePlayer;
            btnCreate.Content = Properties.Resources.btnModify;
            lbTitle.Content = Properties.Resources.lbTitleModify;
            txtEmail.IsEnabled = false;
            imgBlockLabel.Visibility = Visibility.Visible;
            ShowPersonalInformation();
            this.homeFrame = homeFrame;
            soundHelper = new SoundHelper();
        }


        public void ShowPersonalInformation()
        {
            txtUser.Text = activePlayer.Username;
            txtName.Text = activePlayer.Name;
            txtPhone.Text = activePlayer.Phonenumber;
            txtEmail.Text = activePlayer.Email;
            txtDateOfBirthday.Text = activePlayer.Birthday.ToString("dd-MM-yyyy");
        }

        private bool ValidatePasswords()
        {
            if (string.IsNullOrEmpty(psdPassword.Password))
            {
                var warningPage = new PageWarning(Properties.Resources.lbTitleWarning, Properties.Resources.lbPasswordEmpty);
                frMessage.Content = warningPage;
                brdGrayBackground.Visibility = Visibility.Visible;
                warningPage.MessageClosed += (s, args) =>
                {
                    txtLoadingDots.Visibility = Visibility.Collapsed;
                    brdGrayBackground.Visibility = Visibility.Collapsed;
                    frMessage.Visibility = Visibility.Collapsed;
                };
                return false;
            }

            if (psdPassword.Password != psdPasswordConfirm.Password)
            {
                
                var warningPage = new PageWarning(Properties.Resources.lbTitleWarning, Properties.Resources.lbPasswordDontMatch);
                frMessage.Content = warningPage;
                brdGrayBackground.Visibility = Visibility.Visible;
                warningPage.MessageClosed += (s, args) =>
                {
                    txtLoadingDots.Visibility = Visibility.Collapsed;
                    brdGrayBackground.Visibility = Visibility.Collapsed;
                    frMessage.Visibility = Visibility.Collapsed;
                };
                return false;
            }
            return true;
        }

        private void BtnClickSend(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            if (ValidateFields() && ValidatePasswords())
            {
                try
                {
                    var newPlayerActive = new SRIPlayerManagement.DTOPlayer()
                    {
                        Username = txtUser.Text,
                        Name = txtName.Text,
                        Phonenumber = txtPhone.Text,
                        Email = txtEmail.Text,
                        Birthdate = DateTime.ParseExact(txtDateOfBirthday.Text, "dd-MM-yyyy", null),
                        Password = psdPassword.Password
                    };

                    if (activePlayer == null)
                    {
                        if (!ValidateUserAndEmail())
                        {
                            var warningPage = new PageWarning(Properties.Resources.lbTitleExistsDate, Properties.Resources.lbUserEmailExits);
                            frMessage.Content = warningPage;
                            brdGrayBackground.Visibility = Visibility.Visible;
                            warningPage.MessageClosed += (s, args) =>
                            {
                                txtLoadingDots.Visibility = Visibility.Collapsed;
                                brdGrayBackground.Visibility = Visibility.Collapsed;
                                frMessage.Visibility = Visibility.Collapsed;
                            };

                            return; 
                        }

                        client.RegisterPlayer(newPlayerActive);
                        var successPage = new PageSuccess(Properties.Resources.lbTitleRegisterSucces, Properties.Resources.lbRegisterSuccess);
                        frMessage.Content = successPage;
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
                        var newPlayerUpdate = new SRIPlayerManagement.DTOPlayer()
                        {
                            IdPlayer = activePlayer.IdPlayer,
                            Username = txtUser.Text,
                            Name = txtName.Text,
                            Phonenumber = txtPhone.Text,
                            Birthdate = DateTime.ParseExact(txtDateOfBirthday.Text, "dd-MM-yyyy", null),
                        };

                        if (client.UpdateFullProfile(newPlayerUpdate))
                        {
                            var errorPage = new PageError(Properties.Resources.lbTitleErrorServer, Properties.Resources.lbMessageErrorServer);
                            frMessage.Content = errorPage;
                            errorPage.MessageClosed += (s, args) =>
                            {
                                txtLoadingDots.Visibility = Visibility.Collapsed;
                                brdGrayBackground.Visibility = Visibility.Collapsed;
                                frMessage.Visibility = Visibility.Collapsed;
                            };
                        }

                        client.UpdateFullProfile(newPlayerUpdate);

                        var successPage = new PageSuccess(Properties.Resources.lbTitleModifySuccess, Properties.Resources.lbModifySuccess);

                        successPage.MessageClosed += (s, args) =>
                        {
                            txtLoadingDots.Visibility = Visibility.Collapsed;
                            brdGrayBackground.Visibility = Visibility.Collapsed;
                            var home = new PageHome(activePlayer, language);
                            NavigationService?.Navigate(home);

                        };
                        frMessage.Content = successPage;
                    }
                }
                catch (FormatException ex)
                {
                    var warningPage = new PageWarning(Properties.Resources.lbError, Properties.Resources.lbDateError);
                    frMessage.Content = warningPage;
                    brdGrayBackground.Visibility = Visibility.Visible;
                    warningPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed;
                        brdGrayBackground.Visibility = Visibility.Collapsed;
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
                    };
                }
            }
        }

        private bool ValidateUserAndEmail()
        {
            if (activePlayer == null)
            {
                string username = txtUser.Text;
                string email = txtEmail.Text;

                var validateUser = client.ValidateEmailAndUser(username, email);

                if (!validateUser)
                {
                    brdGrayBackground.Visibility = Visibility.Visible;
                    var errorPage = new PageError(Properties.Resources.lbError, Properties.Resources.lbDateExists);
                    frMessage.Content = errorPage;
                    errorPage.MessageClosed += (s, args) =>
                    {
                        txtLoadingDots.Visibility = Visibility.Collapsed;
                        brdGrayBackground.Visibility = Visibility.Collapsed;
                        frMessage.Visibility = Visibility.Collapsed;
                    };
                }

                return validateUser;
            }
            return true;
        }


        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                var warningPage = new PageWarning(Properties.Resources.lbEmptyField, Properties.Resources.tbMessageWarning);
                frMessage.Content = warningPage;
                brdGrayBackground.Visibility = Visibility.Visible;
                warningPage.MessageClosed += (s, args) =>
                {
                    txtLoadingDots.Visibility = Visibility.Collapsed;
                    brdGrayBackground.Visibility = Visibility.Collapsed;
                    frMessage.Visibility = Visibility.Collapsed;
                };
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                var warningPage = new PageWarning(Properties.Resources.lbEmptyField, Properties.Resources.tbMessageWarning);
                frMessage.Content = warningPage;
                brdGrayBackground.Visibility = Visibility.Visible;
                warningPage.MessageClosed += (s, args) =>
                {
                    txtLoadingDots.Visibility = Visibility.Collapsed;
                    brdGrayBackground.Visibility = Visibility.Collapsed;
                    frMessage.Visibility = Visibility.Collapsed;
                };
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length != 10 || !Regex.IsMatch(txtPhone.Text, @"^\d{10}$"))
            {
                MessageBox.Show("El campo de teléfono no debe estar vacío, debe contener exactamente 10 dígitos y debe contener solo números.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El campo de email no debe estar vacío y debe ser un email válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(psdPassword.Password))
            {
                var warningPage = new PageWarning(Properties.Resources.lbEmptyField, Properties.Resources.tbMessageWarning);
                frMessage.Content = warningPage;
                brdGrayBackground.Visibility = Visibility.Visible;
                warningPage.MessageClosed += (s, args) =>
                {
                    txtLoadingDots.Visibility = Visibility.Collapsed;
                    brdGrayBackground.Visibility = Visibility.Collapsed;
                    frMessage.Visibility = Visibility.Collapsed;
                };
                return false;
            }

            if (!DateTime.TryParseExact(txtDateOfBirthday.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                var warningPage = new PageWarning(Properties.Resources.lbError, Properties.Resources.lbDateError);
                frMessage.Content = warningPage;
                brdGrayBackground.Visibility = Visibility.Visible;
                warningPage.MessageClosed += (s, args) =>
                {
                    txtLoadingDots.Visibility = Visibility.Collapsed;
                    brdGrayBackground.Visibility = Visibility.Collapsed;
                    frMessage.Visibility = Visibility.Collapsed;
                }; 
                return false;
            }

            return true;
        }

        private void TxtUserTextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUser.Text.Length > 12)
            {
                txtUser.Text = txtUser.Text.Substring(0, 12);
                txtUser.CaretIndex = txtUser.Text.Length;
            }
        }

        private void TxtNameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length > 40)
            {
                txtName.Text = txtName.Text.Substring(0, 40);
                txtName.CaretIndex = txtName.Text.Length;
            }
        }

        private void TxtPhoneTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(txtPhone.Text, @"^\d*$") || txtPhone.Text.Length > 10)
            {
                MessageBox.Show("El campo de teléfono no debe exceder 10 caracteres y debe contener solo números.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                string cleanedPhone = Regex.Replace(txtPhone.Text, @"[^\d]", "");
                if (cleanedPhone.Length > 10)
                {
                    cleanedPhone = cleanedPhone.Substring(0, 10);
                }

                txtPhone.Text = cleanedPhone;
                txtPhone.CaretIndex = txtPhone.Text.Length;
            }
        }

        private void TxtEmailTextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtEmail.Text.Length > 45)
            {
                txtEmail.Text = txtEmail.Text.Substring(0, 45);
                txtEmail.CaretIndex = txtEmail.Text.Length;
            }
        }

        private void PsdPasswordPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            int maxLength = 20;

            if (passwordBox.Password.Length > maxLength)
            {
                passwordBox.Password = passwordBox.Password.Substring(0, maxLength);
            }
        }

        private void PsdPasswordConfirmPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            int maxLength = 20;

            if (passwordBox.Password.Length > maxLength)
            {
                passwordBox.Password = passwordBox.Password.Substring(0, maxLength);
            }
        }

        private void BtnClickReturn(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            if (activePlayer != null)
            {
                var home = new PageHome(activePlayer, language);
                homeFrame.Navigate(home);

                if (homeFrame.Content is PageHome pageHome)
                {
                    pageHome.dataGridItemsGames.Visibility = Visibility.Visible;
                }
            }
            else
            {
                NavigationService.GoBack();
            }
        }
    }
}
