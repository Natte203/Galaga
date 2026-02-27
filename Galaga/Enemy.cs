namespace Galaga;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.Security.Cryptography.X509Certificates;

public class Enemy : Entity {
    public Vector2 Position { get; }
    public Vector2 Extent { get; }
    private GameEventBus gameEventBus;

    public Enemy(DynamicShape shape, IBaseImage image, GameEventBus eventBus) : base(shape, image) {
        Position = shape.Position;
        Extent = shape.Extent;
        gameEventBus = eventBus;
    }

    public void DeleteEnemy() {
        gameEventBus.RegisterEvent<AddExplosionEvent>(new AddExplosionEvent(Position, Extent));
        DeleteEntity();        
    }
}
