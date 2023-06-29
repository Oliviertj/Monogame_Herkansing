using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;

namespace Monogame_Herkansing
{
    public class Game1 : Game
    {
        public int windowHeight;
        public int windowWidth;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle backgroundRectangle;

        private Texture2D _playerTexture;
        private Texture2D _bulletTexture;
        private Texture2D _enemyTexture;
        private Texture2D _backgroundTexture;

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
            _backgroundTexture = Content.Load<Texture2D>("Space-Background");

            Vector2 playerPosition = new Vector2(0, windowHeight / 2);
            _player = new Player(_playerTexture, playerPosition, _playerSpeed, this);

            Vector2 enemyPosition = new Vector2(windowWidth, windowHeight / 2); 
            _enemy = new Enemy(_enemyTexture, enemyPosition, _enemySpeed, windowWidth, windowHeight, this);

            _bullet = new Bullet(_bulletTexture, playerPosition, _bulletSpeed, windowWidth, _enemy, this);
            _bullet.player = _player;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            backgroundRectangle = new Rectangle(0, 0, windowWidth, windowHeight);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        public void DestroyBullet(Bullet bullet)
        {
            _bullet.playerBullets.Remove(bullet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemyHitbox"></param>
        /// <param name="rectangle">Rectangle can disable or enable.</param>
        public void DeactivateEnemy(Enemy enemy, Rectangle rectangle)
        {
            enemy.enemyHitbox = new Rectangle(0, 0, 0, 0);
        }
        public void ActivateEnemy(Rectangle enemyRectangle, float scale)
        {
            enemyRectangle = new Rectangle(enemyRectangle.X, enemyRectangle.Y, (int)(_playerTexture.Width * scale), (int)(_playerTexture.Height * scale));
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
            _spriteBatch.Draw(_backgroundTexture, backgroundRectangle, Color.White);
            _player.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);
            _bullet.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}