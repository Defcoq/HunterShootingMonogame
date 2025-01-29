//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//public class Bullet
//{
//    public Vector2 Position;
//    public float Speed;
//    public Texture2D Texture;

//    public Bullet(Texture2D texture, Vector2 position, float speed)
//    {
//        Texture = texture;
//        Position = position;
//        Speed = speed;
//    }

//    public void Update(GameTime gameTime)
//    {
//        // Déplacer la balle vers le haut
//        Position.Y -= Speed;
//    }

//    public void Draw(SpriteBatch spriteBatch)
//    {
//        // Dessiner la balle
//        spriteBatch.Draw(Texture, Position, Color.White);
//    }
//}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Bullet
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public float Speed { get; set; }

    public Bullet(Texture2D texture, Vector2 position, float speed)
    {
        Texture = texture;
        Position = position;
        Speed = speed;
    }

    public void Update(GameTime gameTime)
    {
     

        var position = Position;  // On récupère la position dans une variable temporaire

        position.Y -= Speed;
        Position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    // Retourne le rectangle de la balle pour la détection de collision
    public Rectangle GetBounds()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
    }
}

