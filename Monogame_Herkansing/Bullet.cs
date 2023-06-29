using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Monogame_Herkansing
{
    public class Bullet
    {
        public Player player;
        public List<Bullet> playerBullets = new List<Bullet>();
        private Game1 _game;
        private Enemy _enemy;
        private Rectangle bulletHitbox;
        private Texture2D _bulletTexture;
        private Vector2 _position;
        private float _speed;
        private float _bulletTime;
        private bool _isActive;
        private int _screenWidth;
        private KeyboardState _previousKeyboardState;
        private float scale = 0.4f;

        /// <summary>
        /// Constructor with parameters used to create the bullet.
        /// </summary>
        /// <param name="texture">Contains the bullet texture.</param>
        /// <param name="position">Contains the player start position.</param>
        /// <param name="speed">Contains the bullet travel speed.</param>
        /// <param name="screenWidth">contains the screenwidth.</param>
        public Bullet(Texture2D texture, Vector2 position, float speed, int screenWidth, Enemy enemy, Game1 game)
        {
            _bulletTexture = texture;
            _position = position;
            _speed = speed;
            _screenWidth = screenWidth;
            _isActive = false;
            bulletHitbox = new Rectangle((int)position.X, (int)position.Y, (int)(_bulletTexture.Width * scale), (int)(_bulletTexture.Height * scale));
            _enemy = enemy;
            _game = game;
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
                    bulletHitbox.X = (int)bullet._position.X;
                    bulletHitbox.Y = (int)bullet._position.Y;

                    // Check for collision with enemy
                    if (bulletHitbox.Intersects(_enemy.enemyHitbox))
                    {
                        _game.DestroyBullet(bullet);

                        _game.DeactivateEnemy(_enemy, new Rectangle(0,0,0,0));
                        break; 
                    }
                }

                // Removes bullets if at least 1 is in the list.
                for (int i = playerBullets.Count - 1; i >= 0; i--)
                {
                    Bullet bullet = playerBullets[i];
                    if (bullet._position.X > _screenWidth)
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
                foreach (Bullet bullet in playerBullets)
                {                    
                     spriteBatch.Draw(bullet._bulletTexture, bullet._position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                    Texture2D pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                    pixelTexture.SetData(new[] { Color.Red });
                    spriteBatch.Draw(pixelTexture, bulletHitbox, Color.Red);
                }
            }
        }
        public void Shoot()
        {
            // Bullet position placed slaightly different due to scaling.
            _position.X = player.position.X + 100; 
            _position.Y = player.position.Y;
            playerBullets.Add(new Bullet(_bulletTexture, _position, _speed, _screenWidth, _enemy, _game));
            _isActive = true;
        }
    }
}