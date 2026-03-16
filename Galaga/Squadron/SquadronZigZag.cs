namespace Galaga.Squadron;
using System;
using System.Numerics;
using DIKUArcade.Events;

public class SquadronZigZag : SquadronBase {
    private readonly Vector2 origin;

    public override int numEnemies => 9;

    public SquadronZigZag(Vector2 origin, GameEventBus gameEventBus) : base (gameEventBus) {
        this.origin = origin;
    }

    public override Vector2 GetSafeOrigin(Random random) {
        return new Vector2(
            0.05f,
            random.NextSingle() * 0.2f + 0.6f);
    }

    public override Vector2 GetPosition(int index) {
        int row = index < 4 ? 0 : 1;
        int column = index < 4 ? index : index - 4;
        return new Vector2(
            origin.X + column * 0.17f + (1 - row) * 0.1f,
            origin.Y - row * 0.1f);
    }
}