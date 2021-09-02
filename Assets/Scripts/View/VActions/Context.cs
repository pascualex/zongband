using Zongband.View.Entities;

using RLEngine.Entities;
using RLEngine.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System.Collections.Generic;

public class Context
{
    private readonly Vector3 origin;
    private readonly Vector3 scale;

    public Context(Transform entitiesParent, Tilemap tilemap, Vector3 origin, Vector3 scale, float movementDuration, Ease movementEase)
    {
        EntitiesParent = entitiesParent;
        Tilemap = tilemap;
        this.origin = origin;
        this.scale = scale;
        MovementDuration = movementDuration;
        MovementEase = movementEase;
    }

    public Dictionary<Entity, VEntity> VEntities { get; } = new();
    public Transform EntitiesParent { get; }
    public Tilemap Tilemap { get; }
    public float MovementDuration { get; }
    public Ease MovementEase { get; }

    public Vector3 CoordsToWorld(Coords coords)
    {
        var offset = new Vector3(coords.X + 0.5f, 0f, coords.Y + 0.5f);
        return origin + Vector3.Scale(offset, scale);
    }
}