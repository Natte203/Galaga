namespace Galaga;

using System;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;

public class Player : Entity {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
 
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        
    }

    private void UpdateVelocity(float move) {
        ((DynamicShape) Shape).Velocity.X = move; // Vend evt. tilbage og tjek. Lav evt. egen property. 
    }

    public void Move() { // Shape.Position for lang ift Style Guide
        ((DynamicShape) Shape).Move();
        Shape.Position = new Vector2(Math.Clamp(Shape.Position.X, 0.0f, 1.0f - Shape.Extent.X), Shape.Position.Y);
    }

    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = - MOVEMENT_SPEED;
        }
        else { moveLeft = 0; }
        UpdateVelocity(moveLeft);
    }

    public void SetMoveRight(bool val) {
        if (val) {
            moveRight = MOVEMENT_SPEED;
        }
        else { moveRight = 0; }
        UpdateVelocity(moveRight);
    }

    public void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }
        switch (key) {
            case KeyboardKey.Left:
                SetMoveLeft(true);
                Move();
                break;
            case KeyboardKey.Right:
                SetMoveRight(true);
                Move();
                break;
        }
    }
    
    public Vector2 GetPosition () {
        Vector2 gunPosition = Shape.Position; // tager positionen for det nederste venstre hjørne af objektet
        gunPosition = new Vector2(gunPosition.X + 0.045f, gunPosition.Y + 0.1f); //placerer skuddet lidt foran gunnen
        return gunPosition;
    }
}
