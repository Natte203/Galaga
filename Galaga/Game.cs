namespace Galaga;

using System.Collections.Generic;
using System.Numerics;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Game : DIKUGame {
        private Player player;
        private EntityContainer<Enemy> enemies;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f),
                            new Vector2(0.1f, 0.1f)),
            new Image("Galaga.Assets.Images.Player.png"));
        
        //5.2.3 Enemies: Animation
        List<Image> images =
            ImageStride.CreateStrides(4, "Galaga.Assets.Images.BlueMonster.png"); //ImageStride (ligger i Graphics) får dem til at bevæge sig. 4 henviser til antallet af frames i image-filen. The string er der hvor images bor.
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies); //Entitycontainer kan vist ses som en liste af entities. Tager int size.
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy( // Her tilføjes enemy entities til EntetyContaineren en ad gangen til vi når i-1.
                new DynamicShape(new Vector2(0.1f + (float)i * 0.1f, 0.9f), // Her placeres en enemy på skærmen. i sørger for, at de ikke ligger oveni hinanden.
                                new Vector2(0.1f, 0.1f)),
                new ImageStride(80, images))); // Her sættes en ImageStride: public ImageStride(int milliseconds, IEnumerable<Image> images) 
        }

        // TODO: Set key event handler (inherited window field of DIKUGame class)
    }

    public override void Render(WindowContext context) {
        player.RenderEntity(context);
        enemies.RenderEntities(context); // RenderEnteties ligger i EntityContainer.cs.
    }

    public override void Update() {
        // Code here
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        player.KeyHandler(action, key);
        // Husk ESC
    }
}
