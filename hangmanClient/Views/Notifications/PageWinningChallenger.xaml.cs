using System.Windows;
using System.Windows.Controls;
using Views.Pages;

namespace Views.Notifications
{
    public partial class PageWinningChallenger : Page
    {
        System.Windows.Controls.Frame homeFrame;
        Domain.DTOPlayer activePlayer;
        string language;
        public PageWinningChallenger(Domain.DTOPlayer activePlayer, System.Windows.Controls.Frame homeFrame, int score, string language)
        {
            InitializeComponent();
            this.homeFrame = homeFrame;
            this.activePlayer = activePlayer;
            lbScore.Content = score;
            this.language = language;
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            var home = new PageHome(activePlayer, language);
            homeFrame.Navigate(home);
        }
    }
}
