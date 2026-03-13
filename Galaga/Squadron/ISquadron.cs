namespace Galaga.Squadron;

using System;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.Numerics;
using DIKUArcade.Events;
using System.Collections.Generic;
using Galaga.Movement;
using Galaga.Hit;



public interface ISquadron {
    Vector2 GetPosition(int index);
    int numEnemies { get; }
    Vector2 GetSafeOrigin(Random random);

    EntityContainer<Enemy> CreateEnemies(
        List<Image> enemyStrides,
        List<Image> enragedStrides,
        Func<IMovementStrategy> movement,
        Func<IHitStrategy> hit,
        GameEventBus gameEventBus){
        // List<Image> images =
        //     ImageStride.CreateStrides(4, enemyStrides); Took out - ImageStride is used directly below.
       // const int numEnemies = 8; //need to control the count
        var enemies = new EntityContainer<Enemy>(numEnemies); 
        for (int i = 0; i < numEnemies; i++) {
            var pos = GetPosition(i); //added this
            enemies.AddEntity(new Enemy( 
                new DynamicShape(pos, new Vector2(0.1f, 0.1f)),  //changed from new DynamicShape(new Vector2(0.1f + (float)i * 0.1f, 0.9f), new Vector2(0.1f, 0.1f)),       
                new ImageStride(80, enemyStrides),
                movement(),
                hit(),
                gameEventBus));  
        }
        return enemies;

    }
}

//Et stempelt som er en formation -> det er vigtigt at den kan bevæge sig (don't hardcode)
//enemyStrides (blå) engared rød)
//Abstract factory: For rød/blå.
//Strategy pattern: for resten.
//Husk guardrails så de bliver indenfor skærmen.
//Isquadron vælger randomly en movement og en hit (selvom der kun er én).
//Instantier enimy her (se enemy og update med hit og movement)
//Husk eventbussen i Squadron - som argument.
//I gamee: I game kan man tage en randome, som bare kalder én af de tre.