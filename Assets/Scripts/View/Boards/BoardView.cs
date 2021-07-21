// using Zongband.Engine.Boards;
// using Zongband.Engine.Entities;
// using Zongband.Utils;

// using UnityEngine;
// using UnityEngine.Tilemaps;
// using System.Collections.Generic;

// namespace Zongband.View.Boards
// {
//     public class BoardView
//     {
//         private readonly Tilemap tilemap;
//         private readonly Dictionary<Entity, GameObject> entities = new();

//         public BoardView(Tilemap tilemap)
//         {
//             this.tilemap = tilemap;
//         }

//         public void Add(Entity entity, Coords at)
//         {
//             if (entities.ContainsKey(entity)) return;
//             if (entity.Type.Visuals is not GameObject prefab)
//             {
//                 Debug.Log(Warnings.UnexpectedVisualsObject);
//                 return;
//             }

//             var position = CoordsToWorld(at);
//             var model = GameObject.Instantiate(prefab, position, Quaternion.identity);
//             entities.Add(entity, model);
//         }

//         public void Move(Entity entity, Coords to)
//         {
//             if (!entities.TryGetValue(entity, out var model)) return;

//             var position = CoordsToWorld(to);
//             model.transform.position = position;
//         }

//         public void Remove(Entity entity)
//         {
//             if (!entities.TryGetValue(entity, out var model)) return;

//             GameObject.Destroy(model);
//             entities.Remove(entity);
//         }

//         public void Modify(Coords at, ITileType tileType)
//         {
//             if (tileType.Visuals is not TileBase tilebase)
//             {
//                 Debug.Log(Warnings.UnexpectedVisualsObject);
//                 return;
//             }

//             var position = new Vector3Int(at.X, at.Y, 0);
//             tilemap.SetTile(position, tilebase);
//         }

//         private Vector3 CoordsToWorld(Coords coords)
//         {
//             var origin = tilemap.transform.position;
//             var offset = new Vector3(coords.X + 0.5f, 0f, coords.Y + 0.5f);
//             var scale = new Vector3(tilemap.cellSize.x, 0f, tilemap.cellSize.y);
//             return origin + Vector3.Scale(offset, scale);
//         }
//     }
// }
