using UnityEngine;
using System;

using Zongband.Entities;

namespace Zongband.Boards
{
    public class NotInTileException : TileException
    {
        public NotInTileException(Entity entity) : base(entity.position)
        {

        }
    }
}
