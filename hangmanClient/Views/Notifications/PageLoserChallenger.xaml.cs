using System.Windows;
using System.Windows.Controls;
using Views.Pages;

namespace Views.Notifications
{
    public partial class PageLoserChallenger : Page
    {
        Frame homeFrame;
        Domain.DTOPlayer activePlayer;
        public PageLoserChallenger(Domain.DTOPlayer activePlayer, Frame homeFrame, int score)
        {
            InitializeComponent();
            this.homeFrame = homeFrame;
            this.activePlayer = activePlayer;
            lbScore.Content = score;
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            var home = new PageHome(activePlayer);
            homeFrame.Navigate(home);
        }
    }
}
