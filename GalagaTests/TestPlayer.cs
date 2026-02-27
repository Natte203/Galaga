namespace GalagaTests;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using Galaga;
using NUnit.Framework;

public class TestsPlayer {
    public Player testPlayer2 = new Player(
        new DynamicShape(new Vector2(0.45f, 0.1f), new Vector2(0.1f, 0.1f)),
        new NoImage());

    [SetUp]
    public void Setup() {
        testPlayer2 = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f), new Vector2(0.1f, 0.1f)),
            new NoImage());
    }

    // ***** Testing Move()
    [Test]
    public void TestVelocityMove() { // Testing normal movement.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.01f, 0.0f);
        // Act
        testPlayer2.Move();
        // Assert
        Assert.That(0.46f, Is.EqualTo(testPlayer2.Position.X).Within(0.000001)); // 
    }

    [Test]
    public void TestLeftBoundaryMove() { // Testing that the player cannot move further than the left boundary.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(-0.46f, 0.0f);
        // Act
        testPlayer2.Move();
        // Assert
        Assert.AreEqual(0.0f, testPlayer2.Position.X);
    }

    [Test]
    public void TestRightBoundaryMove() { // Testing that the player cannot move further than the right boundary.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.55f, 0.0f);
        // Act
        testPlayer2.Move();
        // Assert
        Assert.AreEqual(1.0f - testPlayer2.Shape.Extent.X, testPlayer2.Position.X);
    }

    // ***** Testing SetMoveLeft() and SetMoveRight()
    [Test]
    public void TestSetMoveLeftTrue() { // Testing that SetMoveLeft(true) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.SetMoveLeft(true);
        // Assert
        Assert.That(-0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    [Test]
    public void TestSetMoveLeftFalse() { // Testing that SetMoveLeft(false) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.SetMoveLeft(false);
        // Assert
        Assert.That(0.0f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    // ***** Testing SetMoveRight()
    [Test]
    public void TestSetMoveRightTrue() { // Testing that SetMoveRight(true) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.SetMoveRight(true);
        // Assert
        Assert.That(0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    [Test]
    public void TestSetMoveRightFalse() { // Testing that SetMoveRight(false) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.SetMoveRight(false);
        // Assert
        Assert.That(0.0f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    // ***** Testing KeyHandler()
    [Test]
    public void TestKeyHandlerKeyReleaseLeft() { // Testing that KeyHandler() does runs the correct switch with KeyRelease.Left.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.Left);
        // Assert
        Assert.That(0.0f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    [Test]
    public void TestKeyHandlerKeyReleaseRight() { // Testing that KeyHandler() does runs the correct switch with KeyRelease.Right.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.Right);
        // Assert
        Assert.That(0.0f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    [Test]
    public void TestKeyHandlerLeft() { // Testing that KeyHandler() runs correct switch in the right way when KeyboardKey.Left.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Left);
        // Assert
        Assert.That(-0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    [Test]
    public void TestKeyHandlerRight() { // Testing that KeyHandler() runs the correct switch in the right way when KeyboardKey.Right.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Right);
        // Assert
        Assert.That(0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    [Test]
    public void TestKeyHandlerInvalidKey() { // Testing that KeyHandler() does not run the switch with invalid input.
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.G);
        // Assert
        Assert.That(0.5f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
    }

    // ***** Testing GetPosition
    [Test]
    public void TestGetPosition() {
        // Arrange
        ((DynamicShape) testPlayer2.Shape).Position = new Vector2(0.5f, 0.1f);
        // Act
        var result = testPlayer2.GetPosition();
        // Assert
        Assert.That(0.5f + 0.045f, Is.EqualTo(result.X).Within(0.000001));
        Assert.That(0.1f + 0.1f, Is.EqualTo(result.Y).Within(0.000001));
    }
}