using Microsoft.Xna.Framework;

namespace Monogame_Herkansing
{
    internal class CollisionHandler
    {
        internal Rectangle _enemyHitbox;

        private Rectangle _player;
        private Rectangle _enemy;
        private Rectangle _bullet;

        public void update(GameTime gametime)
        {
            if(_bullet.Intersects(_player))
            {
                
            }
        }

    }

}
