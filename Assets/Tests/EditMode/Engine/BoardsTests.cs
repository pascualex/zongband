using NUnit.Framework;
using NSubstitute;
using System;

using Zongband.Engine.Boards;
using Zongband.Engine.Entities;
using Zongband.Utils;
using ZongbandTests.Utils;

namespace ZongbandTests.Engine
{
    public class BoardsTests
    {
        private class Fixture
        {
            public Board Board { get; }
            public Entity Entity { get; }

            public Fixture(Size size)
            {
                var defaultTerrain = Substitute.For<ITerrain>();
                var boardView = Substitute.For<IBoardView>();
                Board = new(size, defaultTerrain, boardView);
                var entityType = Substitute.For<IEntityType>();
                Entity = new(entityType);
            }
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void EntityIsAdded(int x, int y)
        {
            var f = new Fixture(new Size(3, 3));

            var position = new Coords(x, y);
            var added = f.Board.Add(f.Entity, position);
            Assert.That(added, Is.True);

            var tile = f.Board.GetTile(position).FailIfNull();
            Assert.That(tile.Entities, Has.Member(f.Entity));
        }

        [Test]
        [TestCase(0, 1, 2, 1)]
        [TestCase(0, 0, 2, 2)]
        [TestCase(1, 1, 1, 1)]
        public void EntityIsMoved(int ix, int iy, int fx, int fy)
        {
            var f = new Fixture(new Size(3, 3));

            var initialPosition = new Coords(ix, iy);
            f.Board.Add(f.Entity, initialPosition);

            var finalPosition = new Coords(fx, fy);
            var moved = f.Board.Move(f.Entity, finalPosition);
            Assert.That(moved, Is.True);

            if (finalPosition != initialPosition)
            {
                var initialTile = f.Board.GetTile(initialPosition).FailIfNull();
                Assert.That(initialTile.Entities, Has.No.Member(f.Entity));
            }

            var finalTile = f.Board.GetTile(finalPosition).FailIfNull();
            Assert.That(finalTile.Entities, Has.Member(f.Entity));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(2, 2)]
        public void EntityIsRemoved(int x, int y)
        {
            var f = new Fixture(new Size(3, 3));

            var position = new Coords(x, y);
            f.Board.Add(f.Entity, position);

            var removed = f.Board.Remove(f.Entity);
            Assert.That(removed, Is.True);

            var tile = f.Board.GetTile(position).FailIfNull();
            Assert.That(tile.Entities, Has.No.Member(f.Entity));
        }
    }
}