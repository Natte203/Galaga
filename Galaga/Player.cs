namespace Galaga;

using System;
using System.Numerics;
using System.Reflection.Metadata;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;

public class Player : Entity {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;
    
    public Vector2 Position { 
        get { return Shape.Position; }
        private set { Shape.Position = value; }
    }
    public Vector2 Velocity { 
        get { return ((DynamicShape) Shape).Velocity; }
        private set { ((DynamicShape) Shape).Velocity = value; }
        }
    public Vector2 Extent {
        get { return Shape.Extent; }
        private set { Shape.Position = value; }
    }
 
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    private void UpdateVelocity(float move) {
        Velocity = new Vector2(move, Velocity.Y); 
    }

    public void Move() {
        ((DynamicShape) Shape).Move();
        Position = new Vector2(Math.Clamp(Position.X, 0.0f, 1.0f - Extent.X), Position.Y);
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
        Vector2 gunPosition = Position; // tager positionen for det nederste venstre hjørne af objektet
        gunPosition = new Vector2(gunPosition.X + 0.045f, gunPosition.Y + 0.1f); //placerer skuddet lidt foran gunnen
        return gunPosition;
    }
}
