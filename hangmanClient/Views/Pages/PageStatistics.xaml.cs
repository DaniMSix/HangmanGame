using Domain;
using ServerManageGame;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageStatistics : Page
    {
        DTOPlayer activePlayer;
        ManageGame manageGame;
        string language;
        public PageStatistics(DTOPlayer activePlayer, string language)
        {
            InitializeComponent();
            this.activePlayer = activePlayer;
            manageGame = new ManageGame(language);
            LoadStatistics();
            this.language = language;
        }

        public void LoadStatistics()
        {
            var statistics = manageGame.RecoveringStatistics(activePlayer.IdPlayer);
            dataGridStatistics.ItemsSource = statistics;
        }
    }
}
