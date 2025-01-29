using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Bird
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }  // La propriété Position doit avoir un setter.
    public float Speed { get; set; }

    public Bird(Texture2D texture, Vector2 startPosition, float speed)
    {
        Texture = texture;
        Position = startPosition;
        Speed = speed;
    }

    public void Update(GameTime gameTime)
    {
        // Déplacer l'oiseau vers le bas
        var position = Position;  // On récupère la position dans une variable temporaire

        position.Y += Speed;  // On modifie la position localement

        // Réinitialiser la position si l'oiseau sort de l'écran
        if (position.Y > 600)  // Ajuste la hauteur de l'écran ici si nécessaire
        {
            position.Y = -Texture.Height;
            position.X = new Random().Next(0, 800 - Texture.Width);  // Réapparaître aléatoirement en haut
        }

        Position = position;  // On réassigne la position modifiée à la propriété Position
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    // Méthode pour obtenir le rectangle de collision de l'oiseau
    public Rectangle GetBounds()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
    }
}
