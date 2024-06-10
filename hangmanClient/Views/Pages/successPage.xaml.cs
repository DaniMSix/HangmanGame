using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Views.Pages
{
    public partial class SuccessPage : Page
    {
        public event EventHandler MessageClosed;

        public SuccessPage(string title, string content)
        {
            InitializeComponent();
            txtTitle.Content = title;
            ((TextBlock)txtContent.Content).Text = content;
        }

        private void CloseMessage()
        {
            // Lógica para cerrar el mensaje de éxito
            MessageClosed?.Invoke(this, EventArgs.Empty);
        }

        // Llama a este método cuando quieras cerrar el mensaje
        private void SomeActionThatClosesTheMessage()
        {
            CloseMessage();
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            // Lógica para el clic del botón de aceptar
            SomeActionThatClosesTheMessage();
        }
    }
}
