namespace Galaga;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Galaga.Hit;
using Galaga.Movement;
using Microsoft.VisualBasic;

public class Game : DIKUGame {
    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private GameEventBus gameEventBus;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vector2(0.45f, 0.1f),
                            new Vector2(0.1f, 0.1f)),
            new Image("Galaga.Assets.Images.Player.png"));

        gameEventBus = new GameEventBus();
        gameEventBus.Subscribe<AddExplosionEvent>(AddExplosion);

        IHitStrategy[] strategies =
            new IHitStrategy[] {new IncreaseSpeedStrategy(),
            new TeleportStrategy(),
            new CauseEnrageStrategy()};
        Random random = new Random();

        List<Image> images =
            ImageStride.CreateStrides(4, "Galaga.Assets.Images.BlueMonster.png");
        List<Image> enragedImages =
            ImageStride.CreateStrides(2, "Galaga.Assets.Images.RedMonster.png");
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);

        for (int i = 0; i < numEnemies; i++) {
            var randomStratInt = random.Next(0, 3);
            var hitStrat = strategies[randomStratInt];

            var moveStrat = new NoMove();

            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vector2(0.1f + (float) i * 0.1f, 0.9f),
                                new Vector2(0.1f, 0.1f)),
                new ImageStride(80, images), new ImageStride(80, enragedImages),
                gameEventBus, hitStrat, moveStrat));
        }

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image("Galaga.Assets.Images.BulletRed2.png");

        enemyExplosions = new AnimationContainer(numEnemies);
        explosionStrides = ImageStride.CreateStrides(
            8, "Galaga.Assets.Images.Explosion.png"
        );
    }

    private void IterateShots() {
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
                        enemy.TakeHit();
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
        IterateShots();
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
