using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Monogame_Herkansing
{
    internal class CollisionHandler
    {
        public bool CollisionCheck(Bullet bullet, Player player, Enemy enemy)
        {
            if (enemy.enemyHitbox.Intersects(bullet.bulletHitbox))
            {
                enemy.SpawnEnemy();
                if (bullet.i >= 0 && bullet.i < bullet.playerBullets.Count)
                {
                    bullet.playerBullets.RemoveAt(bullet.i);
                }
                return true;
            }
            return false;
        }

        public void RemoveAt(int i, List<Bullet> toRemove)
        {
            if (i >= 0 && i < toRemove.Count)
            {
                toRemove.RemoveAt(i);
            }
        }

        public void PlayerCollisionCheck(Player player, Enemy enemy)
        {
            if (enemy.enemyHitbox.Intersects(player.playerHitbox))
            {
                Application.Exit();
            }
        }
    }
}
