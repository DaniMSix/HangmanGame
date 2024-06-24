using ServerManageGame;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Views.Frame
{
    public partial class FrameOccupations : Page
    {
        private ManageGame manageGame;
        private string language;
        private int idCategory;
        public event EventHandler<int> WordSelected;
        private SoundHelper soundHelper;

        public FrameOccupations(int idCategory, string language)
        {
            InitializeComponent();
            soundHelper = new SoundHelper();
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

        private void BtnClickGetId(object sender, RoutedEventArgs e)
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
