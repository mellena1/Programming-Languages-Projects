using System;
using System.Collections.Generic;
using comp3350_a7;

/*
 * Note that Dummy is a pretty BAD example of object-oriented
 * programming. Instead of having separate classes for stationary and
 * mobile dummies, they are distinguished using the flag "alive".  You
 * probably do not want to base your solution on this class.
 */


public class Dummy : IEntity
{
    /* The symbol of this entity. */
    private char symbol = 'D';
    /* The pasture this entity is in. */
    protected Pasture pasture;
    /* The number of ticks this entity should get before moving. */
    protected int moveDelay;

    protected bool alive = true;

    /*
     * Creates a new instance of this class, with the given pasture as
     * its pasture.
     */
    public Dummy(Pasture pasture)
    {
        this.pasture = pasture;
        moveDelay = 10;
    }

    /*
     * Creates a new instance of this class, with the given pasture as
     * its pasture, and its status as a moving entity.
     */
    public Dummy(Pasture pasture, bool alive)
    {
        this.pasture = pasture;
        this.alive = alive;
        moveDelay = 10;
    }

    /*
     * Performs the relevant actions of this entity, depending on what
     * kind of entity it is.
     */
    public void Tick()
    {
        if (alive)
            moveDelay--;

        if (moveDelay == 0)
        {
            Point neighbor = GetRandomMember(pasture.GetFreeNeighbors(this));

            if (neighbor != null)
                pasture.MoveEntity(this, neighbor);

            moveDelay = 10;
        }
    }

    /*
     * Tests if this entity can be on the same position in the pasture
     * as the given one.
     */
    public bool IsCompatible(IEntity otherEntity) { return false; }

    protected static X GetRandomMember<X>(ICollection<X> c)
    {
        if (c.Count == 0)
            return default(X);

        var it = c.GetEnumerator();
        int n = new Random().Next(0, c.Count);

        while (n-- > 0)
        {
            it.MoveNext();
        }

        it.MoveNext();
        return it.Current;
    }

    public char GetSymbol()
    {
        return symbol;
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
