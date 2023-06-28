using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Herkansing;
using System;

internal class Enemy
{
    private Texture2D _enemyTexture;
    private Vector2 _position;
    private Rectangle enemyHitbox;
    private float _speed;
    private bool _isMovingLeft;
    private int _windowWidth;
    private int _windowHeight;

    /// <summary>
    /// Constructor for the parameters needed for the Enemy logic.
    /// </summary>
    /// <param name="texture">Contains the Enemy texture. </param>
    /// <param name="position">Contains the starting position for the Enemy. </param>
    /// <param name="speed">Contains the enemy speed. </param>
    /// <param name="windowWidth">Windowwidth used for the screenwidth.</param>
    public Enemy(Texture2D texture, Vector2 position, float speed, int windowWidth, int windowHeight)
    {
        _enemyTexture = texture;
        _position = position;
        _speed = speed;
        _isMovingLeft = true;
        _windowWidth = windowWidth;
        _windowHeight = windowHeight;
        enemyHitbox = new Rectangle((int)position.X, (int)position.Y, _enemyTexture.Width / 2, _enemyTexture.Height / 3);
    }

    public void Update(GameTime gameTime)
    {
        if (_isMovingLeft)
        {
            _position.X -= _speed;

            if (_position.X + _enemyTexture.Width <= 0)
            {
                // Enemy reached the left screen barrier, reset its position to appear from the right side
                Random rnd = new Random();
                _position.X = _windowWidth;
                _position.Y = rnd.Next(1, _windowHeight - 100);
            }
        }
        enemyHitbox.X = (int)_position.X;
        enemyHitbox.Y = (int)_position.Y;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        float scale = 0.5f;

        spriteBatch.Draw(_enemyTexture, _position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

    }
}