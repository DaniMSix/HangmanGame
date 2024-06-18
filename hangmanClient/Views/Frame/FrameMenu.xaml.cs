using Domain;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    
    public partial class FrameMenu : Page
    {
        private Frame homeFrame;
        private DTOPlayer playerActive;
        public FrameMenu(Frame frHome, DTOPlayer playerActive)
        {
            InitializeComponent();
            homeFrame = frHome;
            this.playerActive = playerActive;
        }

        private void BtnClicHideMenu(object sender, RoutedEventArgs e)
        {
            var parentFrame = this.Parent as Frame;
            if (parentFrame != null)
            {
                parentFrame.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickEditProfile(object sender, RoutedEventArgs e)
        {
            var registerPage = new PageCreateProfile(playerActive, homeFrame);
            if (homeFrame.Content is PageHome pageHome)
            {
                pageHome.dataGridItemsGames.Visibility = Visibility.Collapsed;
            }

            homeFrame.Navigate(registerPage);
        }

        private void ClickViewStatistics(object sender, RoutedEventArgs e)
        {
            var statistics = new PageStatistics(playerActive);
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
