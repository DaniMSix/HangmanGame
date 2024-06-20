using ServerManageGame;
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

namespace Views.Frame
{
    public partial class FrameInstruments : Page
    {
        ManageGame manageGame;
        string language;
        int idCategory;
        public FrameInstruments(int idCategory, string language)
        {
            InitializeComponent();
            manageGame = new ManageGame(language);
            this.idCategory = idCategory;
            this.language = language;
            AssignButtonNames();
        }
        private void AssignButtonNames()
        {
            var words = manageGame.RecoveringWordsForCategory(2);
            var buttons = new Button[] { btnInstrumentOne, btnInstrumentTwo, btnInstrumentThree, btnInstrumentFour, btnInstrumentFive, btnInstrumentSix, btnInstrumentSeven, btnInstrumentEight, btnInstrumentNine, btnInstrumentTen };

            for (int i = 0; i < words.Length && i < buttons.Length; i++)
            {
                if (language == "Ingles")
                {
                    buttons[i].Content = words[i].NameEn;
                }
                else
                {
                    buttons[i].Content = words[i].Name;
                }

            }
        }
    }
}
