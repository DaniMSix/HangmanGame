using System.Windows;

namespace Views
{
    public partial class App : Application
    {
        App()
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("");
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
        }
        /*protected override void OnStartup(StartupEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); 


            /*
             * String idioma = "";
            try
            {
                idioma = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Resources-en.txt").Substring(0, 5);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Resources.txt", "es-MX");
            }
            catch (ArgumentOutOfRangeException)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Resources.txt", "es-MX");
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("");
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            if (idioma.Equals("en-US"))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
            }
             
        }*/
    }
}

