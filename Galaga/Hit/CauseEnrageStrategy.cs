namespace Galaga.Hit;

using System;

public class CauseEnrageStrategy : IHitStrategy {
    public void Hit(Enemy enemy) {
        if (enemy.Hitpoints < 3) {
            enemy.MovementStrategy.Scale(1.02f);
            enemy.Image = enemy.CauseEnrageImage;
            Console.WriteLine("Enraged: Speed increases"); // Just added so it is possible to see, that the strategy runs
        }
    }
}

