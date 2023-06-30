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
        public Rectangle bulletHitbox;
        private Texture2D _bulletTexture;
        private Vector2 _position;
        private float _speed;
        private int _screenWidth;
        private KeyboardState _previousKeyboardState;
        private float scale = 0.4f;
        private bool _isActive = false;
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
            bulletHitbox = new Rectangle((int)position.X, (int)position.Y, (int)(_bulletTexture.Width * scale), (int)(_bulletTexture.Height * scale));
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
                bullet._position.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                bulletHitbox.X = (int)bullet._position.X;
                bulletHitbox.Y = (int)bullet._position.Y;
            }


            // ensures that bullets are removed from the list if they collide
            for (i = playerBullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = playerBullets[i];
                if (bullet._position.X > _screenWidth)
                {
                    cHandler.RemoveAt(i, playerBullets);
                }
                if (cHandler.CollisionCheck(this, player, enemy) == true)
                {
                    cHandler.RemoveAt(i, playerBullets);                 
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in playerBullets)
            {
                spriteBatch.Draw(bullet._bulletTexture, bullet._position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
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