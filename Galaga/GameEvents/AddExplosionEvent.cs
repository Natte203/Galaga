namespace Galaga;

using System.Numerics;

// HVOR AddExplosion skal ske henne
public readonly struct AddExplosionEvent { //readonly: værdien kan KUN sættes ved instantiering/ immutable
    public Vector2 Position { get; }
    public Vector2 Extent { get; }

    //Når værdien sættes ved instantiering kan den aldrig ændres
    public AddExplosionEvent(Vector2 position, Vector2 extent) {
        Position = position;
        Extent = extent; 
    }
} 