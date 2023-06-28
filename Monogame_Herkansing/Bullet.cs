using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Monogame_Herkansing
{
    internal class Bullet
    {
        public Player player;
        public List<Bullet> playerBullets = new List<Bullet>();

        private Texture2D _bulletTexture;
        private Vector2 _position;
        private float _speed;
        private float _bulletTime;
        private bool _isActive;
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

            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Shoot bullet when spacebar is pressed
            if (currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
                
                Shoot();
            }

            _previousKeyboardState = currentKeyboardState;
  
            if (_isActive)
            {
                foreach (Bullet bullet in playerBullets.ToArray())
                {
                    bullet._position.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _bulletTime = (float)gameTime.ElapsedGameTime.Seconds;
                }

                // Removes bullets if at least 1 is in the list.
                for (int i = playerBullets.Count - 1; i >= 0; i--)
                {
                    Bullet bullet = playerBullets[i];

                    if (bullet._position.X > _screenWidth - 150)
                    {
                        playerBullets.RemoveAt(i);
                    }
                    if (_bulletTime >= 15f)
                    {
                        playerBullets.RemoveAt(i);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                float scale = 0.4f;
                foreach (Bullet bullet in playerBullets)
                {                    
                     spriteBatch.Draw(bullet._bulletTexture, bullet._position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
        }

        public void Shoot()
        {
            _position.X = player.position.X + 100; 
            _position.Y = player.position.Y + 50;
            playerBullets.Add(new Bullet(_bulletTexture, _position, _speed, _screenWidth));
            _isActive = true;
        }
    }
}