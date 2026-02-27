namespace Galaga;

using System.Collections.Generic;
using System.Numerics;
using Microsoft.VisualBasic;
using System.Reflection;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events; 
using DIKUArcade.Physics;
using System.Security.Principal;

public class Game : DIKUGame {
    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots; //added w 5.2.4
    private IBaseImage playerShotImage;             //Added w 5.2.4
    private GameEventBus gameEventBus;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

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
        //5.2.4 Player: Shooting
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image("Galaga.Assets.Images.BulletRed2.png"); 

        // 5.3 Events
        gameEventBus = new GameEventBus(); //GameEventBus obj (Events.GameEventBus)
        gameEventBus.Subscribe<AddExplosionEvent>(AddExplosion); //Tilføjer AddExplosion til eventbus
        enemyExplosions = new AnimationContainer(numEnemies); //Instantierer en container med 8 pladser
        explosionStrides = ImageStride.CreateStrides(
            8, "Galaga.Assets.Images.Explosion.png" 
        ); //Laver en liste med 8 billieder af eksplosion
    }
    
    private void IterateShots() {
        float topBoundary = 1f; 
        playerShots.Iterate(shots => {  //=>{} er en lambda funktion. shots er én instance af Playershots. Den tilgås via shots.Shape
            shots.Shape.Move(); //bevæger shots
            if (shots.Shape.Position.Y + shots.Shape.Extent.Y >= topBoundary) { 
                shots.DeleteEntity(); // hvis shot kommer udenfor skærmens top slettes det
            } 
            else {
                enemies.Iterate(enemy => { 
                CollisionData data = 
                    CollisionDetection.Aabb(
                        shots.Shape.AsDynamicShape(), enemy.Shape.AsDynamicShape() //shots og enemy castes til at være dynamicshapes for at kollision kan tjekkes
                    ); //beregner kollision som objekt
                if (data.Collision) { //bestemmer en bool udfra kollisions-objektet (CollisionData.cs)
                    shots.DeleteEntity(); //shots slettes ved kollision -> true
                    gameEventBus.RegisterEvent<AddExplosionEvent>(
                        new AddExplosionEvent(enemy.Position, enemy.Extent)); //Tilføjer et event til eventbus køen
                    enemy.DeleteEntity(); //shots slettes ved kollision -> true
                } 
                });
            }
        });
    }

    private void createShot() {
        Vector2 pos = player.GetPosition();
        PlayerShot shot = new PlayerShot(pos, playerShotImage);
        playerShots.AddEntity(shot);
    }

    ~Game() { //Cleanup?
        gameEventBus.Unsubscribe<AddExplosionEvent>(AddExplosion);
    }    

    public override void Render(WindowContext context) {
        player.RenderEntity(context);
        enemies.RenderEntities(context); // RenderEnteties ligger i EntityContainer.cs.
        enemyExplosions.RenderAnimations(context);
        playerShots.RenderEntities(context); 
    }

    public override void Update() {
        IterateShots();
        player.Move();
        gameEventBus.ProcessEvents(); //Kører events gemt i gameEventBus & fortæller subscribers at events er kørt
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        player.KeyHandler(action, key);
        if (action != KeyboardAction.KeyRelease) {
            return;
        }
        switch (key) {
            case KeyboardKey.Space:
                createShot();
                break;
        }
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public void AddExplosion(AddExplosionEvent addExplosionEvent) {
        enemyExplosions.AddAnimation(
            new StationaryShape(addExplosionEvent.Position, addExplosionEvent.Extent), 
            EXPLOSION_LENGTH_MS,
            new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides));
    }
}
