using ServerManageGame;
using System.Windows.Controls;
using System.Windows;
using Views.Notifications;
using System;

namespace Views.Pages
{
    public partial class PageGamePlay : Page
    {
        ManageGame manageGame;
        System.Windows.Controls.Frame frCurrent;
        string language;

        public PageGamePlay(ManageGame manageGame, System.Windows.Controls.Frame frCurrentFrame, string word, string hint, string letters)
        {
            InitializeComponent();

            this.manageGame = manageGame;
            this.frCurrent = frCurrentFrame;
            lbWord.Content = word;
            lbHint.Content = hint;
            lbWordGuess.Content = letters;
            this.manageGame.IfGuessed += OnIfGuessed;
            this.manageGame.GameFinished += OnGameFinished;
            ButtonGrid.Visibility = Visibility.Collapsed;
        }

        public PageGamePlay(ManageGame manageGame, System.Windows.Controls.Frame frCurrentFrame, string hint, string letters)
        {   
            InitializeComponent();
            this.manageGame = manageGame;
            this.frCurrent = frCurrentFrame;
            lbHint.Content = hint;
            lbWordGuess.Content = letters;
            this.manageGame.IfGuessed += OnIfGuessed;
            this.manageGame.GameFinished += OnGameFinished;
        }

        public void OnIfGuessed(char[] letters, int failedAttempts, bool guesser)
        {
            string word = "";
            foreach(char letter in letters)
            {
                if (letter == '\0')
                {
                    word += "?";
                }
                else
                {
                    word += letter.ToString();
                }
            }
            lbWordGuess.Content = word;
            if (guesser)
            {
                ButtonGrid.Visibility = Visibility.Visible;
            }

            switch (failedAttempts)
            {
                case 1:
                    imgStepOne.Visibility = Visibility.Visible;
                    break;
                case 2:
                    imgStepTwo.Visibility = Visibility.Visible;
                    break;
                case 3:
                    imgStepThree.Visibility = Visibility.Visible;
                    break;
                case 4:
                    imgStepFour.Visibility = Visibility.Visible;
                    break;
                case 5:
                    imgStepFive.Visibility = Visibility.Visible;
                    break;
                case 6:
                    imgStepSix.Visibility = Visibility.Visible;
                    break;

            }
        }

        public void OnGameFinished(Domain.DTOPlayer activePlayer, System.Windows.Controls.Frame frame, string word, int score, bool win, bool challenger)
        {
            if (challenger)
            {
                if (win)
                {
                    frMessage.Content = new PageWinningChallenger(activePlayer, frame, score, language);
                }
                else
                {
                    frMessage.Content = new PageLoserChallenger(activePlayer, frame, score, language);
                }
            }
            else
            {
                if (win)
                {
                    frMessage.Content = new PageWinningGuesser(activePlayer, frame, score, language);
                }
                else
                {
                    frMessage.Content = new PageLoserGuesser(activePlayer, frame, word, score, language);
                }
            }
        }

        private void BtnGetLetter(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonText = clickedButton.Content.ToString();
                clickedButton.IsEnabled = false;
                clickedButton.Opacity = 0.5;
                manageGame.ValidateLetter(buttonText[0]);
                ButtonGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnClickSalir(object sender, RoutedEventArgs e)
        {
            manageGame.Discconect();
            var pageHome = new PageHome(manageGame.activePlayer, language);
            frCurrent.Navigate(pageHome);
        }
    }
}
