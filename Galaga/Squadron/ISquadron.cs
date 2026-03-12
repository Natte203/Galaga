namespace Galaga.Squadron;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
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