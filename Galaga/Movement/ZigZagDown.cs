namespace Galaga.Movement;

using System;
using System.Numerics;
using DIKUArcade.Entities;

public class ZigZagDown : IMovementStrategy {
    private float speed = -0.0003f;
    private const float AMPLITUDE = 0.045f;
    private const float PERIOD = 0.045f;



    public ZigZagDown() {
    }

    public void Scale(float factor) {
        speed *= factor;
    }

    public void Move(Enemy enemy) {
        var enemyDynShape = (DynamicShape) enemy.Shape;
        float posY = enemy.Position.Y;
        float posX = enemy.Position.X;
        posX =
            enemy.StartPosition.X + AMPLITUDE * 
                (float) Math.Sin(2 * Math.PI * (enemy.StartPosition.Y - (posY + speed) / PERIOD));
        enemyDynShape.ChangeVelocity(new Vector2(posX - enemy.Position.X, speed));
        enemyDynShape.Move();
    }
}