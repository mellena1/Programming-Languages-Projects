using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using comp3350_a7;

/*
* A pasture contains cows, tigers, fences, grass, and possibly
* other entities. These entities move around in the pasture and try
* to find food, other entities of the same kind and run away from
* possible enimies. 
*/

public class Pasture
{

    private int width = 60;
    private int height = 20;

    private int dummys = 0;
    private int tigers = 200;
    private int cows = 400;
    private int grass = 20;
    private int fences;

    private Engine engine;
    private ISet<IEntity> world = new HashSet<IEntity>();
    private Dictionary<Point, List<IEntity>> grid = new Dictionary<Point, List<IEntity>>();
    private Dictionary<IEntity, Point> point = new Dictionary<IEntity, Point>();

    /** 
     * Creates a new instance of this class and places the entities in
     * it on random positions.
     */
    public Pasture()
    {
        
        engine = new Engine(this);

        /* The pasture is surrounded by a fence. Replace Dummy for
         * Fence when you have created that class */
        for (int i = 0; i < width; i++)
        {
            AddEntity(new Fence(), new Point(i, 0));
            AddEntity(new Fence(), new Point(i, this.height - 1));
        }
        for (int i = 1; i < height - 1; i++)
        {
            AddEntity(new Fence(), new Point(0, i));
            AddEntity(new Fence(), new Point(width - 1, i));
        }

        /* 
         * Now insert the right number of different entities in the
         * pasture.
         */
        for (int i = 0; i < dummys; i++)
        {
            IEntity dummy = new Dummy(this, true);
            Point p = GetFreePosition(dummy);
            if (p.Valid())
                AddEntity(dummy, p);
        }

        for (int i = 0; i < grass; i++)
        {
            IEntity grass = new Grass(this, 1);
            Point p = GetFreePosition(grass);
            if (p.Valid())
                AddEntity(grass, p);
        }

        for (int i = 0; i < tigers; i++)
        {
            IEntity tiger = new Tiger(this, 3, 8, 16);
            Point p = GetFreePosition(tiger);
            if (p.Valid())
                AddEntity(tiger, p);
        }

        for (int i = 0; i < cows; i++)
        {
            IEntity cow = new Cow(this, 2, 9, 7);
            Point p = GetFreePosition(cow);
            if (p.Valid())
                AddEntity(cow, p);
        }


        engine.Run();
    }

    public void Refresh()
    {
        Console.SetCursorPosition(0, 0);
        StringBuilder sb = new StringBuilder();

        for (int h = 0; h < height; ++h)
        {
            for (int w = 0; w < width; ++w)
            {
                Point p = new Point(w, h);
                if (!grid.ContainsKey(p))
                {
                    sb.Append(' ');
                    continue;
                }
                List<IEntity> l = grid[p];
                if (l.Count == 0)
                    sb.Append(' ');
                else if (l.Count == 1)
                {
                    sb.Append(l[0].GetSymbol());
                }
                else
                    sb.Append('@');
            }
            sb.AppendLine();
        }

        sb.AppendLine(Organism.getOrganismAmts());
        sb.AppendLine();
        sb.AppendLine("time: " + engine.Time);

        Console.Write(sb);

    }

    /*
     * Returns a random free position in the pasture if there exists
     * one.
     * 
     * If the first random position turns out to be occupied, the rest
     * of the board is searched to find a free position. 
     */
    private Point GetFreePosition(IEntity toPlace)
    {
        Point position = new Point(new Random().Next(0, width),
        (new Random().Next(0, height)));
        int startX = position.X;
        int startY = position.Y;

        int p = startX + startY * width;
        int m = height * width;
        int q = 97; //any large prime will do

        for (int i = 0; i < m; i++)
        {
            int j = (p + i * q) % m;
            int x = j % width;
            int y = j / width;

            position = new Point(x, y);
            bool free = true;

            IList<IEntity> c = GetEntitiesAt(position);
            if (c != null)
            {
                foreach (IEntity thisThing in c)
                {
                    if (!toPlace.IsCompatible(thisThing) || !thisThing.IsCompatible(toPlace))
                    {
                        free = false; break;
                    }
                }
            }
            if (free) return position;
        }

        return new Point(-1, -1);
    }


    public Point GetPosition(IEntity e)
    {
        return point[e];
    }

    /*
     * Add a new entity to the pasture.
     */
    public void AddEntity(IEntity entity, Point pos)
    {
        world.Add(entity);

        if (!grid.ContainsKey(pos))
        {
            List<IEntity> newl = new List<IEntity>();
            grid.Add(pos, newl);
        }

        List<IEntity> l = grid[pos];
        l.Add(entity);
        point.Add(entity, pos);
    }

    public void MoveEntity(IEntity e, Point newPos)
    {
        Point oldPos = point[e];
        List<IEntity> l = grid[oldPos];
        l.Remove(e);
        point.Remove(e);
        if (!grid.ContainsKey(newPos)) {
            List<IEntity> newl = new List<IEntity>();
            grid.Add(newPos, newl);
        }
        l = grid[newPos];
        l.Add(e);

        point.Add(e, newPos);
    }

    /*
     * Remove the specified entity from this pasture.
     */
    public void RemoveEntity(IEntity entity)
    {
        entity.setDead();
        Organism.addToOrganismAmt(entity.GetType(), -1);
        Point p = point[entity];
        world.Remove(entity);
        grid[p].Remove(entity);
        point.Remove(entity);
    }

    /*
     * Various methods for getting information about the pasture
     */

    public IList<IEntity> GetEntities()
    {
        return new List<IEntity>(world);
    }

    public IList<IEntity> GetEntitiesAt(Point lookAt)
    {
        if (grid.ContainsKey(lookAt))
            return new List<IEntity>(grid[lookAt]);

        return null;
    }

    public ISet<Point> GetFreeNeighbors(IEntity entity)
    {
        ISet<Point> free = new HashSet<Point>();
        Point p;

        int entityX = GetEntityPosition(entity).X;
        int entityY = GetEntityPosition(entity).Y;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                p = new Point(entityX + x, entityY + y);
                if (FreeSpace(p, entity))
                    free.Add(p);
            }
        }
        return free;
    }

    private bool FreeSpace(Point p, IEntity e)
    {
        if (!grid.ContainsKey(p))
            return true;
        List<IEntity> l = grid[p];
        foreach (IEntity old in l)
            if (!old.IsCompatible(e))
                return false;
        return true;
    }

    public Point GetEntityPosition(IEntity entity)
    {
        return point[entity];
    }

    /** The method for C# to run. */
    public static void Main(String[] args)
    {
        Pasture p = new Pasture();
    }


}
