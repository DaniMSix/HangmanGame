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
        public PageLoserGuesser(Domain.DTOPlayer activePlayer, System.Windows.Controls.Frame homeFrame, string word, int score, string language)
        {
            InitializeComponent();
            this.homeFrame = homeFrame;
            this.activePlayer = activePlayer;
            lbScore.Content = score;
            //lbWord.Content += " " + word;
            this.language = language;
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            var home = new PageHome(activePlayer, language);
            homeFrame.Navigate(home);
        }
    }
}
