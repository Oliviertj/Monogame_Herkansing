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
        public void Update(GameTime gameTime)
        {
            // Get the current keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            // Update player movement based on keyboard input
            if (keyboardState.IsKeyDown(Keys.W))
                _position.Y -= _speed;
            if (keyboardState.IsKeyDown(Keys.S))
                _position.Y += _speed;
            if (keyboardState.IsKeyDown(Keys.A))
                _position.X -= _speed;
            if (keyboardState.IsKeyDown(Keys.D))
                _position.X += _speed;

            // Check if spacebar is pressed and create new bullet if it is not already pressed.
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (!isPressed)
                {
                    _speed = _speed * 1.25f;
                    isPressed = true;
                }
            }
            else
            {
                _speed = _speed;
                isPressed = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture, _position, Color.White);
        }
    }

}
