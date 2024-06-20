using System;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageSuccess : Page
    {
        public event EventHandler MessageClosed;

        public PageSuccess(string title, string content)
        {
            InitializeComponent();
            txtTitle.Content = title;
            ((TextBlock)txtContent.Content).Text = content;
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
