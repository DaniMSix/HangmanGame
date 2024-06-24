using Domain;
using ServerManageGame;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageStatistics : Page
    {
        DTOPlayer activePlayer;
        ManageGame manageGame;
        string language;
        System.Windows.Controls.Frame frCurrent;
        private SoundHelper soundHelper;

        public PageStatistics(DTOPlayer activePlayer, string language, System.Windows.Controls.Frame frCurrent)
        {
            InitializeComponent();
            this.activePlayer = activePlayer;
            manageGame = new ManageGame(language);
            this.language = language;
            LoadStatistics();
            lbGamesWon.Content = manageGame.RecoveringScorePlayer(activePlayer.IdPlayer).ToString() + " puntos ganados";
            this.frCurrent = frCurrent;
            soundHelper = new SoundHelper();
        }

        public void LoadStatistics()
        {
            var statistics = manageGame.RecoveringStatistics(activePlayer.IdPlayer);
            dataGridStatistics.ItemsSource = statistics;
        }

        private void BtnClickReturn(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var home = new PageHome(activePlayer, language);
            frCurrent.Navigate(home);

            if (frCurrent.Content is PageHome pageHome)
            {
                pageHome.dataGridItemsGames.Visibility = Visibility.Visible;
            }
        }
    }
}