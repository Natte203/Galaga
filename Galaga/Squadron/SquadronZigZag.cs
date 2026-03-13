namespace Galaga.Squadron;
using System;
using System.Numerics;

public class SquadronZigZag : ISquadron {
    private readonly Vector2 _origin;

    public int numEnemies => 9;

    public SquadronZigZag(Vector2 origin) {
        _origin = origin;
    }

    public Vector2 GetSafeOrigin(Random random) {
        return new Vector2(
            0.05f, 
            random.NextSingle() *0.2f + 0.6f);
    }

    public Vector2 GetPosition(int index) {
        int row = index < 4 ? 0 : 1;
        int column = index < 4 ? index : index - 4;
        return new Vector2(
            _origin.X + column *0.17f + (1- row) * 0.1f, 
            _origin.Y - row * 0.1f);
    }
}