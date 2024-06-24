using System;
using System.Windows;
using System.Windows.Controls;
using Views.Utils;
using System.Threading.Tasks;

namespace Views.Pages
{
    public partial class PageWarning : Page
    {
        public event EventHandler MessageClosed;
        private SoundHelper soundHelper;
        public PageWarning(String title, string message)
        {
            InitializeComponent();
            lbTitleWarning.Content = title;
            tbMessageWarning.Text = message;
            soundHelper = new SoundHelper();
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
