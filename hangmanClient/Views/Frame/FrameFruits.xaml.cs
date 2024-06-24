using ServerManageGame;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Views.Frame
{
    public partial class FrameFruits : Page
    {
        private ManageGame manageGame;
        private string language;
        private int idCategory;
        public event EventHandler<int> WordSelected;
        private SoundHelper soundHelper;
        public FrameFruits(int idCategory, string language)
        {
            InitializeComponent();
            manageGame = new ManageGame(language);
            this.idCategory = idCategory;
            this.language = language;
            AssignButtonNames();
            soundHelper = new SoundHelper();
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
            soundHelper.PlayBackgroundMusic(@"C:\Users\DMS19\OneDrive\Escritorio\Github\Juego\HangmanGame\hangmanClient\Views\Music\button-sound.mp3");
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag != null)
            {
                int wordId = (int)clickedButton.Tag;
                WordSelected?.Invoke(this, wordId);

                foreach (Button button in GetAllButtons())
                {
                    button.Opacity = 1.0;
                }
                clickedButton.Opacity = 0.5;
            }
        }

        private IEnumerable<Button> GetAllButtons()
        {
            var buttons = new List<Button>();
            foreach (var child in gridButtons.Children)
            {
                if (child is Button button)
                {
                    buttons.Add(button);
                }
            }
            return buttons;
        }
    }
}
