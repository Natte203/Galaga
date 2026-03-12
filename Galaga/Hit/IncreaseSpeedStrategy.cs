namespace Galaga.Hit;

using System;

public class IncreaseSpeedStrategy : IHitStrategy {
    public void Hit(Enemy enemy) {
        if (enemy.Hitpoints > 0) {
            enemy.MovementStrategy.Scale(1.02f);
            Console.WriteLine("Speed: Speed increases"); // Just added so it is possible to see, that the strategy runs
        }
    }
}