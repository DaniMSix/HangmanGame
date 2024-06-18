using System.Windows;
using System.Windows.Controls;
using Views.Pages;

namespace Views.Notifications
{
    public partial class PageLoserGuesser : Page
    {
        Frame homeFrame;
        Domain.DTOPlayer activePlayer;
        public PageLoserGuesser(Domain.DTOPlayer activePlayer, Frame homeFrame, string word, int score)
        {
            InitializeComponent();
            this.homeFrame = homeFrame;
            this.activePlayer = activePlayer;
            lbScore.Content = score;
            lbWord.Content += " " + word;
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            var home = new PageHome(activePlayer);
            homeFrame.Navigate(home);
        }
    }
}
