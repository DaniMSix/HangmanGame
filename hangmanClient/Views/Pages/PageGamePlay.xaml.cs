using ServerManageGame;
using System.Windows.Controls;
using System.Windows;
using Views.Notifications;

namespace Views.Pages
{
    public partial class PageGamePlay : Page
    {
        ManageGame manageGame;
        Frame frame;

        public PageGamePlay(ManageGame manageGame, Frame frCurrentFrame, string word, string hint)
        {
            InitializeComponent();
            this.manageGame = manageGame;
            this.frame = frCurrentFrame;
            lbWord.Content = word;
            lbHint.Content = hint;
            this.manageGame.IfGuessed += OnIfGuessed;
            this.manageGame.GameFinished += OnGameFinished;
            ButtonGrid.Visibility = Visibility.Collapsed;
        }

        public PageGamePlay(ManageGame manageGame, Frame frCurrentFrame, string hint)
        {
            InitializeComponent();
            this.manageGame = manageGame;
            this.frame = frCurrentFrame;
            lbHint.Content = hint;
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

            //switch (failedAttempts)
        }

        public void OnGameFinished(Domain.DTOPlayer activePlayer, Frame frame, string word, int score, bool win, bool challenger)
        {
            if (challenger)
            {
                if (win)
                {
                    frMessage.Content = new PageWinningChallenger(activePlayer, frame, score);
                }
                else
                {
                    frMessage.Content = new PageLoserChallenger(activePlayer, frame, score);
                }
            }
            else
            {
                if (win)
                {
                    frMessage.Content = new PageWinningGuesser(activePlayer, frame, score);
                }
                else
                {
                    frMessage.Content = new PageLoserGuesser(activePlayer, frame, word, score);
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
    }
}
