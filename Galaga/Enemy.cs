namespace Galaga;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using Galaga.Hit;
using Galaga.Movement;

public class Enemy : Entity {
    public Vector2 Position {
        get {
            return Shape.Position;
        }
    }
    public Vector2 Extent {
        get {
            return Shape.Extent;
        }
    }
    private GameEventBus gameEventBus;
    private IBaseImage causeEnrageImage;
    public IBaseImage CauseEnrageImage {
        get {
            return causeEnrageImage;
        }
    }
    private IHitStrategy hitStrategy;
    private int hitpoints = 5;
    public int Hitpoints {
        get {
            return hitpoints;
        }
    }
    private IMovementStrategy movementStrategy;
    public IMovementStrategy MovementStrategy {
        get {
            return movementStrategy;
        }
    }

    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enragedImage, GameEventBus eventBus, IHitStrategy hitStrat, IMovementStrategy moveStrat) : base(shape, image) {
        causeEnrageImage = enragedImage;
        gameEventBus = eventBus;
        hitStrategy = hitStrat;
        movementStrategy = moveStrat;
    }

    public void DeleteEnemy() {
        gameEventBus.RegisterEvent<AddExplosionEvent>(new AddExplosionEvent(Position, Extent));
        DeleteEntity();
    }

    public void TakeHit() {
        hitpoints = hitpoints - 1;
        if (hitpoints < 1) {
            DeleteEnemy();
        } else
            hitStrategy.Hit(this);
    }
}
