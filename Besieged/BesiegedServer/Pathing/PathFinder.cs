﻿using Framework;
using Framework.Map.Tile;
using Framework.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BesiegedServer.Pathing
{
    public sealed class PriorityQueue<P, V>
    {
        private SortedDictionary<P, Queue<V>> list = new SortedDictionary<P, Queue<V>>();

        public void Enqueue(P priority, V value)
        {
            Queue<V> q;
            if (!list.TryGetValue(priority, out q))
            {
                q = new Queue<V>();
                list.Add(priority, q);
            }
            q.Enqueue(value);
        }

        public V Dequeue()
        {
            // will throw if there isn’t any first element!
            var pair = list.First();
            var v = pair.Value.Dequeue();
            if (pair.Value.Count == 0) // nothing left of the top priority.
                list.Remove(pair.Key);
            return v;
        }

        public bool IsEmpty
        {
            get { return !list.Any(); }
        }
    }

    public class PathFinder
    {
        public GameState Board { get; set; }

        public PathFinder(GameState gameState)
        {
            Board = gameState;
        }
        public List<Tuple<int,int>> GetNeighbours(int x, int y)
        {
            List<Tuple<int, int>> tiles = new List<Tuple<int, int>>();

            var map = Board.GameBoard;
            var units = Board.Units;
            if (x < 0 || x > map.MapLength || y < 0 || y > map.MapHeight) return tiles;

            if (!(x - 1 < 0) && map.Tiles[y][x - 1].IsPassable && !units.Any(unit => unit.X_Position == x-1 && unit.Y_Position == y))
            {
                tiles.Add(new Tuple<int,int>(y,x - 1));
            }
            if (!(x + 1 >= map.MapLength) && map.Tiles[y][x + 1].IsPassable && !units.Any(unit => unit.X_Position == x + 1 && unit.Y_Position == y))
            {
                tiles.Add(new Tuple<int, int>(y, x + 1));
            }
            if (!(y - 1 < 0) && map.Tiles[y - 1][x].IsPassable && !units.Any(unit => unit.X_Position == x && unit.Y_Position == y - 1))
            {
                tiles.Add(new Tuple<int, int>(y - 1, x));
            }
            if (!(y + 1 >= map.MapHeight) && map.Tiles[y + 1][x].IsPassable && !units.Any(unit => unit.X_Position == x && unit.Y_Position == y + 1))
            {
                tiles.Add(new Tuple<int, int>(y + 1, x));
            }
            return tiles;
        }

        public List<Tuple<int, int>> GetHostileNeighbours(int x, int y)
        {
            List<Tuple<int, int>> tiles = new List<Tuple<int, int>>();

            var map = Board.GameBoard;
            var units = Board.Units;
            if (x < 0 || x > map.MapLength || y < 0 || y > map.MapHeight) return tiles;

            if (!(x - 1 < 0) && map.Tiles[y][x - 1].IsPassable)
            {
                tiles.Add(new Tuple<int, int>(y, x - 1));
            }
            if (!(x + 1 >= map.MapLength) && map.Tiles[y][x + 1].IsPassable)
            {
                tiles.Add(new Tuple<int, int>(y, x + 1));
            }
            if (!(y - 1 < 0) && map.Tiles[y - 1][x].IsPassable)
            {
                tiles.Add(new Tuple<int, int>(y - 1, x));
            }
            if (!(y + 1 >= map.MapHeight) && map.Tiles[y + 1][x].IsPassable)
            {
                tiles.Add(new Tuple<int, int>(y + 1, x));
            }
            return tiles;
        }

        public int FindPath(int startX, int startY, int destinationX, int destinationY)
        {
            Tuple<int, int> start = new Tuple<int, int>(startY,startX);

            var closed = new HashSet<Tuple<int, int>>();
            var queue = new PriorityQueue<double, Path<Tuple<int, int>>>();
            Tuple<int, int> destination = new Tuple<int, int>(destinationY, destinationX);
            queue.Enqueue(0, new Path<Tuple<int, int>>(start));

            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                    continue;
                if (path.LastStep.Equals(destination))
                    return path.TotalCost;
                closed.Add(path.LastStep);
                foreach (var n in GetNeighbours(path.LastStep.Item2, path.LastStep.Item1))
                {
                    var newPath = path.AddStep(n, 1);
                    queue.Enqueue(newPath.TotalCost, newPath);
                }
            }
            return -1;
        }

        public bool HasAttackableTargets(IUnit unit)
        {
            Tuple<int, int> start = new Tuple<int, int>(unit.Y_Position, unit.X_Position);

            var closed = new HashSet<Tuple<int, int>>();
            var queue = new PriorityQueue<double, Path<Tuple<int, int>>>();

            queue.Enqueue(0, new Path<Tuple<int, int>>(start));

            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                    continue;

                closed.Add(path.LastStep);
                foreach (var n in GetHostileNeighbours(path.LastStep.Item2, path.LastStep.Item1))
                {
                    var newPath = path.AddStep(n, 1);
                    if (newPath.TotalCost <= unit.Range)
                    {
                        queue.Enqueue(newPath.TotalCost, newPath);
                    }
                }
            }

            return (from t in closed let units = Board.Units where units.Any(u => u.X_Position == t.Item2 && u.Y_Position == t.Item1 && u.Owner != unit.Owner) select t).Any();
        }

        public bool IsWithinAttackableRange(IUnit Attacker, IUnit Defender)
        {
            Tuple<int, int> start = new Tuple<int, int>(Attacker.Y_Position, Attacker.X_Position);

            var closed = new HashSet<Tuple<int, int>>();
            var queue = new PriorityQueue<double, Path<Tuple<int, int>>>();

            queue.Enqueue(0, new Path<Tuple<int, int>>(start));

            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                    continue;

                closed.Add(path.LastStep);
                foreach (var n in GetHostileNeighbours(path.LastStep.Item2, path.LastStep.Item1))
                {
                    var newPath = path.AddStep(n, 1);
                    if (newPath.TotalCost <= Attacker.Range)
                    {
                        queue.Enqueue(newPath.TotalCost, newPath);
                    }
                }
            }

            return closed.Any(t => t.Item2 == Defender.X_Position && t.Item1 == Defender.Y_Position);
        }

        public bool IsAnyUnitWithinAttackableRange(string playerId)
        {
            var units = Board.Units.Where(x => x.Owner == playerId).ToList();
            foreach (var unit in units)
            {
                if(HasAttackableTargets(unit))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public sealed class Path<T> : IEnumerable<T>
    {
        public T LastStep { get; private set; }

        public Path<T> PreviousSteps { get; private set; }

        public int TotalCost { get; private set; }

        private Path(T lastStep, Path<T> previousSteps, int totalCost)
        {
            LastStep = lastStep;
            PreviousSteps = previousSteps;
            TotalCost = totalCost;
        }

        public Path(T start)
            : this(start, null, 0)
        {
        }

        public Path<T> AddStep(T step, int stepCost)
        {
            return new Path<T>(step, this, TotalCost + stepCost);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Path<T> p = this; p != null; p = p.PreviousSteps)
                yield return p.LastStep;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}