namespace Galaga;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class PlayerShot : Entity {

    private static Vector2 extent = new Vector2(0.008f, 0.021f);
    private static Vector2 velocity = new Vector2(0.0f, 0.01f);

    public PlayerShot(Vector2 position, IBaseImage image) 
        : base(new DynamicShape(position, extent, velocity), image) { // Har tilføjet velocity her istedet
        // ((DynamicShape) Shape).Velocity = velocity; //Added this, despite of it not being explicit in the assignment text - otherwise the bullets didn't move
    }
    
}
