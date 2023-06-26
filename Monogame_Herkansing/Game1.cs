﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Herkansing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _playerTexture;
      //private Texture2D _bulletTexture;
        private Texture2D _enemyTexture;

        private Player _player;
        private Enemy _enemy;

        public int windowHeight;
        public int windowWidth;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            windowWidth = _graphics.PreferredBackBufferWidth = 1600;
            windowHeight = _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _playerTexture = Content.Load<Texture2D>("Rocket");
          //  _bulletTexture = Content.Load<Texture2D>("bullet");
            _enemyTexture = Content.Load<Texture2D>("Ufo");

            Vector2 playerPosition = new Vector2(0, windowHeight / 2);
            float playerSpeed = 7.5f;
            _player = new Player(_playerTexture, playerPosition, playerSpeed);

            Vector2 enemyPosition = new Vector2(windowWidth, windowHeight / 2);
            float enemySpeed = 5f;
            _enemy = new Enemy(_enemyTexture, enemyPosition, enemySpeed, windowWidth, windowHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime, windowWidth, windowHeight);

            _enemy.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            _spriteBatch.Begin();

            // Draw the player
            _player.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}