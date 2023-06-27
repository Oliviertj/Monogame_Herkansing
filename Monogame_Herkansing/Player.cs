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
        private KeyboardState _previousKeyboardState;
        private Texture2D _playerTexture;
        private Vector2 _position;
        private float _speed;
        private bool isPressed;

        /// <summary>
        /// Constructor for Player's parameters.
        /// </summary>
        /// <param name="texture">Contains the player texture.</param>
        /// <param name="position">Contains the starting position for the Player.</param>
        /// <param name="speed">Contains the player speed.</param>
        public Player(Texture2D texture, Vector2 position, float speed)
        {
            _playerTexture = texture;
            _position = position;
            _speed = speed;
        }
        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // Update player movement based on keyboard input
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                _position.Y -= _speed;
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                _position.Y += _speed;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                _position.X -= _speed;
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                _position.X += _speed;

            // Restrict player movement within the screen boundaries
            _position.X = MathHelper.Clamp(_position.X, 0, screenWidth - 130);
            _position.Y = MathHelper.Clamp(_position.Y, 0, screenHeight - 120);

            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Shoot bullet when spacebar is pressed
            if (currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
            {
              
            }

            _previousKeyboardState = currentKeyboardState;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 0.33f; // 50% scaling factor

            spriteBatch.Draw(_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }

}
