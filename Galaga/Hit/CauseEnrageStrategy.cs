namespace Galaga.Hit;

using System;

public class CauseEnrageStrategy : IHitStrategy {
    public void Hit(Enemy enemy) {
        if (enemy.Hitpoints < 3) {
            enemy.MovementStrategy.Scale(3.1f);
            enemy.Image = enemy.CauseEnrageImage;
        }
    }
}

