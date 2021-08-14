namespace Poggers.EntityStates
{
    public static class EntityStateExtension
    {
        public static bool IsAttacking(this EntityState state)
        {
            return state == EntityState.Attacking;
        }

        public static bool IsDead(this EntityState state)
        {
            return state == EntityState.Dead;
        }

        public static bool IsDodging(this EntityState state)
        {
            return state == EntityState.Dodging;
        }

        public static bool IsIdle(this EntityState state)
        {
            return state == EntityState.Idle;
        }

        public static bool IsMoving(this EntityState state)
        {
            return state == EntityState.Moving;
        }

        public static bool IsRooted(this EntityState state)
        {
            return state == EntityState.Attacking || state == EntityState.Dodging || state == EntityState.Dead;
        }
    }
}
