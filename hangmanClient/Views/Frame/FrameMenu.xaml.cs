using Domain;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Views.Pages
{
    
    public partial class FrameMenu : Page
    {
        private System.Windows.Controls.Frame homeFrame;
        private DTOPlayer playerActive;
        string language;
        private SoundHelper soundHelper;
        public FrameMenu(System.Windows.Controls.Frame frHome, DTOPlayer playerActive, string language)
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
            homeFrame = frHome;
            this.playerActive = playerActive;
            this.language = language;
        }

        private void BtnClicHideMenu(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var parentFrame = this.Parent as System.Windows.Controls.Frame;
            if (parentFrame != null)
            {
                parentFrame.Visibility = Visibility.Collapsed;
            }
        }

        private void ClickEditProfile(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var registerPage = new PageCreateProfile(playerActive, homeFrame, language);
            if (homeFrame.Content is PageHome pageHome)
            {
                pageHome.dataGridItemsGames.Visibility = Visibility.Collapsed;
            }

            homeFrame.Navigate(registerPage);
        }

        private void ClickViewStatistics(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var statistics = new PageStatistics(playerActive, language, homeFrame);
            homeFrame.Navigate(statistics);
        }

        private void ClickChangePassword(object sender, RoutedEventArgs e)
        {

        }

        private void ClickSignOut(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var login = new Login();
            homeFrame.Navigate(login);
        }
    }
}
