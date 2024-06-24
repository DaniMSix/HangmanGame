using ServerManageGame;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Views.SRIManageGameService;
using Views.Utils;

namespace Views.Pages
{
    public partial class PageHome : Page
    {
        Domain.DTOPlayer activePlayer;
        ManageGame manageGame;
        DataGrid datagridGames;
        private Gamematch gamematch = new Gamematch();
        string language;
        System.Windows.Controls.Frame frame;
        private SoundHelper soundHelper;

        public PageHome(Domain.DTOPlayer activePlayer, string language)
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
            this.activePlayer = activePlayer;
            this.language = language;
            manageGame = new ManageGame(activePlayer, frHome, language);
            datagridGames = dataGridItemsGames;
            var selectLanguagePage = new PageSelectLanguage();
            selectLanguagePage.LanguageSelected += SelectLanguagePage_LanguageSelected;
            this.manageGame.AccessCode += ShowMessageLobbyComplete;
            LoadGamesAsync();
        }

        private void SelectLanguagePage_LanguageSelected(object sender, string e)
        {
            this.language = e;

            var app = Application.Current as App;
            if (app != null)
            {
                if (language == "Ingles")
                {
                    app.SetCulture("en");
                }
                else
                {
                    app.SetCulture("");
                }
            }
        }

        private void BtnClicShowMenu(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var menuPage = new FrameMenu(frHome, activePlayer, language);
            frMenu.Navigate(menuPage);
            frMenu.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
        }

        private void BtnClicHideMenu(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            frMenu.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
        }

        private  void BtnClickJoinGameWithCode(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var joinGame = new PageAccessCode(activePlayer, frHome);
            frHome.Navigate(joinGame);
        }

        private void BtnClickCreateGame(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var pageSelectWord = new PageSelectWord(activePlayer, frHome, language);
            frHome.Navigate(pageSelectWord);
        }

        private async void LoadGamesAsync()
        {
            dataGridItemsGames.ItemsSource = await Task.Run(() => manageGame.RecoveringGames());
        }

        private void BtnJoinGame(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            if (dataGridItemsGames.SelectedItem != null)
            {
                var selectedGame = (SRIManageGameService.DTOGameMatch)dataGridItemsGames.SelectedItem;

                int gameId = selectedGame.idGamematch;
                gamematch.idGuesser = activePlayer.IdPlayer;
                manageGame.StartJoinGame(gamematch, false);
            }
        }

        private void BtnLoadGames(object sender, RoutedEventArgs e)
        {
            if (soundHelper.IsBackgroundMusicPlaying())
            {
                soundHelper.PauseBackgroundMusic();
            }
            LoadGamesAsync();
            if (!soundHelper.IsBackgroundMusicPlaying())
            {
                soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\retro-videogame.mp3");
            }
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
