namespace Galaga.Squadron;

using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using Galaga.Hit;
using Galaga.Movement;

public abstract class SquadronBase : ISquadron {
    protected readonly GameEventBus gameEventBus;

    protected SquadronBase(GameEventBus gameEventBus) {
        this.gameEventBus = gameEventBus;
    }

    public abstract Vector2 GetPosition(int index);
    public abstract int numEnemies {
        get;
    }
    public abstract Vector2 GetSafeOrigin(Random random);

    public EntityContainer<Enemy> CreateEnemies(
        List<Image> enemyStrides,
        List<Image> enragedStrides,
        Func<IMovementStrategy> movement,
        Func<IHitStrategy> hit) {
        var enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            var pos = GetPosition(i);
            enemies.AddEntity(new Enemy(
                new DynamicShape(pos, new Vector2(0.1f, 0.1f)),
                new ImageStride(80, enemyStrides),
                new ImageStride(80, enragedStrides),
                gameEventBus,
                hit(),
                movement()));
        }
        return enemies;
    }
}
