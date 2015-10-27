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
    public class GreenEnemy
    {
        public int screenWidth;
        public int screenHeight;

        public int enemyWidth;
        public int enemyHeight;

        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public Rectangle greenEnemyRectangle;

        public bool isVisible;
        Random random = new Random();
        public float randX;
        public float randY;

        public int health;//health of enemy


        //bullet variables
        public Texture2D bulletTexture;
        public float bulletDelay;
        public List<Bullet> bulletList; 

        public GreenEnemy(Texture2D newTexture, Texture2D newBulletTexture, Vector2 newPosition)
        {
            screenWidth = 1440;
            screenHeight = 900;
            enemyWidth = 75;
            enemyHeight = 90;
            texture = newTexture;
            position = newPosition;
            isVisible = true;
            randX = random.Next(0 + enemyWidth, screenWidth - enemyWidth );
            randY = random.Next(-800, -70);
            speed = 2;

            health = 2;

            bulletTexture = newBulletTexture;
            bulletDelay = 80;
            bulletList = new List<Bullet>();
        }


        public void LoadContent(ContentManager Content)
        {

        }


        public void Update(GameTime gameTime)
        {
            greenEnemyRectangle = new Rectangle((int)position.X, (int)position.Y , texture.Width, texture.Height);

            position.Y = position.Y + speed;

            if (position.Y >= screenHeight)
            {
                position.Y = random.Next(-800, -70); //random y co-ordinate between -800 and -70
                position.X = random.Next(10, screenWidth - enemyWidth); //random x co-ordinate between 10 and 1350
            }

            Shoot();
            UpdateEnemyBullet();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }
            
        }

        public void Shoot()
        {
            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + texture.Width / 2 - bulletTexture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;
                bulletDelay = 80;

                if (bulletList.Count < 5)
                {
                    bulletList.Add(newBullet);
                }
            }

            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }

        }


        public void UpdateEnemyBullet()
        {
            foreach (Bullet b in bulletList)
            {
                b.bulletRectangle = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y + b.speed;

                if (b.position.Y >= screenHeight)
                {
                    b.isVisible = false;
                }
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }                   
        }
    }
}
