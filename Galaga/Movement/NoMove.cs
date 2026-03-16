namespace Galaga.Movement;

public class NoMove : IMovementStrategy {
    private float speed = 0;

    public NoMove() {
    }

    public void Scale(float factor) {
        speed *= factor;
    }

    public void Move(Enemy enemy) {
    }
}