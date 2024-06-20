using ServerManageGame;
using System;
using System.Windows.Controls;

namespace Views.Frame
{
    public partial class FrameFruits : Page
    {
        private ManageGame manageGame;
        private string language;
        private int idCategory;
        public event EventHandler<int> WordSelected;
        public FrameFruits(int idCategory, string language)
        {
            InitializeComponent();
            manageGame = new ManageGame(language);
            this.idCategory = idCategory;
            this.language = language;
            AssignButtonNames();
        }

        private void AssignButtonNames()
        {
            var words = manageGame.RecoveringWordsForCategory(3);
            var buttons = new Button[] { btnFruitOne, btnFruitTwo, btnFruitThree, btnFruitFour, btnFruitFive, btnFruitSix, btnFruitSeven, btnFruitEight, btnFruitNine, btnFruitTen };

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
