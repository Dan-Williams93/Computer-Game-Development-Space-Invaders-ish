using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Invaders
{
    public class PinkEnemy
    {

        public int screenWidth;
        public int screenHeight;

        public int enemyWidth;
        public int enemyHeight;

        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public Rectangle pinkEnemyRectangle;

        public bool isVisible;
        Random random = new Random();
        public float randX;
        public float randY;

        public int health;//health of enemy

        //bullet variables
        public float bulletDelay;
        public Texture2D bulletTexture;
        public List<Bullet> EnemyBulletList;


        public PinkEnemy(Texture2D newTexture, Texture2D newBulletTexture, Vector2 newPosition)
        {
            screenHeight = 900;
            screenWidth = 1440;
            texture = newTexture;
            position = newPosition;
            speed = 2;
            isVisible = true;
            randX = random.Next(0 + enemyWidth, screenWidth - enemyWidth);
            randY = random.Next(-800, -70);

            bulletDelay = 80;
            bulletTexture = newBulletTexture;
            EnemyBulletList = new List<Bullet>();

            health = 1;
        }


        public void LoadContent(ContentManager Content)
        {

        }


        public void Update(GameTime gameTime)
        {
            pinkEnemyRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            position.Y = position.Y + speed;

            if (position.Y >= screenHeight)
            {
                position.Y = random.Next(-800, -70);
                position.X = random.Next(0 , screenWidth - texture.Width); 
            }

            Shoot();
            UpdateEnemyBullet();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            foreach (Bullet b in EnemyBulletList)
            {
                b.Draw(spriteBatch);
            }
        }

        public void Shoot()
        {
            //add bullet to list
            //bullet delay is more than 0  take away from bullet delay
            //what bullet is and its position
            //bullet count is more that 5 add bullet to list

            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + texture.Width / 2 - bulletTexture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;
                bulletDelay = 80;

                if (EnemyBulletList.Count < 5)
                {
                    EnemyBulletList.Add(newBullet);
                }
            }

            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }
        }

        public void UpdateEnemyBullet()
        {
            //bullet rectangle for bullets in the list
            //movement
            //off the screen
            //when not visible remove from list

            foreach (Bullet b in EnemyBulletList)
            {
                b.bulletRectangle = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y + b.speed;

                if(b.position.Y >= screenHeight)
                {
                    b.isVisible = false;
                }
            }

            for (int i = 0; i < EnemyBulletList.Count; i++)
            {
                if(!EnemyBulletList[i].isVisible)
                {
                    EnemyBulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
