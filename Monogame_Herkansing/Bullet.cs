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
        public int i;
        public int bulletsFired;
        public int enemiesHit;
        public Rectangle bulletHitbox;
        private Texture2D _bulletTexture;
        private Vector2 _position;
        private float _speed;
        private float _bulletTime;
        private int _screenWidth;
        private KeyboardState _previousKeyboardState;
        private float scale = 0.4f;
        public Rectangle bulletRect;
                    

        /// <summary>
        /// Constructor with parameters used to create the bullet.
        /// </summary>
        /// <param name="texture">Contains the bullet texture.</param>
        /// <param name="position">Contains the player start position.</param>
        /// <param name="speed">Contains the bullet travel speed.</param>
        /// <param name="screenWidth">contains the screenwidth.</param>
        public Bullet(Texture2D texture, Vector2 position, float speed, int screenWidth)
        {
            _bulletTexture = texture;
            _position = position;
            _speed = speed;
            _screenWidth = screenWidth;  
        }

        public void Update(GameTime gameTime, CollisionHandler cHandler, Enemy enemy)
        {
            cHandler.PlayerCollisionCheck(player, enemy);
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Shoot bullet when spacebar is pressed
            if (currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
                Shoot();
            }

            _previousKeyboardState = currentKeyboardState;

            foreach (Bullet bullet in playerBullets)
            {
                Rectangle bulletRect = new Rectangle((int)bullet._position.X, (int)bullet._position.Y, (int)(_bulletTexture.Width * scale), (int)(_bulletTexture.Height * scale));
                bullet._position.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                bulletRect.X = (int)bullet._position.X;
                _bulletTime = (float)gameTime.ElapsedGameTime.Seconds;
                Console.WriteLine(bulletRect.X);
                // Use the updated bulletRect here as needed.
                bulletRect.X = (int)(bullet._position.X + (_speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
                
            }


            // REMOVES BULLETS IF AT LEAST 1 IS IN THE LIST.
            for (i = playerBullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = playerBullets[i];
                if (bullet._position.X > _screenWidth)
                {
                    cHandler.RemoveAt(i, playerBullets);
                }
                if (_bulletTime >= 15f)
                {
                    cHandler.RemoveAt(i, playerBullets);
                }

                if (cHandler.CollisionCheck(this, player, enemy) == true)
                {
                    cHandler.RemoveAt(i, playerBullets);
                    enemiesHit++;
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in playerBullets)
            {
                spriteBatch.Draw(bullet._bulletTexture, bullet._position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                Texture2D pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                pixelTexture.SetData(new[] { Color.Red });
                spriteBatch.Draw(pixelTexture, bulletHitbox, Color.Red);
                spriteBatch.Draw(_bulletTexture, bulletRect, Color.Red);
            }
        }
        public void Shoot()
        {
            bulletsFired++;
            // Bullet position placed slaightly different due to scaling.
            _position.X = player.position.X + 100;
            _position.Y = player.position.Y;
            playerBullets.Add(new Bullet(_bulletTexture, _position, _speed, _screenWidth));
        }
    }
}