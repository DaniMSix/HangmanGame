using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Views.Utils
{
    public class ButtonAnimation
    {
        public void ClickDarken(Button button, Action action)
        {
            button.Dispatcher.Invoke(() =>
            {
                button.Opacity = 0.3;
            });

            Thread.Sleep(2000);

            button.Dispatcher.Invoke(() =>
            {
                button.Opacity = 1.0;
            });

            action?.Invoke();
        }


    }
}
