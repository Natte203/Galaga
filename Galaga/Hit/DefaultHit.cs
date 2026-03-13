namespace Galaga.Hit;

public class DefaultHit : IHitStrategy {
    public void Hit(Enemy enemy) {
        enemy.DeleteEnemy();
    }
}