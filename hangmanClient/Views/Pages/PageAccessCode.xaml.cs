using Domain;
using System;
using System.Windows;
using System.Windows.Controls;
using ServerManageGame;
using Views.SRIManageGameService;

namespace Views.Pages
{
    public partial class PageAccessCode : Page
    {
        private ManageGame manageGame;
        private Domain.DTOPlayer activePlayer;
        string accessCode;
        private Gamematch gamematch = new Gamematch();
        System.Windows.Controls.Frame frameCurrent;
        string language;
        private SoundHelper soundHelper;

        public PageAccessCode(Domain.DTOPlayer playerActive, System.Windows.Controls.Frame frameCurrent)
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
            this.activePlayer = playerActive;
            manageGame = new ManageGame(activePlayer, frameCurrent, language);
            this.frameCurrent = frameCurrent;
            this.manageGame.AccessCode +=  ShowMessageAccessCode;
            this.manageGame.AccessCode += ShowMessageLobbyComplete;
        }

        private void BtnClickJoinGame(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            accessCode = txtCodeOneNumber.Text + txtCodeSecondNumber.Text + txtCodeThirdNumber.Text + txtCodeFourthNumber.Text;

            gamematch.idGuesser = activePlayer.IdPlayer;
            gamematch.code = accessCode;
            manageGame.StartJoinGame(gamematch, true);
        }

        private void BtnClickReturn(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var home = new PageHome(activePlayer, language);
            frameCurrent.Navigate(home);

            if (frameCurrent.Content is PageHome pageHome)
            {
                pageHome.dataGridItemsGames.Visibility = Visibility.Visible;
            }
        }

        private void ShowMessageAccessCode(string title, string message)
        {
            var warningPage = new PageWarning(title, message);
            frMessage.Content = warningPage;
            warningPage.MessageClosed += (s, args) =>
            {
                frMessage.Visibility = Visibility.Collapsed;
            };
        }

        private void ShowMessageLobbyComplete(string title, string message)
        {
            var warningPage = new PageError(title, message);
            frMessage.Content = warningPage;
            warningPage.MessageClosed += (s, args) =>
            {
                frMessage.Visibility = Visibility.Collapsed;
            };
        }
    }
}
