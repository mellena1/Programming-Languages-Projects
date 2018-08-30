using System;
using System.Collections.Generic;
using System.Threading;
using comp3350_a7;

/*
* The simulation is run by an internal timer that sends out a 'tick'
* with a given interval. One tick from the timer means that each
* entity in the pasture should obtain a tick. When an entity obtains
* a tick, this entity is allowed to carry out their tasks according
* to what kind they are. This could mean moving the entity, making
* the entity starve from hunger, or producing a new offspring.
*/
public class Engine
{
    private int speed = 100;
    private int time = 0;
    private Pasture pasture;

    public Engine(Pasture pasture)
    {
        this.pasture = pasture;
    }

    public void RunActions()
    {
        IList<IEntity> queue = pasture.GetEntities();
        foreach (IEntity e in queue)
        {
            if (!e.isDead()) e.Tick();
        }
        time++;
    }

    public int Time
    {
        get { return time; }
        set { if (value >= 0) time = value; }
    }

    public int Speed
    {
        get { return Speed; }
        set { if (value > 0) speed = value; }
    }

    public void Run()
    {
        System.Diagnostics.Stopwatch clock = new System.Diagnostics.Stopwatch();
        clock.Start();
        while (!Organism.somethingExtinct())  // Stop if any organism goes extinct
        {
            RunActions();
            pasture.Refresh();
            Thread.Sleep(speed);
        }
        Console.WriteLine("Something went extinct!");
    }
}
