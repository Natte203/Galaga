namespace Galaga;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Galaga.Movement;

public class Enemy : Entity {
    public Vector2 Position { // Changed NDO
        get { return Shape.Position; }
        private set { Shape.Position = value; }
    }
    public Vector2 Extent { get; }
    private GameEventBus gameEventBus;
    private IMovementStrategy movementStrategy; // Added NDO
    public Vector2 StartPosition { get; } // Added NDO

    public Enemy(
        DynamicShape shape, IBaseImage image, GameEventBus eventBus, IMovementStrategy mvStrat) // Changed NDO
            : base(shape, image) {
        Position = shape.Position;
        Extent = shape.Extent;
        gameEventBus = eventBus;
        movementStrategy = mvStrat;
        StartPosition = shape.Position;
    }

    public void DeleteEnemy() {
        gameEventBus.RegisterEvent<AddExplosionEvent>(new AddExplosionEvent(Position, Extent));
        DeleteEntity();        
    }

    public void Move() { // Added NDO
        movementStrategy.Move(this);
        Position = new Vector2(Position.X, Math.Clamp(Position.Y, 0.2f, 0.9f));
    }
}
