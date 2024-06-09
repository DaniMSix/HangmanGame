using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Views.Utils;

namespace Views.Pages
{
    public partial class Login : Page
    {
        private bool isPlaying = false;

        public Login()
        {
            InitializeComponent();
            // Subscribe to the MediaEnded event to restart playback when the media file has ended
            //mediaPlayerBackgroundMusic.MediaEnded += MediaPlayerMediaEnded;
        }

        private void ClicPlayMusicButtonClick(object sender, RoutedEventArgs e)
        {
            btnMusic.Opacity = 0.2;
            if (!isPlaying)
            {
                // Set the LoadedBehavior to Manual
                //mediaPlayerBackgroundMusic.LoadedBehavior = MediaState.Manual;
                // Play the media file
                //mediaPlayerBackgroundMusic.Play();
                // Increase the volume (for example, by 10%)
                //mediaPlayerBackgroundMusic.Volume += 0.1; // Adjust this value as needed
                // Update the playback state
                isPlaying = true;
            }
            else
            {
                // Pause the playback
                //mediaPlayerBackgroundMusic.Pause();
                // Update the playback state
                isPlaying = false;
            }
        }

        // Method to restart playback when the media file has ended
        private void MediaPlayerMediaEnded(object sender, RoutedEventArgs e)
        {
            // If the media file has ended and playback is active, restart playback
            if (isPlaying)
            {
                //mediaPlayerBackgroundMusic.Position = TimeSpan.Zero; // Reset the player position to the beginning
                //mediaPlayerBackgroundMusic.Play(); // Start playback again
            }
        }

        private void ClickLogin(object sender, RoutedEventArgs e)
        {
            ButtonAnimation buttonAnimation = new ButtonAnimation();
            buttonAnimation.ClickDarken(btnLogin, () =>
            {
                btnLogin.Dispatcher.Invoke(() =>
                {
                    btnLogin.Opacity = 1.0;
                });
            });
            btnLogin.Opacity = 1.0;
            SoundManager.PlayButtonClickSound();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
