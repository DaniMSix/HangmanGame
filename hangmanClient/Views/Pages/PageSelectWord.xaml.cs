using Domain;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using ServerManageGame;
using Views.SRIPlayerManagement;
using Views.Frame;

namespace Views.Pages
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class PageSelectWord : Page
    {
        private ManageGame manageGame;
        Domain.DTOPlayer activePlayer;
        System.Windows.Controls.Frame frCurrentFrame;
        string language;
        public PageSelectWord(Domain.DTOPlayer activePlayer, System.Windows.Controls.Frame frHome, string language)
        {
            InitializeComponent();
            manageGame = new ManageGame(activePlayer, frHome, language);
            this.activePlayer = activePlayer;
            this.language = language;
            var pageAnimals = new FrameAnimals(1, language);
            frCategories.Navigate(pageAnimals);
        }

        private void BtnClickAnimals(object sender, RoutedEventArgs e)
        {
            var pageAnimals = new FrameAnimals(1, language);
            frCategories.Navigate(pageAnimals);
        }

        private void BtnClickInstruments(object sender, RoutedEventArgs e)
        {
            var pageInstruments = new FrameInstruments();
            frCategories.Navigate(pageInstruments);
        }

        private void BtnClickFruits(object sender, RoutedEventArgs e)
        {
            var pageFruits = new FrameFruits();
            frCategories.Navigate(pageFruits);
        }

        private void BtnClickCities(object sender, RoutedEventArgs e)
        {
            var pageCities = new FrameCities();
            frCategories.Navigate(pageCities);
        }

        private void BtnClickOccupations(object sender, RoutedEventArgs e)
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

        /*
         * private void BtnClickGameCreate(object sender, RoutedEventArgs e)
        {
            
        }
         */


    }
}
