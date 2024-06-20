using Domain;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using ServerManageGame;
using Views.SRIPlayerManagement;
using Views.Frame;
using System.Linq;

namespace Views.Pages
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class PageSelectWord : Page
    {
        private ManageGame manageGame;
        Domain.DTOPlayer activePlayer;
        System.Windows.Controls.Frame frCurrentFrame;
        string language;
        int idWord;
        public PageSelectWord(Domain.DTOPlayer activePlayer, System.Windows.Controls.Frame frHome, string language)
        {
            InitializeComponent();
            manageGame = new ManageGame(activePlayer, frHome, language);
            this.activePlayer = activePlayer;
            this.language = language;
            var pageAnimals = new FrameAnimals(1, language);
            pageAnimals.WordSelected += WordSelected;
            frCategories.Navigate(pageAnimals);
        }

        private void BtnClickAnimals(object sender, RoutedEventArgs e)
        {
            var pageAnimals = new FrameAnimals(1, language);
            pageAnimals.WordSelected += WordSelected;
            frCategories.Navigate(pageAnimals);
        }

        private void BtnClickInstruments(object sender, RoutedEventArgs e)
        {
            var pageInstruments = new FrameInstruments(2, language);

            frCategories.Navigate(pageInstruments);
        }

        private void BtnClickFruits(object sender, RoutedEventArgs e)
        {
            var pageFruits = new FrameFruits(3, language);
            pageFruits.WordSelected += WordSelected;
            frCategories.Navigate(pageFruits);
        }

        private void BtnClickCities(object sender, RoutedEventArgs e)
        {
            var pageCities = new FrameCities(4, language);
            pageCities.WordSelected += WordSelected;
            frCategories.Navigate(pageCities);
        }

        private void BtnClickOccupations(object sender, RoutedEventArgs e)
        {
            var pageOccupations = new FrameOccupations(5, language);
            pageOccupations.WordSelected += WordSelected;
            frCategories.Navigate(pageOccupations);
        }

        private void BtnClickCreateGame(object sender, RoutedEventArgs e)
        {
            SRIManageGameService.Gamematch gamematch = new SRIManageGameService.Gamematch
            {
                idChallenger = activePlayer.IdPlayer,
                language = language,
                idWord = this.idWord,
                idMatchStatus = 2
            };

            manageGame.StartGameConection(gamematch);
        }

        private void WordSelected(object sender, int categoryId)
        {
            this.idWord = categoryId;
        }

        private void BtnClickReturn(object sender, RoutedEventArgs e)
        {
            var home = new PageHome(activePlayer, language);
            frCurrentFrame.Navigate(home);

            if (frCurrentFrame.Content is PageHome pageHome)
            {
                pageHome.dataGridItemsGames.Visibility = Visibility.Visible;
            }
        }
    }
}