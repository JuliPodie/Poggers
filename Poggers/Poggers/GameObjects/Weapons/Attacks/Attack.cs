using System;
using System.Collections.Generic;
using System.Timers;

namespace Poggers.GameObjects.Weapons.Attacks
{
    public abstract class Attack
    {
        private readonly float range;
        private readonly int updateRepetitions;
        private readonly Timer stopTimer;
        private readonly Timer intervalTimer;
        private IWeapon weapon;
        private float damage;
        private int duration;

        private List<IAttackComponent> hitboxes = new List<IAttackComponent>();

        public Attack(IWeapon weapon, float damage, int duration, float range)
        {
            this.Weapon = weapon;
            this.Damage = damage;
            this.Duration = duration;
            this.range = range;
            this.updateRepetitions = 30;
            this.stopTimer = new Timer(duration);
            this.stopTimer.Elapsed += (_, __) => this.Stop();

            this.intervalTimer = new Timer(this.duration / this.updateRepetitions);
            this.intervalTimer.Elapsed += (_, __) => this.Update();
        }

        public float Damage { get => this.damage; set => this.damage = value; }

        public int Duration { get => this.duration; set => this.duration = value; }

        public IWeapon Weapon { get => this.weapon; set => this.weapon = value; }

        public float Range => this.range;

        public int UpdateRepetitions => this.updateRepetitions;

        public List<IAttackComponent> Hitboxes { get => this.hitboxes; set => this.hitboxes = value; }

        public abstract void InitializeHitboxes();

        public void Start()
        {
            this.InitializeHitboxes();
            this.stopTimer.Start();
            this.intervalTimer.Start();
        }

        public void Update()
        {
            try
            {
                this.Hitboxes.ForEach(hitbox => hitbox.Update());
            }
            catch (Exception)
            {
            }
        }

        public void Stop()
        {
            this.stopTimer.Stop();
            this.intervalTimer.Stop();
            this.hitboxes.Clear();
            this.Weapon.StopAttack();
        }
    }
}
