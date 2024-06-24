using System;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageSuccess : Page
    {
        public event EventHandler MessageClosed;
        private SoundHelper soundHelper;

        public PageSuccess(string title, string content)
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
            txtTitle.Content = title;
            ((TextBlock)txtContent.Content).Text = content;
        }

        private void CloseMessage()
        {
            MessageClosed?.Invoke(this, EventArgs.Empty);
        }
       
        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            CloseMessage();
        }
    }
}
