namespace Galaga.Movement;

using System.Numerics;
using DIKUArcade.Entities;

public class Down : IMovementStrategy {
    private float speed = -0.0003f;

    public Down() {
    }

    public void Scale(float factor) {
        speed *= factor;
    }

    public void Move(Enemy enemy) {
        var enemyDynShape = (DynamicShape) enemy.Shape;
        enemyDynShape.ChangeVelocity(new Vector2(0.0f, speed));
        enemyDynShape.Move();
    }
}