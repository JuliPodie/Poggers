using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using OpenTK.Windowing.Desktop;
using Poggers.Collision;
using Poggers.EntityStates;
using Poggers.GameObjects;
using Poggers.GameObjects.Entities;
using Poggers.GameObjects.Entities.Enemies;
using Poggers.GameObjects.Items;
using Poggers.GameObjects.Weapons.Attacks;
using Poggers.Interfaces;
using Poggers.Overlays;

namespace Poggers
{
    public class Model : IModel
    {
        private GameWindow window;
        private bool gameRun;
        private Player player;
        private BossCeolmaer boss;
        private IOverlayController overlayController;
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<Attack> attacks = new List<Attack>();
        private List<IOverlay> overlays = new List<IOverlay>();

        public Model(GameWindow window)
        {
            this.window = window;
            this.GameRun = false;
            this.OverlayController = new OverlayController(this);
            this.window.UpdateFrame += _ => this.Update();
            this.window.UpdateFrame += _ => this.UpdateEntities();
            this.LoadMainMenu();
        }

        public bool GameRun { get => this.gameRun; set => this.gameRun = value; }

        public Player Player { get => this.player; set => this.player = value; }

        public BossCeolmaer Boss { get => this.boss; set => this.boss = value; }

        public IOverlayController OverlayController { get => this.overlayController; set => this.overlayController = value; }

        public List<GameObject> GameObjects { get => this.gameObjects; set => this.gameObjects = value; }

        public List<Attack> Attacks { get => this.attacks; set => this.attacks = value; }

        public List<IOverlay> Overlays { get => this.overlays; set => this.overlays = value; }

        public void LoadMainMenu(bool levelSelection = false)
        {
            this.ResetGame();
            this.overlayController.MainMenu(levelSelection);
        }

        public void PauseGame()
        {
            this.GameRun = false;
        }

        public void ResumeGame()
        {
            this.GameRun = true;
        }

        public void ResetGame()
        {
            this.Player?.Unsubscribe();

            this.PauseGame();

            this.Player = null;
            this.GameObjects.Clear();
            this.OverlayController.ResetHUD();
        }

        public void FinishedGame()
        {
            this.boss = null;
            this.Player?.Unsubscribe();
            this.Player.Direction = Directions.Direction.S;
            this.overlayController.StartFadeOut();
            Timer end = new Timer();
            end.Interval = 1000;
            end.Elapsed += this.EndScreen;
            end.Start();
        }

        public void EndScreen(object source, ElapsedEventArgs e)
        {
            this.OverlayController.Win();
        }

        public void LoadGame(int level)
        {
            // Run async
            this.LoadLevel(level);
        }

        public void AddAttacks(Attack attack)
        {
            this.Attacks.Add(attack);
        }

        public void RemoveAttacks(Attack attack)
        {
            this.Attacks.Remove(attack);
        }

        public void AddItem(IItem item)
        {
            this.gameObjects.Add(item as GameObject);
        }

        public void UpdateEntities()
        {
            if (!this.GameRun)
            {
                return;
            }

            List<GameObject> gameObjtemp = new List<GameObject>(this.GameObjects);
            foreach (BasicEntity entity in gameObjtemp.OfType<BasicEntity>())
            {
                entity.Move();

                if (entity is IArmedEntity armed)
                {
                    armed.Attack();
                }
            }
        }

        public void Update()
        {
            if (!this.GameRun)
            {
                return;
            }

            List<GameObject> toRemove = new List<GameObject>();

            List<GameObject> gameObjtemp = new List<GameObject>(this.GameObjects);
            Attack[] attackCopy = new Attack[this.Attacks.Count];
            this.Attacks.CopyTo(attackCopy);

            foreach (Attack attack in attackCopy)
            {
                foreach (ICollidable hitbox in attack.Hitboxes.ToArray())
                {
                    foreach (ICollidable objects in gameObjtemp)
                    {
                        if (hitbox.CollidesWith(objects) && !attack.Weapon.Owner.Equals(objects))
                        {
                            if (objects is BasicEntity entity)
                            {
                                entity.TakeDamage(attack.Damage);
                                if (entity.State.IsDead())
                                {
                                    toRemove.Add(entity);
                                }
                            }
                            else if (objects is Wall wall)
                            {
                                if (wall.DestroyAble)
                                {
                                    wall.Destroyed = true;
                                    toRemove.Add(wall);
                                }
                            }
                        }
                    }
                }
            }

            foreach (BasicEntity entity in gameObjtemp.OfType<BasicEntity>())
            {
                if (entity.State.IsDead())
                {
                    continue;
                }

                if (entity.State.IsMoving() || entity.State.IsDodging())
                {
                    entity.MoveX();
                    foreach (ICollidable collidable in gameObjtemp)
                    {
                        if (entity != collidable && collidable.CollidesWith(entity))
                        {
                            if (!(collidable is GridBox) || collidable is Wall)
                            {
                                if (!this.HandleCollision(entity, collidable, toRemove))
                                {
                                    entity.RevertMovementX(collidable);
                                    break;
                                }
                            }

                            if (collidable is GridBox box)
                            {
                                if (box.GetEnd())
                                {
                                    this.LoadMainMenu(true);
                                }
                            }
                        }
                    }

                    entity.MoveY();
                    foreach (ICollidable collidable in gameObjtemp)
                    {
                        if (entity != collidable && collidable.CollidesWith(entity))
                        {
                            if (!(collidable is GridBox) || collidable is Wall)
                            {
                                if (!this.HandleCollision(entity, collidable, toRemove))
                                {
                                    entity.RevertMovementY(collidable);
                                    break;
                                }
                            }

                            if (collidable is GridBox box)
                            {
                                if (box.GetEnd())
                                {
                                    this.LoadMainMenu(true);
                                }
                            }
                        }
                    }

                    entity.FinishedMovement();
                }
            }

            toRemove.ForEach(x => this.GameObjects.Remove(x));
        }

        private async void LoadLevel(int level)
        {
            this.OverlayController.LoadScreen();
            await Task.Run(() => new World(this, level));
            this.OverlayController.StopLoading();
            this.OverlayController.StartGame();
        }

        /// <summary>
        /// This method handles the collision between an Entity and a collidable Object.
        /// </summary>
        /// <param name="entity">The Entity which caused the collision.</param>
        /// <param name="collidable">The collidable Object which caused the collision.</param>
        /// <param name="toRemove">The List, where objects cna be staged for later removal.</param>
        /// <returns>true, if the entity is able to move there. False, if not.</returns>
        private bool HandleCollision(BasicEntity entity, ICollidable collidable, List<GameObject> toRemove)
        {
            if (collidable is IItem item)
            {
                if (entity is IItemConsumer consumer && !toRemove.Contains(item as GameObject))
                {
                    consumer.AddItem(item);
                    toRemove.Add(item as GameObject);
                }

                return true;
            }

            if (entity is BasicEntity && !(entity is IArmedEntity) && collidable is Player p)
            {
                p.TakeDamage(1);
            }

            return false;
        }
    }
}
