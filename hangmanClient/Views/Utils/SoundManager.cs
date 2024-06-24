using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Media;

namespace Views.Utils
{
    public class SoundManager
    {
        private static MediaPlayer player = new MediaPlayer();

        public static void PlaySound(string soundFile)
        {
            player.Open(new Uri(soundFile, UriKind.Relative));
            player.Play();
        }
    }
}
