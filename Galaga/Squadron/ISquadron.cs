namespace Galaga.Squadron;

using System;
using System.Collections.Generic;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using Galaga.Hit;
using Galaga.Movement;

public interface ISquadron {
    Vector2 GetPosition(int index);
    int numEnemies {
        get;
    }
    Vector2 GetSafeOrigin(Random random);
    EntityContainer<Enemy> CreateEnemies(
        List<Image> enemyStrides,
        List<Image> enragedStrides,
        Func<IMovementStrategy> movement,
        Func<IHitStrategy> hit,
        GameEventBus gameEventBus) {
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

