using Domain;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    
    public partial class FrameMenu : Page
    {
        private System.Windows.Controls.Frame homeFrame;
        private DTOPlayer playerActive;
        string language;
        public FrameMenu(System.Windows.Controls.Frame frHome, DTOPlayer playerActive, string language)
        {
            InitializeComponent();
            homeFrame = frHome;
            this.playerActive = playerActive;
            this.language = language;
        }

        private void BtnClicHideMenu(object sender, RoutedEventArgs e)
        {
            var parentFrame = this.Parent as System.Windows.Controls.Frame;
            if (parentFrame != null)
            {
                parentFrame.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickEditProfile(object sender, RoutedEventArgs e)
        {
            var registerPage = new PageCreateProfile(playerActive, homeFrame, language);
            if (homeFrame.Content is PageHome pageHome)
            {
                pageHome.dataGridItemsGames.Visibility = Visibility.Collapsed;
            }

            homeFrame.Navigate(registerPage);
        }

        private void ClickViewStatistics(object sender, RoutedEventArgs e)
        {
            var statistics = new PageStatistics(playerActive, language);
            homeFrame.Navigate(statistics);
        }

        private void ClickChangePassword(object sender, RoutedEventArgs e)
        {

        }

        private void ClickChangeLanguage(object sender, RoutedEventArgs e)
        {

        }

        private void ClickSignOut(object sender, RoutedEventArgs e)
        {

        }
    }
}
