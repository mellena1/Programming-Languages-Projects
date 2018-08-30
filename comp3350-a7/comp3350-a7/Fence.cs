using System;
namespace comp3350_a7
{
    public class Fence : IEntity
    {
        /* The symbol of this entity. */
        private char symbol = 'F';
        private bool alive;

        public Fence()
        {
            this.alive = true;
        }

        char IEntity.GetSymbol()
        {
            return symbol;
        }

        bool IEntity.IsCompatible(IEntity otherEntity)
        {
            return false;
        }

        void IEntity.Tick() {
            return;
        }

        void IEntity.setDead()
        {
            alive = false;
        }

        bool IEntity.isDead()
        {
            return !alive;
        }
    }
}
