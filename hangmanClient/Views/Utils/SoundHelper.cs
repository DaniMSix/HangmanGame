using System.Windows.Media;
using System;

public class SoundHelper
{
    private MediaPlayer backgroundPlayer;
    private bool isBackgroundPlaying;

    public SoundHelper()
    {
        backgroundPlayer = new MediaPlayer();
        isBackgroundPlaying = false;
    }

    public void PlayBackgroundMusic(string soundFile)
    {
        if (!isBackgroundPlaying)
        {
            backgroundPlayer.Open(new Uri(soundFile, UriKind.RelativeOrAbsolute));
            backgroundPlayer.Play();
            isBackgroundPlaying = true;
        }
    }

    public void PauseBackgroundMusic()
    {
        if (isBackgroundPlaying)
        {
            backgroundPlayer.Pause();
            isBackgroundPlaying = false;
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundPlayer.Stop();
        isBackgroundPlaying = false;
    }

    public bool IsBackgroundMusicPlaying()
    {
        return isBackgroundPlaying;
    }
}
