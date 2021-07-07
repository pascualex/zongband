// // using UnityEngine;
// using System;
// using System.Collections.Generic;

// using Zongband.Games.Boards;
// using Zongband.Utils;

// using Random = UnityEngine.Random;
// using ANE = System.ArgumentNullException;

// namespace Zongband.Games.Generation
// {
//     public class DungeonGenerator
//     {
//         public DungeonData? GenerateDungeon(DungeonSO dungeonSO)
//         {
//             var rooms = dungeonSO.Rooms;
//             var size = dungeonSO.Size;
//             var minSide = dungeonSO.MinSide;
//             var maxSide = dungeonSO.MaxSide;
//             var roomList = GenerateRooms(rooms, size, minSide, maxSide);

//             var iterations = 0;
//             var padding = dungeonSO.Padding;
//             var maxIterations = dungeonSO.MaxIterations;
//             while (iterations < maxIterations && ExpandRooms(roomList, size, padding)) iterations++;
//             Debug.Log("Dungeon generated in " + (iterations + 1) + " iterations");
//             if (iterations >= maxIterations) Debug.LogWarning("Iteration limit reached");

//             var floor = dungeonSO.Floor.Value();
//             var wall = dungeonSO.Wall.Value();
//             var dungeonData = new DungeonData(size, floor, wall);
//             dungeonData.Rooms.AddRange(roomList);
//             var playerSpawnPlaced = false;
//             foreach (var room in roomList)
//             {
//                 if (!playerSpawnPlaced)
//                 {
//                     dungeonData.PlayerSpawn = new Tile(room.Center);
//                     playerSpawnPlaced = true;
//                 }
//                 else dungeonData.EnemiesSpawn.Add(new Tile(room.Center));
//             }

//             var connections = ConnectRooms(roomList);
//             dungeonData.Connections.AddRange(connections);

//             return dungeonData;
//         }

//         public DungeonData GenerateTestDungeon(Size size, int wallWidth, TerrainSO floor, TerrainSO wall)
//         {
//             var dungeonData = new DungeonData(size, floor, wall);

//             dungeonData.Rooms.Add(new Room(new Tile(wallWidth), size - new Size(wallWidth * 2)));
//             dungeonData.PlayerSpawn = new Tile(5, 5);

//             return dungeonData;
//         }

//         private List<Room> GenerateRooms(int quantity, Size dungeonSize, int minSide, int maxSide)
//         {
//             var rooms = new List<Room>(quantity);
//             for (var i = 0; i <quantity; i++) rooms.Add(GenerateRoom(dungeonSize, minSide, maxSide));
//             return rooms;
//         }

//         private Room GenerateRoom(Size dungeonSize, int minSide, int maxSide)
//         {
//             var sideX = Random.Range(minSide, maxSide + 1);
//             var sideY = Random.Range(minSide, maxSide + 1);
//             var size = new Size(sideX, sideY);

//             var insideUnitCircle = Random.insideUnitCircle;
//             var x = (dungeonSize.X / 2f) + (insideUnitCircle.x * dungeonSize.X / 8f) - (size.X / 2f);
//             var y = (dungeonSize.Y / 2f) + (insideUnitCircle.y * dungeonSize.Y / 8f) - (size.Y / 2f);
//             var origin = new Tile(Convert.ToInt32(x), Convert.ToInt32(y));

//             return new Room(origin, size);
//         }

//         private bool ExpandRooms(List<Room> rooms, Size dungeonSize, int padding)
//         {
//             var collision = false;
//             foreach (var roomA in rooms)
//             {
//                 foreach (var roomB in rooms)
//                 {
//                     if (roomA == roomB) continue;
//                     if (!roomA.MoveAway(roomB, padding)) continue;
//                     collision = true;
//                     break;
//                 }
//             }
//             rooms.RemoveAll(room => room.IsOutside(dungeonSize, padding));
//             return collision;
//         }

//         // TODO: improve efficiency
//         private List<Tuple<Room, Room>> ConnectRooms(List<Room> rooms)
//         {
//             var notConnected = new LinkedList<Room>(rooms);
//             var connected = new LinkedList<Room>();

//             connected.AddLast(notConnected.Last.Value);
//             notConnected.RemoveLast();

//             var connections = new List<Tuple<Room, Room>>();

//             while (notConnected.Count > 0)
//             {
//                 Tuple<Room, Room>? minPair = null;
//                 var minDistance = int.MaxValue;

//                 foreach (var roomA in connected)
//                 {
//                     foreach (var roomB in notConnected)
//                     {
//                         var distance = roomA.GetDistance(roomB);
//                         if (distance < minDistance)
//                         {
//                             minPair = new Tuple<Room, Room>(roomA, roomB);
//                             minDistance = distance;
//                         }
//                     }
//                 }

//                 connections.Add(minPair!);
//                 connected.AddLast(minPair!.Item2);
//                 notConnected.Remove(minPair!.Item2);
//             }

//             return connections;
//         }
//     }
// }