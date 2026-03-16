namespace Galaga;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga.Hit;
using Galaga.Movement;
using DIKUArcade.Events;

public class Enemy : Entity {
    public Vector2 Position {
        get { return Shape.Position; }
        private set { Shape.Position = value; }
    }
    public Vector2 Extent {
        get { return Shape.Extent; }
    }
    private GameEventBus gameEventBus;
    public Vector2 StartPosition { get; set; }
    private IBaseImage causeEnrageImage;
    public IBaseImage CauseEnrageImage {
        get { return causeEnrageImage; }
    }
    private IHitStrategy hitStrategy;
    private int hitpoints = 5;
    public int Hitpoints {
        get { return hitpoints; }
    }
    private IMovementStrategy movementStrategy;
    public IMovementStrategy MovementStrategy {
        get { return movementStrategy; }
    }

    public Enemy(
        DynamicShape shape, 
        IBaseImage image, 
        IBaseImage enragedImage,
        GameEventBus eventBus,
        IHitStrategy hitStrat,
        IMovementStrategy mvStrat) : base(shape, image) {
        causeEnrageImage = enragedImage;    
        Position = shape.Position;
        gameEventBus = eventBus;
        movementStrategy = mvStrat;
        StartPosition = shape.Position;
        hitStrategy = hitStrat;
    }

    public void DeleteEnemy() {
        gameEventBus.RegisterEvent<AddExplosionEvent>(new AddExplosionEvent(Position, Extent));
        DeleteEntity();
    }

    public void Move() { 
        movementStrategy.Move(this);
        Position = new Vector2(
            Math.Clamp(Position.X, 0.0f, 1.0f - Extent.X), Math.Clamp(Position.Y, 0.2f, 0.9f));
    }

    public void TakeHit() {
        hitpoints = hitpoints - 1;
        if (hitpoints < 1) {
            DeleteEnemy();
        } else
            hitStrategy.Hit(this);
    }
}
