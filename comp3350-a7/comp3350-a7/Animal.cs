using System;
using System.Collections.Generic;
namespace comp3350_a7
{
    public abstract class Animal : Organism
    {
        protected Pasture pasture;
        protected int timeToMove;
        protected int timeSinceMoved;
        protected int timeUntilStarves;
        protected int timeSinceEaten;

        public Animal(Type animalType, Pasture pasture, int timeToMove, int timeUntilStarves, int timeUntilReproduce)
            : base(animalType, timeUntilReproduce)
        {
            this.pasture = pasture;
            this.timeToMove = timeToMove;
            this.timeSinceMoved = 0;
            this.timeUntilStarves = timeUntilStarves;
            this.timeSinceEaten = 0;
        }

        protected Point Move(ISet<Point> freeSpaces) {
            if (timeSinceMoved < timeToMove)
            {
                timeSinceMoved++;
                return null;
            }

            if (freeSpaces.Count == 0) return null;
            int spaceToMoveTo = randomNum.Next(0, freeSpaces.Count);

            int count = 0;
            Point p = null;
            foreach (var point in freeSpaces)
            {
                if (count == spaceToMoveTo)
                {
                    p = point;
                    break;
                }
                count++;   
            }

            timeSinceMoved = 0;
            return p;
        }

        protected IEntity Eat(Point p, HashSet<Type> canEat)
        {
            IList<IEntity> entitiesHere = pasture.GetEntitiesAt(p);
            foreach (var entity in entitiesHere)
            {
                if (canEat.Contains(entity.GetType())) {
                    return entity;
                }
            }
            return null;
        }

        protected void AnimalTick(IEntity animal, HashSet<Type> canEat) {
            // Move
            ISet<Point> free = pasture.GetFreeNeighbors(animal);
            Point movePoint = Move(free);
            if (movePoint != null) {
                pasture.MoveEntity(animal, movePoint);
            }
            // Eat
            Point currentPoint = pasture.GetPosition(animal);
            IEntity food = Eat(currentPoint, canEat);
            if (food != null)
            {
                pasture.RemoveEntity(food);
                timeSinceEaten = 0;
            } else {
                timeSinceEaten++;
                if (timeSinceEaten >= timeUntilStarves)
                {
                    pasture.RemoveEntity(animal);
                }
            }
        }
    }
}
