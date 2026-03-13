namespace Galaga.Squadron;
using System;
using System.Numerics;

public class SquadronDiamond : ISquadron {
    private readonly Vector2 _origin;

    public int numEnemies => 13;

    public SquadronDiamond(Vector2 origin) {
        _origin = origin;
    }

    public Vector2 GetSafeOrigin(Random random) {
        return new Vector2(
            random.NextSingle() *0.3f + 0.1f, 
            random.NextSingle() *0.4f + 0.1f);
    }

    public Vector2 GetPosition(int index) {
        int row = 0;
        int column = 0;
        int remaining = index;
        for (row = 0; row < 5; row++) {
        int distance = Math.Abs(row - 2);
        int enemiesRow = 5 - distance * 2;
            if (remaining < enemiesRow) {
                column = remaining;
                break;
            }
            remaining -= enemiesRow;
        }
        int enemyColumn = Math.Abs(row - 2);
        return new Vector2(
            _origin.X + (enemyColumn + column) *0.1f, 
            _origin.Y - row * 0.1f);
    }
}


    