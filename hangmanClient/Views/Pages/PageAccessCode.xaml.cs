using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Views.ServerManageGame;

namespace Views.Pages
{
    public partial class PageAccessCode : Page
    {
        private ManageGame manageGame;
        DTOPlayer activePlayer;
        string accessCode;
        public PageAccessCode(DTOPlayer playerActive, Frame frameCurrent)
        {
            InitializeComponent();
            this.activePlayer = playerActive;
            manageGame = new ManageGame(activePlayer, frameCurrent);
        }

        private void BtnClickJoinGame(object sender, RoutedEventArgs e)
        {
            accessCode = txtCodeOneNumber.Text + txtCodeSecondNumber.Text + txtCodeThirdNumber.Text + txtCodeFourthNumber.Text
                + txtCodeFifthNumber + txtCodeSixthNumber;
            manageGame.StartJoinGame(accessCode);
        }
    }
}
