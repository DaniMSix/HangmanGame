using Domain;
using ServerManageGame;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageStatistics : Page
    {
        DTOPlayer activePlayer;
        ManageGame manageGame;
        public PageStatistics(DTOPlayer activePlayer)
        {
            InitializeComponent();
            this.activePlayer = activePlayer;
            manageGame = new ManageGame();
            LoadStatistics();
        }

        public void LoadStatistics()
        {
            var statistics = manageGame.RecoveringStatistics(activePlayer.IdPlayer);
            dataGridStatistics.ItemsSource = statistics;
        }
    }
}
