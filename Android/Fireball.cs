using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Fireball
{
    private Texture2D _texture;
    public Vector2 Position;
    private float _speed;

    public Fireball(Texture2D texture, Vector2 position, float speed)
    {
        _texture = texture;
        Position = position;
        _speed = speed;
    }

    public void Update(GameTime gameTime)
    {
        Position.Y += _speed; // Déplacement vers le bas
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, Color.White);
    }

    public Rectangle GetBounds()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
    }
}
