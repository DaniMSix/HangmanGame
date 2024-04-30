using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Views.Utils;

namespace Views.Pages
{
    public partial class Login : Page
    {
        // Declarar una variable booleana para realizar un seguimiento del estado de reproducción
        private bool isPlaying = false;
        public Login()
        {
            InitializeComponent();
            // Suscribirse al evento MediaEnded para reiniciar la reproducción cuando el archivo multimedia haya terminado
            mediaPlayerBackgroundMusic.MediaEnded += MediaPlayerMediaEnded;
        }

        private void ClicPlayMusicButtonClick(object sender, RoutedEventArgs e)
        {
            btnMusic.Opacity = 0.2;
            if (!isPlaying)
            {
                // Establecer el comportamiento de carga en Manual
                mediaPlayerBackgroundMusic.LoadedBehavior = MediaState.Manual;
                // Cargar el archivo de medios
                mediaPlayerBackgroundMusic.Play();
                // Cambiar el volumen (por ejemplo, aumentarlo en un 10%)
                mediaPlayerBackgroundMusic.Volume += 0.1; // Puedes ajustar este valor según tus necesidades
                // Actualizar el estado de reproducción
                isPlaying = true;
            }
            else
            {
                // Detener la reproducción
                mediaPlayerBackgroundMusic.Pause();
                // Actualizar el estado de reproducción
                isPlaying = false;
            }
        }

        // Método para reiniciar la reproducción cuando el archivo multimedia haya terminado
        private void MediaPlayerMediaEnded(object sender, EventArgs e)
        {
            // Si el archivo multimedia ha terminado y la reproducción está activada, reiniciar la reproducción
            if (isPlaying)
            {
                mediaPlayerBackgroundMusic.Position = TimeSpan.Zero; // Reiniciar la posición del reproductor al principio
                mediaPlayerBackgroundMusic.Play(); // Iniciar la reproducción nuevamente
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
    }
}
