using System;
using System.Collections.Generic;
namespace comp3350_a7
{
    public class Grass : Organism, IEntity
    {
        private char symbol = 'g';
        private Pasture pasture;
        private bool alive;

        public Grass(Pasture pasture, int timeUntilReproduce)
            : base(typeof(Grass), timeUntilReproduce)
        {
            this.pasture = pasture;
            this.alive = true;
        }

        char IEntity.GetSymbol()
        {
            return symbol;
        }

        bool IEntity.IsCompatible(IEntity otherEntity)
        {
            return !(otherEntity is Grass);
        }

        void IEntity.Tick()
        {
            ISet<Point> free = pasture.GetFreeNeighbors(this);
            Point newgrasspoint = Reproduce(free);

            if (newgrasspoint != null)
                pasture.AddEntity(new Grass(pasture, timeUntilReproduce), newgrasspoint);
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
