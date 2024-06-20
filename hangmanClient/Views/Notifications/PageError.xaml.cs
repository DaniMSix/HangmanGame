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

namespace Views.Pages
{
    /// <summary>
    /// Lógica de interacción para ErrorPage.xaml
    /// </summary>
    public partial class PageError : Page
    {
        public event EventHandler MessageClosed;
        public PageError(string title, string message)
        {
            InitializeComponent();
            lbTitleError.Content = title;
            tbMessageError.Text = message;
        }

        private void CloseMessage()
        {
            MessageClosed?.Invoke(this, EventArgs.Empty);
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            CloseMessage();
        }
    }
}
