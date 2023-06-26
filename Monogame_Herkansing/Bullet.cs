using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Herkansing
{
    internal class Bullet
    {
        private Texture2D _bulletTexture;
        private Vector2 _position;
        private float _speed;
        private bool _isActive;

        public Bullet(Texture2D texture, Vector2 position, float speed)
        {
            _bulletTexture = texture;
            _position = position;
            _speed = speed;
            _isActive = false;
        }

        public void Update(GameTime gameTime)
        {
            if (_isActive)
            {
                _position.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Deactivate bullet when it goes off-screen
                if (_position.X > _bulletTexture.Width)
                {
                    _isActive = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                spriteBatch.Draw(_bulletTexture, _position, Color.White);
            }
        }

        public void Shoot(Vector2 startPosition)
        {
            _position = startPosition;
            _isActive = true;
        }
    }
}