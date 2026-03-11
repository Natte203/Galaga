namespace Galaga.Squadron;

using System;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Galaga.Hit;
using Galaga.Movement;

public interface ISquadron {

    EntityContainer<Enemy> CreateEnemies(
        List<Image> enemyStrides,
        List<Image> enragedStrides,
        Func<IMovementStrategy> movement,
        Func<IHitStrategy> hit
    );
}