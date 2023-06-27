using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Herkansing
{
    internal class Bullet
    {
        private KeyboardState _previousKeyboardState;
        private Texture2D _bulletTexture;
        private Vector2 _bulletPosition;
        private float _speed;
        private bool _isActive;
        private Vector2 _playerPosition;

        public Bullet(Texture2D texture, Vector2 position, float speed)
        {
            _bulletTexture = texture;
            _bulletPosition = position;
            _speed = speed;
            _isActive = false;
        }

        public void Update(GameTime gameTime)
        {

            if (_isActive)
            {
                _bulletPosition.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Deactivate bullet when it goes off-screen
                if (_bulletPosition.X > _bulletTexture.Width)
                {
                    _isActive = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                spriteBatch.Draw(_bulletTexture, _bulletPosition, Color.White);
            }
        }

        public void Shoot(Vector2 startPosition)
        {
            _bulletPosition = startPosition;
            _isActive = true;
        }
    }
}