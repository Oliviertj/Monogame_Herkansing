using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace Monogame_Herkansing
{
    public class Game1 : Game
    {
        public int windowHeight;
        public int windowWidth;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle backgroundRectangle;
        private SpriteFont _font;

        private Texture2D _playerTexture;
        private Texture2D _bulletTexture;
        private Texture2D _enemyTexture;
        private Texture2D _backgroundTexture;

        private Player _player;
        private Enemy _enemy;
        private Bullet _bullet;
        CollisionHandler CollisionHandler;

        private float _enemySpeed = 8f;
        private float _playerSpeed = 7.5f;
        private float _bulletSpeed = 1000f;

        private bool win = false;
        private bool isPaused = true;

        private Vector2 _BulletsShotPos = new Vector2(75, 100);
        private Vector2 _EnemiesShotPos = new Vector2(75, 125);

        // Declare a string field to store the content of the text
        private string _displayBulletsShot;
        private string _displayEnemiesShot;
        private string _displayIntroductionText;
        private string _displayWinText;
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
            _player = new Player(_playerTexture, playerPosition, _playerSpeed);

            Vector2 enemyPosition = new Vector2(windowWidth, windowHeight / 2);
            _enemy = new Enemy(_enemyTexture, enemyPosition, _enemySpeed, windowWidth, windowHeight);

            _bullet = new Bullet(_bulletTexture, playerPosition, _bulletSpeed, windowWidth);
            _bullet.player = _player;
            CollisionHandler = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            backgroundRectangle = new Rectangle(0, 0, windowWidth, windowHeight);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("textfont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (!isPaused)
            {
                _player.Update(gameTime, windowWidth, windowHeight);
                _enemy.Update(gameTime);
                _bullet.Update(gameTime, CollisionHandler, _enemy);
                CollisionHandler.CollisionCheck(_bullet, _player, _enemy);
            }
            else
            { 
                UnPauseCheck();
            }
            DisplayText();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            base.Update(gameTime);
        }

        private void UnPauseCheck()
        {
            DisplayIntroduction();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                isPaused = false;
            }
        }

        private void DisplayIntroduction()
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            _displayIntroductionText = "Welcome, This is a basic UFO shooter game \nThe controls are as following: W A S D / ArrowKeys \nTo shoot use Spacebar and to start playing use Enter \nTo win just Shoot the enemy 10 times";

        }
        private void DisplayWinText()
        {     
            _displayWinText = " You Won, Thank you for playing. this is still a beta version of the game but i hope you enjoyed it!";
        }
        
        public void WinScreen()
        {
            if (_enemy.enemiesHit >= 10)
            {
                win = true;
                DisplayWinText();
            }
        }

        private void DisplayText()
        {
                _displayBulletsShot = "Bullets Shot: " + _bullet.bulletsFired.ToString();
                _displayEnemiesShot = "Enemies Shot: " + _enemy.enemiesHit.ToString();
        }
        private void DrawDisplay(SpriteBatch _spriteBatch)
        {
            if (!isPaused)
            {
                _spriteBatch.Draw(_backgroundTexture, backgroundRectangle, Color.White);
                _spriteBatch.DrawString(_font, _displayBulletsShot, _BulletsShotPos, Color.White);
                _spriteBatch.DrawString(_font, _displayEnemiesShot, _EnemiesShotPos, Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_font, _displayIntroductionText, new Vector2(windowWidth / 3, 100) , Color.White);
            }
            if (win == true)
            {
                _spriteBatch.DrawString(_font, _displayWinText, new Vector2(500, 500), Color.Red);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            _spriteBatch.Begin();
            DrawDisplay(_spriteBatch);
            _player.Draw(_spriteBatch);
            _enemy.Draw(_spriteBatch);
            _bullet.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}