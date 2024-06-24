using Domain;
using System.Windows;
using System.Windows.Controls;
using ServerManageGame;

namespace Views.Pages
{

    public partial class PageWaitingRoom : Page
    {
        DTOPlayer activePlayer;
        private ManageGame manageGame;
        System.Windows.Controls.Frame frCurrentFrame;
        string language;
        private SoundHelper soundHelper;

        public PageWaitingRoom(DTOPlayer activePlayer, ManageGame manageGame, System.Windows.Controls.Frame homeFrame, string language)
        {
            this.activePlayer = activePlayer;
            this.frCurrentFrame = homeFrame;
            InitializeComponent();

            this.manageGame = manageGame;
            this.manageGame.PlayerJoined += OnPlayerJoined;
            this.manageGame.PlayerDisconnected += OnPlayerDisconnected;
            btnStartGame.Visibility = Visibility.Collapsed;
            this.language = language;


            if (manageGame.gameMatch.idGuesser > 0)
            {
                lbGuestPlayer.Visibility = Visibility.Collapsed;
                lbTitle.Content = Properties.Resources.lbLobby;
                btnCancel.Content = Properties.Resources.btnOut;
                
            }
            else
            {
                lbTitle.Content = Properties.Resources.lbAccessCodeLobby;
                lbAccessCode.Content = manageGame.gameMatch.code;
                lbGuestPlayer.Visibility = Visibility.Visible;
                btnCancel.Content = Properties.Resources.btnCancel;
            }

            soundHelper = new SoundHelper();
        }

        private void OnPlayerJoined(string namePlayerGuesser)
        {
            lbGuestPlayer.Content = $"{namePlayerGuesser} " + Properties.Resources.lbJoinPlayer;
            lbGuestPlayer.Visibility = Visibility.Visible;
            btnStartGame.Visibility = Visibility.Visible;
        }

        private void OnPlayerDisconnected(string namePlayerGuesser)
        {
            lbGuestPlayer.Content = $"{namePlayerGuesser} " + Properties.Resources.lbDisconnectedPlayer;
            lbGuestPlayer.Visibility = Visibility.Visible;
            btnStartGame.Visibility = Visibility.Collapsed;
        }

        private void BtnClickOutGame(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            manageGame.FinishGameConnectionn();
            var pageHome = new PageHome(activePlayer, language);
            frCurrentFrame.Navigate(pageHome);
        }

        private void BtnStartManageGame(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            manageGame.StartGame();
        }
    }

}
