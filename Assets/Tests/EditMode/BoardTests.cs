using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

using Zongband.Game.Boards;
using Zongband.Game.Entities;
using Zongband.Utils;

using Random = System.Random;

namespace ZongbandTests.Game.Boards
{
    public class BoardTests
    {
        private Board Board = new Board();
        private Entity Entity = new Entity();
        private Agent Agent = new Agent();
        private readonly Random Random = new Random();

        [SetUp]
        public void Setup()
        {
            var boardSO = ScriptableObject.CreateInstance<BoardSO>();
            Board = new Board();
            Board.ApplySO(boardSO);

            var entitySO = ScriptableObject.CreateInstance<EntitySO>();
            Entity = new Entity();
            Entity.ApplySO(entitySO);

            var agentSO = ScriptableObject.CreateInstance<AgentSO>();
            Agent = new Agent();
            Agent.ApplySO(agentSO);
        }

        [Test]
        public void AddEntityPasses()
        {
            var position = Random.Range(Board.Size);
            Board.Add(Entity, position);
            var entityInBoard = Board.GetEntity(position);
            Assert.AreEqual(Entity, entityInBoard);
        }

        [Test]
        public void AddAgentPasses()
        {
            Assert.Pass();
        }

        [Test]
        public void MoveEntityPasses()
        {
            Assert.Pass();
        }

        [Test]
        public void MoveAgentPasses()
        {
            Assert.Pass();
        }

        [Test]
        public void RemoveEntityPasses()
        {
            Assert.Pass();
        }

        [Test]
        public void RemoveAgentPasses()
        {
            Assert.Pass();
        }
    }
}