using ServerManageGame;
using System.Linq;
using System.Windows.Controls;

namespace Views.Frame
{
    public partial class FrameAnimals : Page
    {
        ManageGame manageGame;
        string language;
        int idCategory;
        public FrameAnimals(int idCategory, string language)
        {
            InitializeComponent();
            manageGame = new ManageGame(language);
            this.idCategory = idCategory;
            this.language = language;
            AssignButtonNames();
        }

        
        private void AssignButtonNames()
        {
            var words = manageGame.RecoveringWordsForCategory(1);
            var buttons = new Button[] { btnAnimalOne, btnAnimalTwo, btnAnimalThree, btnAnimalFour, btnAnimalFive, btnAnimalSix, btnAnimalSeven, btnAnimalEight, btnAnimalNine, btnAnimalTen };

            for (int i = 0; i < words.Length && i < buttons.Length; i++)
            {
                if ( language == "Ingles")
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
