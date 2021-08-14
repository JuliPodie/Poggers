using System.Collections.Generic;
using Poggers.GameObjects;
using Poggers.GameObjects.Entities;
using Poggers.GameObjects.Entities.Enemies;
using Poggers.GameObjects.Items;
using Poggers.GameObjects.Weapons.Attacks;

namespace Poggers.Interfaces
{
    public interface IModel
    {
        public List<GameObject> GameObjects { get; }

        public List<IOverlay> Overlays { get; }

        public Player Player { get; set; }

        public BossCeolmaer Boss { get; set; }

        public IOverlayController OverlayController { get; set; }

        public void AddAttacks(Attack attack);

        public void RemoveAttacks(Attack attack);

        public void AddItem(IItem item);

        public void LoadMainMenu(bool levelSelection = false);

        public void ResetGame();

        public void LoadGame(int level);

        public void PauseGame();

        public void ResumeGame();

        public void FinishedGame();
    }
}
