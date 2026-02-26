namespace Galaga;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Enemy : Entity {
    public Vector2 Position { get; }
    public Vector2 Extent { get; }

    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
        Position = shape.Position;
        Extent = shape.Extent;
    }


}
