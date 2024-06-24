using System;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageSelectLanguage : Page
    {
        string language;
        public event EventHandler<string> LanguageSelected;
        private SoundHelper soundHelper;

        public PageSelectLanguage()
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            string language = this.language;
            var app = Application.Current as App;
            if (app != null)
            {
                if (language == "Ingles")
                {
                    app.SetCulture("en");
                    
                }
                else
                {
                    app.SetCulture(""); 
                }
            }
            LanguageSelected?.Invoke(this, language);
        }

        private void BtnMexicoClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            language = "Español"; 
        }

        private void BtnEUClick(object sender, RoutedEventArgs e)
        {
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            language = "Ingles"; 
        }
    }
}