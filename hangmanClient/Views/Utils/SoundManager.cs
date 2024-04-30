using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Media;

namespace Views.Utils
{
    public class SoundManager
    {
        private static MediaPlayer mediaPlayer;

        static SoundManager()
        {
            mediaPlayer = new MediaPlayer();
        }

        // Método para reproducir el sonido del clic del botón
        public static void PlayButtonClickSound()
        {
            mediaPlayer.Open(new Uri("C:\\Users\\Dani\\Desktop\\HangmanGame\\hangmanClient\\Views\\Music\\button-sound.mp3"));
            mediaPlayer.Play();
        }
    }
}
