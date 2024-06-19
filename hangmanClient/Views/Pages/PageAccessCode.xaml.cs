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
        string language;
        public PageAccessCode(Domain.DTOPlayer playerActive, System.Windows.Controls.Frame frameCurrent)
        {
            InitializeComponent();
            this.activePlayer = playerActive;
            manageGame = new ManageGame(activePlayer, frameCurrent, language);
        }

        private void BtnClickJoinGame(object sender, RoutedEventArgs e)
        {
            accessCode = txtCodeOneNumber.Text + txtCodeSecondNumber.Text + txtCodeThirdNumber.Text + txtCodeFourthNumber.Text
                + txtCodeFifthNumber.Text;

            gamematch.idGuesser = activePlayer.IdPlayer;
            gamematch.code = accessCode;
            manageGame.StartJoinGame(gamematch, true);
        }

        private void txtCodeOneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
