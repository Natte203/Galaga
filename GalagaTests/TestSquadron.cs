namespace GalagaTests;

using System.Numerics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using Galaga;
using Galaga.Squadron;
using NUnit.Framework;

public class TestsSquadron {

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void TestAllSquadronsWithinBounds(int pick) {
        ISquadron squadron = pick switch {
            0 => new SquadronBox(new Vector2(0.1f, 0.8f)),
            1 => new SquadronDiamond(new Vector2(0.1f, 0.9f)),
            2 => new SquadronZigZag(new Vector2(0.1f, 0.9f)), 
            _ => new SquadronBox(new Vector2(0.1f, 0.8f))
        };

        for (int i = 0; i < squadron.numEnemies; i++) {
            var pos = squadron.GetPosition(i);
            Assert.That(pos.X, Is.InRange(0.1f, 0.9f));
            Assert.That(pos.Y, Is.InRange(0.49f, 1.0f));    //0.49 due to floating point precision issue with 0.5
        }

        int expected = pick switch {
            0 => 8,
            1 => 13,
            2 => 9,
            _ => 0
        };

        Assert.That(squadron.numEnemies, Is.EqualTo(expected));
    }
}