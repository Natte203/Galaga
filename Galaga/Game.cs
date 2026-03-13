namespace Galaga;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Security.Principal;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Galaga.Hit;
using Galaga.Movement;
using Galaga.Squadron;
using Microsoft.VisualBasic;

public class Game : DIKUGame {
    private Player player;
    private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>(0);
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private GameEventBus gameEventBus;
    private AnimationContainer enemyExplosions = new AnimationContainer(0);
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f),
                            new Vector2(0.1f, 0.1f)),
            new Image("Galaga.Assets.Images.Player.png"));

        gameEventBus = new GameEventBus();
        gameEventBus.Subscribe<AddExplosionEvent>(AddExplosion);

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image("Galaga.Assets.Images.BulletRed2.png");
        explosionStrides = ImageStride.CreateStrides(
            8, "Galaga.Assets.Images.Explosion.png"
        );
        CreateSquadron();
    }

    //Helper for CreateSquadron
    private ISquadron CreateFormation(int pick, Vector2 origin) {
        return pick switch {
            0 => new SquadronBox(origin),
            1 => new SquadronDiamond(origin),
            2 => new SquadronZigZag(origin),
            _ => new SquadronBox(origin)
        };
    }

    private void CreateSquadron() {
        var random = new Random();
        int pick = random.Next(3);                                              //temporary origin
        var origin = CreateFormation(pick, Vector2.Zero).GetSafeOrigin(random); // get safe origin
        ISquadron squadron = CreateFormation(pick, origin);                     // recreate with real origin
        List<Image> enemyStrides = ImageStride.CreateStrides(4, "Galaga.Assets.Images.BlueMonster.png");
        List<Image> enragedStrides = ImageStride.CreateStrides(4, "Galaga.Assets.Images.BlueMonster.png");

        enemyExplosions = new AnimationContainer(squadron.numEnemies);
        enemies = squadron.CreateEnemies(
            enemyStrides,
            enragedStrides,
            () => new NoMovement(), //factory lambda 
            () => new DefaultHit(), //factory lambda
            gameEventBus);
    }


    private void iterateShots() {
        float topBoundary = 1f;
        playerShots.Iterate(shots => {
            shots.Shape.Move();
            if (shots.Shape.Position.Y + shots.Shape.Extent.Y >= topBoundary) {
                shots.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    CollisionData data =
                        CollisionDetection.Aabb(
                            shots.Shape.AsDynamicShape(), enemy.Shape.AsDynamicShape()
                        );
                    if (data.Collision) {
                        shots.DeleteEntity();
                        enemy.DeleteEnemy();
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

    ~Game() {
        gameEventBus.Unsubscribe<AddExplosionEvent>(AddExplosion);
    }

    public override void Render(WindowContext context) {
        player.RenderEntity(context);
        enemies.RenderEntities(context);
        enemyExplosions.RenderAnimations(context);
        playerShots.RenderEntities(context);
    }

    public override void Update() {
        iterateShots();
        player.Move();
        gameEventBus.ProcessEvents();
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
