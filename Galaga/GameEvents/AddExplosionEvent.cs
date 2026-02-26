namespace Galaga;

using System.Numerics;

public readonly struct AddExplosionEvent { //readonly: værdien kan KUN sættes ved instantiering/ immutable
    public Vector2 Position { get; }
    public Vector2 Extent { get; }

    public AddExplosionEvent(Vector2 position, Vector2 extent) { //Når værdien sættes ved instantiering kan den aldrig ændres
        Position = position;
        Extent = extent; 
    }
} 