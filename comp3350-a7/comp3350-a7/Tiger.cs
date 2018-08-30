using System;
using System.Collections.Generic;
namespace comp3350_a7
{
    public class Tiger : Animal, IEntity
    {
        private char symbol = 'T';
        private HashSet<Type> canEat;
        private bool alive;

        public Tiger(Pasture pasture, int timeToMove, int timeUntilStarves, int timeUntilReproduce)
            : base(typeof(Tiger), pasture, timeToMove, timeUntilStarves, timeUntilReproduce)
        {
            canEat = new HashSet<Type>();
            canEat.Add(typeof(Cow));
            this.alive = true;
        }

        char IEntity.GetSymbol()
        {
            return symbol;
        }

        bool IEntity.IsCompatible(IEntity otherEntity)
        {
            return !(otherEntity is Tiger);
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
                    Tiger newTiger = new Tiger(pasture, timeToMove, timeUntilStarves, timeUntilReproduce);
                    pasture.AddEntity(newTiger, reproducePoint);
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
