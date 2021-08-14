using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Poggers.GameObjects.Entities;
using Poggers.Input;
using Poggers.Interfaces;

namespace Poggers.Overlays
{
    public class MainMenu : ILayeredMenu
    {
        private readonly IModel model;
        private MenuButton[] options;
        private int selected;
        private bool levelmenu;

        public MainMenu(IModel model)
        {
            this.model = model;
        }

        public void UpdateMenu(GameWindow window, KeyboardKeyEventArgs args)
        {
            this.options[this.selected].UnHighlight();
            if (args.Key == Keys.Up)
            {
                this.selected--;
            }

            if (args.Key == Keys.Down)
            {
                this.selected++;
            }

            this.selected = ((this.selected % this.options.Length) + this.options.Length) % this.options.Length; // use twice to prevent negative numbers
            this.options[this.selected].Highlight();

            if (args.Key == Keys.Enter)
            {
                if (this.levelmenu)
                {
                    this.model.LoadGame(this.selected);
                }
                else
                {
                    this.CreateMenuLevel();

                    // this.model.BlendIn();
                    this.levelmenu = true;
                }
            }
        }

        public void Show()
        {
            this.selected = 0;
            InputProvider.Instance.SubscribeDown(this.UpdateMenu);
        }

        public void Hide()
        {
            InputProvider.Instance.UnsubscribeDown(this.UpdateMenu);
        }

        public void OpenLayer(int layer)
        {
            this.levelmenu = layer == 1;
            if (this.levelmenu)
            {
                this.CreateMenuLevel();
            }
            else
            {
                this.CreateStartScreen();
            }

            this.Show();
        }

        public void Draw(float windowRatio)
        {
            foreach (MenuButton option in this.options)
            {
                option.Draw(option.Center, windowRatio);
            }
        }

        private void CreateStartScreen()
        {
            this.options = new MenuButton[1] { new MenuButton(new Vector2(0, 0), 1.5f, 0.6f, false, 0) };
        }

        private void CreateMenuLevel()
        {
            this.selected = 0;
            this.options = new MenuButton[3]
            {
                new MenuButton(new Vector2(0, 0.5f), 0.5f, 0.2f, true, 1),
                new MenuButton(new Vector2(0, 0.0f), 0.5f, 0.2f, false, 5),
                new MenuButton(new Vector2(0, -0.5f), 0.5f, 0.2f, false, 9),
            };
        }
    }
}