using Poggers.Interfaces;

namespace Poggers.Overlays
{
    public class OverlayController : IOverlayController
    {
        private static IOverlay hud;
        private static IOverlayWithInput inGameMenu;
        private static IOverlay win;
        private static IOverlayWithInput death;
        private static ILayeredMenu mainMenu;
        private static IOverlay load;
        private static IOverlayEvent fadeEvent;
        private readonly IModel model;
        private bool menu;

        public OverlayController(IModel model)
        {
            this.model = model;
            hud = new Hud(model);
            inGameMenu = new InGameMenu(model);
            win = new InfoWindow(model, 2);
            death = new InfoWindow(model, 1);
            load = new InfoWindow(model, 0);
            mainMenu = new MainMenu(model);
            fadeEvent = new FadeEvent();
        }

        public bool MenuIsOpen => this.menu;

        public void StartGame()
        {
            this.menu = false;
            this.model.Overlays.Add(hud);
            this.StartFadeIn();
        }

        public void ResetHUD()
        {
            this.model.Overlays.Clear();

            // Unsubscribe from all OverlayWithInputs
            death.Hide();
            inGameMenu.Hide();
            mainMenu.Hide();
        }

        public void ToggleMenu()
        {
            if (!this.menu)
            {
                this.model.Overlays.Add(inGameMenu);
                this.model.PauseGame();
                inGameMenu.Show();
            }
            else
            {
                this.model.Overlays.Remove(inGameMenu);
                this.model.ResumeGame();
                inGameMenu.Hide();
            }

            this.menu = !this.menu;
        }

        public void LoadScreen()
        {
            mainMenu.Hide();
            this.model.Overlays.Remove(mainMenu);
            this.model.PauseGame();
            this.model.Overlays.Add(load);
        }

        public void Win()
        {
            this.model.PauseGame();
            this.model.Overlays.Add(win);
        }

        public void StopLoading()
        {
            this.model.Overlays.Remove(load);
            this.model.ResumeGame();
        }

        public void ShowDeathScreen()
        {
            this.model.PauseGame();
            inGameMenu.Hide();
            death.Show();
            this.model.Overlays.Add(death);
        }

        public void MainMenu(bool levelSelection)
        {
            mainMenu.OpenLayer(levelSelection ? 1 : 0);
            this.model.Overlays.Add(mainMenu);
        }

        public void StartFadeIn()
        {
            fadeEvent.Start(0);

            // FadeIn should always be drawn first
            this.model.Overlays.Insert(0, fadeEvent);
        }

        public void StartFadeOut()
        {
            this.model.Overlays.Remove(hud);
            fadeEvent.Start(1);
        }
    }
}
