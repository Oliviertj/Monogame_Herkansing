using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Herkansing
{
    internal class Player
    {
        public Vector2 position;

        public Rectangle playerHitbox;
        private Texture2D _playerTexture;
        private float _speed;
        private float scale = 0.33f; // 50% scaling factor


        /// <summary>
        /// Constructor for Player's parameters.
        /// </summary>
        /// <param name="texture">Contains the player texture.</param>
        /// <param name="position">Contains the starting position for the Player.</param>
        /// <param name="speed">Contains the player speed.</param>
        public Player(Texture2D texture, Vector2 position, float speed)
        {
            _playerTexture = texture;
            this.position = position;
            _speed = speed;
            playerHitbox = new Rectangle((int)position.X, (int)position.Y, (int)(_playerTexture.Width * scale) , (int)(_playerTexture.Height * scale));
        }
        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {          
            KeyboardState keyboardState = Keyboard.GetState();
            // Update player movement based on keyboard input
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                position.Y -= _speed;
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                position.Y += _speed;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                position.X -= _speed;
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                position.X += _speed;

            // Restrict player movement within the screen boundaries
            position.X = MathHelper.Clamp(position.X, 0, screenWidth - 130);
            position.Y = MathHelper.Clamp(position.Y, 0, screenHeight - 65);
            playerHitbox.X = (int)position.X;
            playerHitbox.Y = (int)position.Y;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            Texture2D pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.Red });
            spriteBatch.Draw(pixelTexture, playerHitbox, Color.Red);
        }
    }

}
