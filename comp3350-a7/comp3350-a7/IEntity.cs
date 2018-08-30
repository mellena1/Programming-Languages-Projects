using System;

/*
 * This is the superclass of all entities in the pasture simulation
 * system. This interface must be implemented by all entities
 * that exist in the simulation of the pasture.
 */
public interface IEntity
{
    void Tick();

    bool IsCompatible(IEntity otherEntity);

    char GetSymbol();

    void setDead();

    bool isDead();
}
