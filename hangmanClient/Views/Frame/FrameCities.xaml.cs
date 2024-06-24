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
    public partial class FrameCities : Page
    {
        private ManageGame manageGame;
        private string language;
        private int idCategory;
        public event EventHandler<int> WordSelected;
        private SoundHelper soundHelper;

        public FrameCities(int idCategory, string language)
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
            var words = manageGame.RecoveringWordsForCategory(4);
            var buttons = new Button[] { btnCityOne, btnCityTwo, btnCityThree, btnCityFour, btnCityFive, btnCitySix, btnCitySeven, btnCityEight, btnCityNine, btnCityTen };

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
