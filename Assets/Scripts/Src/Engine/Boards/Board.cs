using System;
using System.Collections.Generic;

using Zongband.Engine.Entities;
using Zongband.Utils;

namespace  Zongband.Engine.Boards
{
    public class Board : IReadOnlyBoard
    {
        public Size Size { get; }

        private readonly Tile[][] tiles;
        private readonly Dictionary<Entity, Coords> entities = new();
        private readonly IBoardView view;

        public Board(Size size, ITerrain defaultTerrain, IBoardView view)
        {
            Size = size;
            tiles = new Tile[Size.Y][];
            this.view = view;

            for (var i = 0; i < Size.Y; i++)
            {
                tiles[i] = new Tile[Size.X];
                for (var j = 0; j < Size.X; j++)
                {
                    tiles[i][j] = new Tile(defaultTerrain);
                    view.Modify(new Coords(j, i), defaultTerrain);
                }
            }
        }

        public bool Add(Entity entity, Coords at)
        {
            if (entities.ContainsKey(entity)) return false;
            if (!Size.Contains(at)) return false;

            if (!tiles[at.Y][at.X].Add(entity)) return false;
            entities[entity] = at;
            return true;
        }

        public bool Move(Entity entity, Coords to, bool relative)
        {
            if (!entities.TryGetValue(entity, out var from)) return false;
            if (!Size.Contains(to)) return false;

            if (relative) to += from;

            if (!tiles[to.Y][to.X].Add(entity)) return false;
            tiles[from.Y][from.X].Remove(entity);
            entities[entity] = to;
            return true;
        }

        public void Remove(Entity entity)
        {
            if (!entities.TryGetValue(entity, out var at)) return;

            tiles[at.Y][at.X].Remove(entity);
            entities.Remove(entity);
        }

        public bool SetTerrain(ITerrain terrain, Coords at)
        {
            if (!Size.Contains(at)) return false;

            if (!tiles[at.Y][at.X].SetTerrain(terrain)) return false;
            view.Modify(at, terrain);
            return true;
        }

        public void SetTerrain(ITerrain terrain, Coords from, Coords to)
        {
            var lower = new Coords(Math.Min(from.X, to.X), Math.Min(from.Y, to.Y));
            var higher = new Coords(Math.Max(from.X, to.X), Math.Max(from.Y, to.Y));
            for (var i = lower.Y; i <= higher.Y; i++)
            {
                for (var j = lower.X; j <= higher.X; j++)
                {
                    SetTerrain(terrain, new Coords(j, i));
                }
            }
        }

        public IReadOnlyTile? GetTile(Coords at)
        {
            if (!Size.Contains(at)) return null;

            return tiles[at.Y][at.X];
        }

        // public Agent? GetAgent(Tile at)
        // {
        //     if (!Size.Contains(at)) return null;
        //     return AgentLayer.Get(at);
        // }

        // public Entity? GetEntity(Tile at)
        // {
        //     if (!Size.Contains(at)) return null;
        //     return EntityLayer.Get(at);
        // }

        // public bool IsTileEmpty(Tile tile)
        // {
        //     if (!Size.Contains(tile)) return false;
        //     if (!AgentLayer.IsTileEmpty(tile)) return false;
        //     if (!EntityLayer.IsTileEmpty(tile)) return false;
        //     return true;
        // }

        // public bool IsTileAvailable(Entity entity, Tile tile, bool relative)
        // {
        //     if (relative) tile += entity.Tile;
        //     if (!Size.Contains(tile)) return false;
        //     /* Add here special interactions in the future */
        //     if (!AgentLayer.IsTileEmpty(tile)) return false;
        //     var isGhost = (entity is Agent agent) && agent.IsGhost;
        //     if (!isGhost && !EntityLayer.IsTileEmpty(tile)) return false;
        //     if (!isGhost && TerrainLayer.GetTile(tile).Type.BlocksGround) return false;
        //     return true;
        // }

        // public bool IsTileAvailable(ITerrainType terrainType, Tile tile)
        // {
        //     if (!Size.Contains(tile)) return false;
        //     /* Add here special interactions in the future */
        //     if (terrainType.BlocksGround && !AgentLayer.IsTileEmpty(tile)) return false;
        //     if (terrainType.BlocksGround && !EntityLayer.IsTileEmpty(tile)) return false;
        //     return true;
        // }
    }
}