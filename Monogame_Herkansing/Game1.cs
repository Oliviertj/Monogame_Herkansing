using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Herkansing
{
    public class Game1 : Game
    {
        public int windowHeight;
        public int windowWidth;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _playerTexture;
        private Texture2D _bulletTexture;
        private Texture2D _enemyTexture;

        private Player _player;
        private Enemy _enemy;
        private Bullet _bullet;

        private float _enemySpeed = 8f;
        private float _playerSpeed = 7.5f;
        private float _bulletSpeed = 1000f;
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
            _enemyTexture = Content.Load<Texture2D>("Ufo");
            _bulletTexture = Content.Load<Texture2D>("Bullet");

            Vector2 playerPosition = new Vector2(0, windowHeight / 2);
            _player = new Player(_playerTexture, playerPosition, _playerSpeed);

            Vector2 enemyPosition = new Vector2(windowWidth, windowHeight / 2); 
            _enemy = new Enemy(_enemyTexture, enemyPosition, _enemySpeed, windowWidth, windowHeight);

            _bullet = new Bullet(_bulletTexture, playerPosition, _bulletSpeed, windowWidth);
            _bullet.player = _player;

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
            _bullet.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            _spriteBatch.Begin();

            // Draw the player
            _player.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);
            _bullet.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}