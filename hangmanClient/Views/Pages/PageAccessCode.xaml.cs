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
        public PageAccessCode(Domain.DTOPlayer playerActive, Frame frameCurrent)
        {
            InitializeComponent();
            this.activePlayer = playerActive;
            manageGame = new ManageGame(activePlayer, frameCurrent);
        }

        private void BtnClickJoinGame(object sender, RoutedEventArgs e)
        {
            accessCode = txtCodeOneNumber.Text + txtCodeSecondNumber.Text + txtCodeThirdNumber.Text + txtCodeFourthNumber.Text
                + txtCodeFifthNumber.Text;

            gamematch.idGuesser = activePlayer.IdPlayer;
            gamematch.code = accessCode;

            Console.WriteLine("--------------- PageAccessCode -----------------");
            Console.WriteLine("IdGuesser " + gamematch.idGuesser);

            

            manageGame.StartJoinGame(gamematch);
        }

        private void txtCodeOneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
