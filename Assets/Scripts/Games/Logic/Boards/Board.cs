using System;

using Zongband.Games.Core.Boards;
using Zongband.Games.Logic.Entities;
using Zongband.Utils;

namespace Zongband.Games.Logic.Boards
{
    public class Board<T> : IBoard
    {
        public readonly Size Size;

        private readonly EntityLayer<Agent> AgentLayer;
        private readonly EntityLayer<Entity> EntityLayer;
        private readonly TerrainLayer<T> TerrainLayer;

        public Board(IBoardData<T> data, IBoardView<T> view)
        {
            Size = data.Size;
            AgentLayer = new EntityLayer<Agent>(Size);
            EntityLayer = new EntityLayer<Entity>(Size);
            var defaultTerrainType = data.DefaultTerrainType;
            TerrainLayer = new TerrainLayer<T>(Size, defaultTerrainType, view.TerrainLayerView);
        }

        public void Add(Entity entity, Tile at)
        {
            if (!IsTileAvailable(entity, at, false)) throw new NotEmptyTileException(at);

            if (entity is Agent agent) AgentLayer.Add(agent, at);
            else EntityLayer.Add(entity, at);
        }

        public void Move(Entity entity, Tile to, bool relative)
        {
            if (relative) to += entity.Tile;

            if (!IsTileAvailable(entity, to, false)) throw new NotEmptyTileException(to);

            if (entity is Agent agent) AgentLayer.Move(agent, to);
            else EntityLayer.Move(entity, to);
        }

        public void Remove(Entity entity)
        {
            if (entity is Agent agent) AgentLayer.Remove(agent);
            else EntityLayer.Remove(entity);
        }

        public void Modify(Tile at, ITerrainTypeData<T> terrainType)
        {
            if (!IsTileAvailable(terrainType, at)) throw new NotEmptyTileException(at);

            TerrainLayer.Modify(at, terrainType);
        }

        public void Box(Tile from, Tile to, ITerrainTypeData<T> terrainType)
        {
            var lower = new Tile(Math.Min(from.X, to.X), Math.Min(from.Y, to.Y));
            var higher = new Tile(Math.Max(from.X, to.X), Math.Max(from.Y, to.Y));
            for (var i = lower.Y; i <= higher.Y; i++)
            {
                for (var j = lower.X; j <= higher.X; j++)
                {
                    Modify(new Tile(j, i), terrainType);
                }
            }
        }

        // public void Apply(BoardData boardData)
        // {
        //     Apply(boardData, Tile.Zero);
        // }

        // public void Apply(BoardData boardData, Tile origin)
        // {
        //     for (var i = 0; i < boardData.Size.Y; i++)
        //     {
        //         for (var j = 0; j < boardData.Size.X; j++)
        //         {
        //             var tile = new Tile(j, i);
        //             Modify(origin + tile, boardData.GetTerrain(tile));
        //         }
        //     }
        // }

        public Agent? GetAgent(Entity entity, Tile at, bool relative)
        {
            if (relative) at += entity.Tile;
            return GetAgent(at);
        }

        public Agent? GetAgent(Tile at)
        {
            if (!Size.Contains(at)) return null;
            return AgentLayer.Get(at);
        }

        public Entity? GetEntity(Tile at)
        {
            if (!Size.Contains(at)) return null;
            return EntityLayer.Get(at);
        }

        public bool IsTileEmpty(Tile tile)
        {
            if (!Size.Contains(tile)) return false;
            if (!AgentLayer.IsTileEmpty(tile)) return false;
            if (!EntityLayer.IsTileEmpty(tile)) return false;
            return true;
        }

        public bool IsTileAvailable(Entity entity, Tile tile, bool relative)
        {
            if (relative) tile += entity.Tile;
            if (!Size.Contains(tile)) return false;
            /* Add here special interactions in the future */
            if (!AgentLayer.IsTileEmpty(tile)) return false;
            var isGhost = (entity is Agent agent) && agent.IsGhost;
            if (!isGhost && !EntityLayer.IsTileEmpty(tile)) return false;
            if (!isGhost && TerrainLayer.GetTile(tile).Type.BlocksGround) return false;
            return true;
        }

        public bool IsTileAvailable(ITerrainTypeData<T> terrainType, Tile tile)
        {
            if (!Size.Contains(tile)) return false;
            /* Add here special interactions in the future */
            if (terrainType.BlocksGround && !AgentLayer.IsTileEmpty(tile)) return false;
            if (terrainType.BlocksGround && !EntityLayer.IsTileEmpty(tile)) return false;
            return true;
        }
    }
}