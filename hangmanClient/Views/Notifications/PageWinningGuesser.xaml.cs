﻿using System.Windows;
using System.Windows.Controls;
using Views.Pages;

namespace Views.Notifications
{
    public partial class PageWinningGuesser : Page
    {
        System.Windows.Controls.Frame homeFrame;
        Domain.DTOPlayer activePlayer;
        string language;
        private SoundHelper soundHelper;
        public PageWinningGuesser(Domain.DTOPlayer activePlayer, System.Windows.Controls.Frame homeFrame, int score, string language)
        {
            InitializeComponent();
            this.homeFrame = homeFrame;
            this.activePlayer = activePlayer;
            lbScore.Content = score;
            this.language = language;
            soundHelper = new SoundHelper();
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            var home = new PageHome(activePlayer, language);
            homeFrame.Navigate(home);
        }
    }
}
