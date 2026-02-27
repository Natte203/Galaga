namespace GalagaTests;

using NUnit.Framework;
using Galaga;
using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;

public class TestsPlayer {
    // public Player testPlayer;
    public Player testPlayer2 = null!; // Her har jeg altså brugt AI.

    [SetUp]
    public void Setup() {
        // testPlayer = new Player( // Jeg har udkommenteret denne testPlayer, da vi ikke bruger den.
        //    new DynamicShape(new Vector2(0.45f, 0.1f), new Vector2(0.1f, 0.1f)),
        //    new Image("GalagaTests.Images.Player.png"));
        testPlayer2 = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f), new Vector2(0.1f, 0.1f)), // Jeg har ændret setuppet, så playerposition starter på (0.45f, 0.1f) fordi det er mere oplagt ift. testen.
            new NoImage());
    }

// ***** Testing Move()
    [Test]
    public void TestVelocityMove() { // Testing normal movement.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.01f, 0.0f); // Her sætter jeg velocity manuelt, så jeg undgår at der sker fejl.
        // Act
        testPlayer2.Move(); // Så kaldes metoden
        // Assert
        Assert.That(0.46f, Is.EqualTo(testPlayer2.Position.X).Within(0.000001)); // 
        //Assert.AreEqual(0.46f, testPlayer2.Position.X); // Jeg kører med rækkefølgen expected value, actual value. Skal vi tjekke om Y ændrer sig?
            // OBS! Med Assert.AreEqual() giver den fejl pga. minimale forskelle i tallene (floats er ikke perfekte). Jeg fandt en løsning på Stack Overflow
            // Med Assert.That(), hvor man kan indskrive en tolerance. Vi kan evt. snakke med Ida om det fredag.
    }

    [Test]
    public void TestLeftBoundaryMove() { // Testing that the player cannot move further than the left boundary.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(- 0.46f, 0.0f); // Her sætter jeg velocity manuelt, så jeg undgår at der sker fejl.
        // Act
        testPlayer2.Move(); // Så kaldes metoden
        // Assert
        Assert.AreEqual(0.0f, testPlayer2.Position.X); // Jeg kører med rækkefølgen expected value, actual value. Skal vi tjekke om Y ændrer sig?
    }

    [Test]
    public void TestRightBoundaryMove() { // Testing that the player cannot move further than the right boundary.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.55f, 0.0f); // Her sætter jeg velocity manuelt, så jeg undgår at der sker fejl.
        // Act
        testPlayer2.Move(); // Så kaldes metoden
        // Assert
        Assert.AreEqual(1.0f - testPlayer2.Shape.Extent.X, testPlayer2.Position.X); // Jeg kører med rækkefølgen expected value, actual value. Skal vi tjekke om Y ændrer sig?
    }

    // ***** Testing SetMoveLeft() and SetMoveRight()
    [Test]
    public void TestSetMoveLeftTrue() { // Testing that SetMoveLeft(true) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f); // Her sætter jeg velocity manuelt,
        // Act
        testPlayer2.SetMoveLeft(true);
        // Assert
        Assert.That(- 0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001)); // Velocity.X because we're only interested in that one (and then we can compare to the float))
            // And - 0.1 because that's the expected value of out Velocity's X. 
    }

    [Test]
    public void TestSetMoveLeftFalse() { // Testing that SetMoveLeft(false) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f); // Her sætter jeg velocity manuelt,
        // Act
        testPlayer2.SetMoveLeft(false);
        // Assert
        Assert.That(0.0f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001)); // Velocity.X because we're only interested in that one (and then we can compare to the float))
    }

    // ***** Testing SetMoveRight()
    [Test]
    public void TestSetMoveRightTrue() { // Testing that SetMoveRight(true) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f); // Her sætter jeg velocity manuelt,
        // Act
        testPlayer2.SetMoveRight(true);
        // Assert
        Assert.That(0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001)); // Velocity.X because we're only interested in that one (and then we can compare to the float))
    }

    [Test]
    public void TestSetMoveRightFalse() { // Testing that SetMoveRight(false) sets Velocity.X correctly.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f); // Her sætter jeg velocity manuelt,
        // Act
        testPlayer2.SetMoveRight(false);
        // Assert
        Assert.That(0.0f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001)); // Velocity.X because we're only interested in that one (and then we can compare to the float))
    }

    // ***** Testing KeyHandler()
    [Test]
    public void TestKeyHandlerKeyRelease() { // Testing that KeyHandler() does not run the switch with KeyRelease.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyRelease, KeyboardKey.Left);
        // Assert
        Assert.That(0.0f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
        }

    [Test]
    public void TestKeyHandlerLeft() { // Testing that KeyHandler() runs the switch correctly when KeyboardKey.Left.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Left);
        // Assert
        Assert.That(- 0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
        }

    [Test]
    public void TestKeyHandlerRight() { // Testing that KeyHandler() runs the switch correctly when KeyboardKey.Right.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.Right);
        // Assert
        Assert.That(0.01f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
        }

    [Test]
    public void TestKeyHandlerInvalidKey() { // Testing that KeyHandler() does not run the switch with invalid input.
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Velocity = new Vector2(0.5f, 0.0f);
        // Act
        testPlayer2.KeyHandler(KeyboardAction.KeyPress, KeyboardKey.G);
        // Assert
        Assert.That(0.5f, Is.EqualTo(testPlayer2.Velocity.X).Within(0.000001));
        }

    // ***** Testing GetPosition
    [Test]
    public void TestGetPosition() {
        // Arrange
        ((DynamicShape)testPlayer2.Shape).Position = new Vector2(0.5f, 0.1f);
        // Act
        var result = testPlayer2.GetPosition();
        // Assert
        Assert.That(0.5f + 0.045f, Is.EqualTo(result.X).Within(0.000001));
        Assert.That(0.1f + 0.1f, Is.EqualTo(result.Y).Within(0.000001));
        }
    }