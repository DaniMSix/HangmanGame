using Domain;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageCreateProfile : Page
    {
        SRIPlayerManagement.PlayerManagementClient client = new SRIPlayerManagement.PlayerManagementClient();

        private string username;
        private string fullname;
        private string phone;
        private string email;
        private string dateOfBirthday;
        private string password;
        private string passwordConfirm;
        private DTOPlayer activePlayer;
        private System.Windows.Controls.Frame homeFrame;
        string language;

        public PageCreateProfile()
        {
            InitializeComponent();
        }

        public PageCreateProfile(DTOPlayer activePlayer, System.Windows.Controls.Frame homeFrame, string language)
        {
            InitializeComponent();
            this.activePlayer = activePlayer;
            btnCreate.Content = "Modificar";
            lbTitle.Content = "Modificar";
            psdPassword.Visibility = Visibility.Collapsed;
            psdPasswordConfirm.Visibility = Visibility.Collapsed;
            txtEmail.IsEnabled = false;
            imgBlockLabel.Visibility = Visibility.Visible;
            imgPassword.Visibility = Visibility.Collapsed;
            imgPasswordConfirm.Visibility = Visibility.Collapsed;
            imgQuestion.Visibility = Visibility.Collapsed;
            ShowPersonalInformation();
            this.homeFrame = homeFrame;
        }

        public void ShowPersonalInformation()
        {
            txtUser.Text = activePlayer.Username; 
            txtName.Text = activePlayer.Name;
            txtPhone.Text = activePlayer.Phonenumber;
            txtEmail.Text = activePlayer.Email;
            txtDateOfBirthday.Text = activePlayer.Birthday.ToString();
        }

        private void BtnClickSend(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var newPlayerActive = new SRIPlayerManagement.DTOPlayer()
                {
                    Username = txtUser.Text,
                    Name = txtName.Text,
                    Phonenumber = txtPhone.Text,
                    Email = txtEmail.Text,
                    Birthdate = DateTime.Parse(txtDateOfBirthday.Text),
                    Password = psdPassword.Password
                };

                if (activePlayer == null)
                {
                    client.RegisterPlayer(newPlayerActive);
                    MessageBox.Show("Perfil creado con éxito.");
                }
                else
                {
                    var newPlayerUpdate = new SRIPlayerManagement.DTOPlayer()
                    {
                        IdPlayer = activePlayer.IdPlayer,
                        Username = txtUser.Text,
                        Name = txtName.Text,
                        Phonenumber = txtPhone.Text,
                        Birthdate = DateTime.Parse(txtDateOfBirthday.Text),
                    };
                    client.UpdateFullProfile(newPlayerUpdate);
                    MessageBox.Show("Perfil modificado con éxito.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Formato de fecha incorrecto. Por favor, ingresa una fecha válida" +
                    ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClickReturn(object sender, RoutedEventArgs e)
        {
            var home = new PageHome(activePlayer, language);
            homeFrame.Navigate(home);

            if (homeFrame.Content is PageHome pageHome)
            {
                pageHome.dataGridItemsGames.Visibility = Visibility.Visible;
            }
        }

    }
}
