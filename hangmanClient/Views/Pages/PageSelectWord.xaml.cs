using Domain;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using ServerManageGame;
using Views.SRIPlayerManagement;

namespace Views.Pages
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class PageSelectWord : Page
    {
        private ManageGame manageGame;
        Domain.DTOPlayer activePlayer;
        Frame frCurrentFrame;
        public PageSelectWord(Domain.DTOPlayer activePlayer, Frame frHome)
        {
            InitializeComponent();
            manageGame = new ManageGame(activePlayer, frHome);
            this.activePlayer = activePlayer;
        }

        private void BtnClickGameCreate(object sender, RoutedEventArgs e)
        {
            SRIManageGameService.Gamematch gamematch = new SRIManageGameService.Gamematch
            {
                idChallenger = activePlayer.IdPlayer,
                language = "Español",
                idWord = 1,
                idMatchStatus = 2
            };

            manageGame.StartGameConection(gamematch);
        }
    }
}
