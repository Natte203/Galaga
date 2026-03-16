namespace Galaga.Squadron;
using System;
using System.Numerics;
using DIKUArcade.Events;

public class SquadronBox : SquadronBase {
    private readonly Vector2 origin;

    public override int numEnemies => 8;

    public SquadronBox(Vector2 origin, GameEventBus gameEventBus) : base(gameEventBus) {
        this.origin = origin;
    }

    public override Vector2 GetSafeOrigin(Random random) {
        return new Vector2(
        random.NextSingle() * 0.6f + 0.1f,
        random.NextSingle() * 0.2f + 0.5f);
    }
    public override Vector2 GetPosition(int index) {
        int adjusted = index >= 4 ? index + 1 : index;
        int column = adjusted % 3;
        int row = adjusted / 3;
        return new Vector2(
            origin.X + column * 0.1f,
            origin.Y + row * 0.1f);
    }
}
