using System;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageError : Page
    {
        public event EventHandler MessageClosed;
        private SoundHelper soundHelper;
        public PageError(string title, string message)
        {
            InitializeComponent();
            lbTitleError.Content = title;
            tbMessageError.Text = message;
            soundHelper = new SoundHelper();
        }

        private void CloseMessage()
        {
            MessageClosed?.Invoke(this, EventArgs.Empty);
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\retro-videogame.mp3");
            CloseMessage();
        }
    }
}
