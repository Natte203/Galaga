namespace Galaga;

using System.Numerics;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
public class Game : DIKUGame {
        private Player player;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f),
                            new Vector2(0.1f, 0.1f)),
            new Image("Galaga.Assets.Images.Player.png"));
        // TODO: Set key event handler (inherited window field of DIKUGame class)
    }

    public override void Render(WindowContext context) {
        player.RenderEntity(context);
    }

    public override void Update() {
        // Code here
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        player.KeyHandler(action, key);
        // Husk ESC
    }
}