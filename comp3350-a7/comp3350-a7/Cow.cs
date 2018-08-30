using System;
using System.Collections.Generic;
namespace comp3350_a7
{
    public class Cow : Animal, IEntity
    {
        private char symbol = 'C';
        private HashSet<Type> canEat;
        private bool alive;

        public Cow(Pasture pasture, int timeToMove, int timeUntilStarves, int timeUntilReproduce)
            : base(typeof(Cow), pasture, timeToMove, timeUntilStarves, timeUntilReproduce)
        {
            this.canEat = new HashSet<Type>();
            this.canEat.Add(typeof(Grass));
            this.alive = true;
        }

        char IEntity.GetSymbol()
        {
            return symbol;
        }

        bool IEntity.IsCompatible(IEntity otherEntity)
        {
            return !(otherEntity is Cow);
        }

        void IEntity.Tick()
        {
            // Move and eat
            AnimalTick(this, canEat);
            // Might've died after the AnimalTick call from starvation
            if (alive)
            {
                // Reproduce
                ISet<Point> free = pasture.GetFreeNeighbors(this);
                Point reproducePoint = Reproduce(free);
                if (reproducePoint != null) {
                    Cow newCow = new Cow(pasture, timeToMove, timeUntilStarves, timeUntilReproduce);
                    pasture.AddEntity(newCow, reproducePoint);
                }
            }
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
