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
    public class BlueEnemy
    {

        public int screenWidth;
        public int screenHeight;

        public int enemyWidth;
        public int enemyHeight;

        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public Rectangle blueEnemyRectangle;

        public bool isVisible;
        Random random = new Random();
        public float randX;
        public float randY;

        public int health;//health of enemy
       
        //bullet variables
        public Texture2D bulletTexture;
        public float bulletDelay;
        public List<Bullet> bulletList;   
    

        public BlueEnemy(Texture2D newtexture, Texture2D newBulletTexture, Vector2 newPosition)
        {
            screenHeight = 900;
            screenWidth = 900;
            enemyHeight = 75;
            enemyWidth = 100;
            texture = newtexture;
            position = newPosition;
            speed = 2;
            isVisible = true;
            randX = random.Next(0 + enemyWidth, screenWidth - enemyWidth);
            randY = random.Next(-800, -70);

            bulletList = new List<Bullet>();
            bulletTexture = newBulletTexture;
            bulletDelay = 80;

            health = 3;
        }


        public void LoadContent(ContentManager Content)
        {
           
        }


        public void Update(GameTime gameTime)
        {
            blueEnemyRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            position.Y = position.Y + speed;

            if (position.Y >= screenHeight)
            {
                position.Y = random.Next(-800, -70);
                position.X = random.Next(0, screenWidth - enemyWidth ); 
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
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }

            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + enemyWidth / 2 - bulletTexture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;
                bulletDelay = 80;
 
                if(bulletList.Count < 5)
                {
                    bulletList.Add(newBullet);
                }

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
