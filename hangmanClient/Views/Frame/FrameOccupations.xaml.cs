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
    public partial class FrameOccupations : Page
    {
        private ManageGame manageGame;
        private string language;
        private int idCategory;
        public event EventHandler<int> WordSelected;
        public FrameOccupations(int idCategory, string language)
        {
            InitializeComponent();
            manageGame = new ManageGame(language);
            this.idCategory = idCategory;
            this.language = language;
            AssignButtonNames();
        }

        private void AssignButtonNames()
        {
            var words = manageGame.RecoveringWordsForCategory(5);
            var buttons = new Button[] { btnOccupationOne, btnOccupationTwo, btnOccupationThree, btnOccupationFour, btnOccupationFive, btnOccupationSix, btnOccupationSeven, btnOccupationEight, btnOccupationNine, btnOccupationTen };

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
                buttons[i].Tag = words[i].IdWord;
            }
        }

        private void BtnClickGetId(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag != null)
            {
                int wordId = (int)clickedButton.Tag;
                WordSelected?.Invoke(this, wordId);
            }
        }
    }
}
