namespace Galaga.Squadron;
using System;
using System.Numerics;
using DIKUArcade.Events;

public class SquadronDiamond : SquadronBase {
    private readonly Vector2 origin;

    public override int numEnemies => 13;

    public SquadronDiamond(Vector2 origin, GameEventBus gameEventBus) : base(gameEventBus) {
        this.origin = origin;
    }

    public override Vector2 GetSafeOrigin(Random random) {
        return new Vector2(
            random.NextSingle() * 0.3f + 0.1f,
            random.NextSingle() * 0.1f + 0.8f);
    }

    public override Vector2 GetPosition(int index) {
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
            origin.X + (enemyColumn + column) * 0.1f,
            origin.Y - row * 0.1f);
    }
}


