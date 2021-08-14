using System.Collections.Generic;
using System.Timers;
using Poggers.EntityStates;
using Poggers.GameObjects.Weapons.Attacks;
using Poggers.Interfaces;

namespace Poggers.GameObjects.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private readonly Timer attackCooldownTimer;
        private readonly Timer attackWindupTimer;

        private float enduranceCost;
        private float cooldowntime = 100;
        private float winduptime = 100;
        private IArmedEntity owner;
        private IModel model;
        private Attack attack;
        private List<Attack> attacks;

        public Weapon(IModel model, IArmedEntity owner)
        {
            this.model = model;
            this.Owner = owner;
            this.attackCooldownTimer = new Timer();
            this.attackCooldownTimer.Elapsed += (_, __) => this.attackCooldownTimer.Stop();
            this.attackWindupTimer = new Timer();
            this.attackWindupTimer.Elapsed += this.EndWindup;
            this.InitializeAttacks();
            this.Attack = this.attacks[0];
        }

        public float EnduranceCost { get => this.enduranceCost; set => this.enduranceCost = value; }

        public float Cooldowntime { get => this.cooldowntime; set => this.cooldowntime = value; }

        public float Winduptime { get => this.winduptime; set => this.winduptime = value; }

        public float Range { get => this.Attack.Range; }

        public IModel Model { get => this.model; set => this.model = value; }

        public IArmedEntity Owner { get => this.owner; set => this.owner = value; }

        public Attack Attack { get => this.attack; set => this.attack = value; }

        public List<Attack> Attacks { get => this.attacks; set => this.attacks = value; }

        public bool IsUsable => !this.attackCooldownTimer.Enabled;

        public void StartAttack(int attack = 0)
        {
            if (attack >= this.attacks.Count)
            {
                return;
            }

            this.Attack = this.attacks[attack];
            this.owner.State = EntityState.Attacking;
            this.attackWindupTimer.Interval = this.Winduptime;
            this.attackWindupTimer.Start();
        }

        public void EndWindup(object source, ElapsedEventArgs e)
        {
            this.attackWindupTimer.Stop();
            if (!this.owner.State.Equals(EntityState.Dead))
            {
                this.Attack?.Start(); // check if attack1 is slash
                this.model.AddAttacks(this.Attack);
            }
        }

        public abstract void InitializeAttacks();

        public void StopAttack()
        {
            if (!this.owner.State.Equals(EntityState.Dead))
            {
                this.owner.State = EntityState.Idle;
            }

            this.model.RemoveAttacks(this.Attack);
            this.attackCooldownTimer.Interval = this.Cooldowntime;
            this.attackCooldownTimer.Start();
        }
    }
}
