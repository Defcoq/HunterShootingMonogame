using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _hunterTexture;
    private Vector2 _hunterPosition;
    private float _hunterSpeed = 5f;

    private List<Bird> _birds;
    private Texture2D _birdTexture;
    private float _birdSpeed = 2f;

    private List<Bullet> _bullets;  // Liste des balles
    private Texture2D _bulletTexture;  // Texture de la balle

    private Texture2D _backgroundTexture;

    private int _score;
    private SoundEffect _birdHitSound;
    private SoundEffect _hunterHitSound;
    private bool _isGameOver;
    private SpriteFont _font;

    private List<Fireball> _fireballs;
    private Texture2D _fireballTexture;
    private Random _random = new Random();
    private int _birdsKilled = 0; // Compteur d'oiseaux tués


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _hunterPosition = new Vector2(400, 500);
        _birds = new List<Bird>();
        _bullets = new List<Bullet>();  // Initialiser la liste des balles

        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 700;
        _graphics.ApplyChanges();

        _score = 0; // Initialisation du score
        _isGameOver = false; // Le jeu n'est pas terminé au début

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _hunterTexture = Content.Load<Texture2D>("hunter_128x256");
        _birdTexture = Content.Load<Texture2D>("bird_64x64");
        _backgroundTexture = Content.Load<Texture2D>("background");
        _bulletTexture = Content.Load<Texture2D>("bullet_16x32");

        _birdHitSound = Content.Load<SoundEffect>("birdHit_wav");
        _hunterHitSound = Content.Load<SoundEffect>("hunterHit_wav");

        _fireballTexture = Content.Load<Texture2D>("explosion_64x64");
        _fireballs = new List<Fireball>();

        _font = Content.Load<SpriteFont>("arial");

        // Ajouter les premiers oiseaux
        AddBirds(5); // Commencer avec 5 oiseaux

        base.LoadContent();
    }

    private void AddBirds(int numberOfBirds)
    {
        for (int i = 0; i < numberOfBirds; i++)
        {
            int birdXPosition = new Random().Next(0, Math.Max(1, 800 - _birdTexture.Width));
            _birds.Add(new Bird(_birdTexture, new Vector2(birdXPosition, -_birdTexture.Height), _birdSpeed));
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (_score <= -20)  // Fin du jeu si score < -20
        {
            _isGameOver = true;
        }

        if (_isGameOver)
        {
            // Si le jeu est terminé, on attend que l'utilisateur appuie sur "Entrée" pour recommencer
            KeyboardState currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                RestartGame();
            }
            return;
        }

        KeyboardState keyboardState = Keyboard.GetState();

        // Déplacement du chasseur
        if (keyboardState.IsKeyDown(Keys.Left))
            _hunterPosition.X -= _hunterSpeed;
        if (keyboardState.IsKeyDown(Keys.Right))
            _hunterPosition.X += _hunterSpeed;
        _hunterPosition.X = MathHelper.Clamp(_hunterPosition.X, 0, _graphics.PreferredBackBufferWidth - _hunterTexture.Width);

        // Tirer avec la touche 'Espace'
        if (keyboardState.IsKeyDown(Keys.Space))
        {
            _bullets.Add(new Bullet(_bulletTexture, new Vector2(_hunterPosition.X + _hunterTexture.Width / 2 - _bulletTexture.Width / 2, _hunterPosition.Y), 5f));
        }

        // Mettre à jour les balles
        foreach (var bullet in _bullets)
        {
            bullet.Update(gameTime);
        }

        // Mettre à jour les oiseaux
        foreach (var bird in _birds)
        {
            bird.Update(gameTime);

            // Calculer la probabilité de tirer un feu
            double baseProbability = 0.001; // Probabilité de base (1%)
            int multiplier = _birdsKilled / 10; // Augmente tous les 10 oiseaux tués
            double currentProbability = baseProbability + (multiplier * 0.01); // Augmentation progressive

            if (_random.NextDouble() < currentProbability)
            {
                _fireballs.Add(new Fireball(_fireballTexture, new Vector2(bird.Position.X + _birdTexture.Width / 2, bird.Position.Y + _birdTexture.Height), 3f));
            }

            //// 5% de chance par update de tirer un feu
            //if (_random.NextDouble() < 0.01)
            //{
            //    _fireballs.Add(new Fireball(_fireballTexture, new Vector2(bird.Position.X + _birdTexture.Width / 2, bird.Position.Y + _birdTexture.Height), 3f));
            //}
        }

        // Détection de collision entre les balles et les oiseaux
        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
            var bullet = _bullets[i];
            for (int j = _birds.Count - 1; j >= 0; j--)
            {
                var bird = _birds[j];
                if (bullet.GetBounds().Intersects(bird.GetBounds()))
                {
                    // Collision détectée, supprimer la balle et l'oiseau
                    _bullets.RemoveAt(i);
                    _birds.RemoveAt(j);

                    // Ajouter des points pour chaque oiseau tué
                    _score += 2;
                    // Collision détectée entre balle et oiseau
                    _birdsKilled++; // Incrémente le compteur d'oiseaux tués

                    // Jouer l'effet sonore
                    _birdHitSound.Play();

                    // Ajouter plus d'oiseaux à chaque fois qu'un oiseau est tué
                    if (_birds.Count < 10) // Limiter le nombre d'oiseaux à 10
                    {
                        AddBirds(1);
                    }

                    break;
                }
            }
        }

        for (int i = _fireballs.Count - 1; i >= 0; i--)
        {
            _fireballs[i].Update(gameTime);

            // Supprimer les projectiles hors de l'écran
            if (_fireballs[i].Position.Y > _graphics.PreferredBackBufferHeight)
            {
                _fireballs.RemoveAt(i);
            }
        }
        for (int i = _fireballs.Count - 1; i >= 0; i--)
        {
            if (_fireballs[i].GetBounds().Intersects(new Rectangle((int)_hunterPosition.X, (int)_hunterPosition.Y, _hunterTexture.Width, _hunterTexture.Height)))
            {
                _score -= 1; // Perdre 10 points
                _hunterHitSound.Play(); // Jouer le son du hunter touché
                _fireballs.RemoveAt(i); // Supprimer le projectile
            }
        }


        // Détection de collision entre le chasseur et les oiseaux
        foreach (var bird in _birds)
        {
            if (bird.GetBounds().Intersects(new Rectangle((int)_hunterPosition.X, (int)_hunterPosition.Y, _hunterTexture.Width, _hunterTexture.Height)))
            {
                // Perdre des points si le chasseur est touché par un oiseau
                _score -= 5;

                // Jouer l'effet sonore
                _hunterHitSound.Play();

                // Réinitialiser la position de l'oiseau
                bird.Position = new Vector2(new Random().Next(0, 800 - _birdTexture.Width), -_birdTexture.Height);
            }
        }



        base.Update(gameTime);
    }

    private void RestartGame()
    {
        _score = 0;  // Réinitialiser le score
        _isGameOver = false;  // Le jeu n'est plus terminé
        _birds.Clear();  // Réinitialiser les oiseaux
        AddBirds(5);  // Ajouter 5 oiseaux au départ
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // Dessiner le fond
        _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

        // Dessiner le chasseur
        _spriteBatch.Draw(_hunterTexture, _hunterPosition, Color.White);

        // Dessiner les oiseaux
        foreach (var bird in _birds)
        {
            bird.Draw(_spriteBatch);
        }

        // Dessiner les balles
        foreach (var bullet in _bullets)
        {
            bullet.Draw(_spriteBatch);
        }

        foreach (var fireball in _fireballs)
        {
            fireball.Draw(_spriteBatch);
        }

        // Afficher le score en haut de l'écran
        _spriteBatch.DrawString(_font, "Score: " + _score, new Vector2(10, 10), Color.White);

        // Afficher Game Over si le jeu est terminé
        if (_isGameOver)
        {
            _spriteBatch.DrawString(_font, "GAME OVER! Press Enter to Restart", new Vector2(250, 350), Color.Red);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
