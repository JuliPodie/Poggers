namespace Poggers.Interfaces
{
    public interface IOverlayController
    {
        public bool MenuIsOpen { get; }

        public void ToggleMenu();

        public void ShowDeathScreen();

        public void MainMenu(bool levelSelection);

        public void ResetHUD();

        public void LoadScreen();

        public void Win();

        public void StopLoading();

        public void StartGame();

        public void StartFadeIn();

        public void StartFadeOut();
    }
}
