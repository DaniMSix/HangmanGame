using System.Windows;
using System.Windows.Controls;
using Views.Pages;

namespace Views.Notifications
{
    public partial class PageLoserGuesser : Page
    {
        System.Windows.Controls.Frame homeFrame;
        Domain.DTOPlayer activePlayer;
        string language;
        private SoundHelper soundHelper;
        public PageLoserGuesser(Domain.DTOPlayer activePlayer, System.Windows.Controls.Frame homeFrame, string word, int score, string language)
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
            this.homeFrame = homeFrame;
            this.activePlayer = activePlayer;
            lbScore.Content = score;
            //lbWord.Content += " " + word;
            this.language = language;
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var home = new PageHome(activePlayer, language);
            homeFrame.Navigate(home);
        }
    }
}
