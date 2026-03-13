namespace Galaga.Hit;

using System;
using System.Numerics;

public class TeleportStrategy : IHitStrategy {
    public void Hit(Enemy enemy) {
        var random = new Random();
        if (enemy.Hitpoints > 0) {
            var randomX = random.NextDouble();
            var x = Math.Clamp(randomX, 0.0 + enemy.Shape.Extent.X, 1.0 - enemy.Shape.Extent.X);
            var randomY = random.NextDouble() * (1.0 - 0.3) + 0.3;
            var y = Math.Clamp(randomY, 0.3 + enemy.Shape.Extent.Y, 1.0 - enemy.Shape.Extent.Y);
            enemy.Shape.Position = new Vector2((float) x, (float) y);
            enemy.StartPosition = new Vector2((float) x, (float) y);
        }
    }
}



