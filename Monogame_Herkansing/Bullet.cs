using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogame_Herkansing
{
    internal class Bullet
    {
        public Player player;
        public List<Bullet> playerBullets = new List<Bullet>();

        private Texture2D _bulletTexture;
        private Vector2 _position;
        private float _speed;
        private bool _isActive;
        private float _bulletTime;
        private int _screenWidth;
        private KeyboardState _previousKeyboardState;

        public Bullet(Texture2D texture, Vector2 position, float speed, int screenWidth)
        {
            _bulletTexture = texture;
            _position = position;
            _speed = speed;
            _screenWidth = screenWidth;
            _isActive = false;          
        }

        public void Update(GameTime gameTime)
        {         
            Console.WriteLine("Reached update");

            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Shoot bullet when spacebar is pressed
            if (currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
                
                Shoot();
            }


            _previousKeyboardState = currentKeyboardState;

            if (_isActive)
            {
                _bulletTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _position.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                playerBullets.Add(new Bullet(_bulletTexture, _position, _speed, _screenWidth));
                // Check for collisions with bullets
                foreach (Bullet bullet in playerBullets.ToArray())
                {
                    // Deactivate bullet when it goes off-screen
                    if (_position.X > _screenWidth - 100)
                    {
                        if (_bulletTime >= 1.5f)
                        {
                           
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                float scale = 0.4f;

                spriteBatch.Draw(_bulletTexture, _position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        public void Shoot()
        {
            _position.X = player.position.X + 100; 
            _position.Y = player.position.Y + 50;
            _isActive = true;
        }
    }
}