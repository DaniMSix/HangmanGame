using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageSelectLanguage : Page
    {
        public event EventHandler MessageClosed;
        System.Windows.Controls.Frame frCurrentFrame;
        System.Windows.Controls.Frame frHome;
        string language;

        public PageSelectLanguage(System.Windows.Controls.Frame frCurrentFrame, 
            System.Windows.Controls.Frame frHome)
        {
            InitializeComponent();
            this.frCurrentFrame = frCurrentFrame;
            this.frHome = frHome;
        }

        private void NavigateToLogin()
        {
            Login loginPage = new Login(language);
            frHome.Navigate(loginPage);
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            SetLanguage(language);
            NavigateToLogin();
        }

        private void SetLanguage(string language)
        {
            if (language == "Ingles")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
            else
            {
                Console.WriteLine("................................w");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("");

            }
        }

        private void BtnMexicoClick(object sender, RoutedEventArgs e)
        {
            language = "Español";
        }

        private void BtnEUClick(object sender, RoutedEventArgs e)
        {
            language = "Ingles";
        }
    }
}
