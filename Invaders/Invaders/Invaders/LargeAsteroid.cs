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
    public class LargeAsteroid
    {
        public int screenWidth = 1440;
        public int screenHeight = 900;

        public int asteroidHeight;
        public int asteroidWidth;

        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public float rotationAngle;
        public Vector2 origin;
        public Rectangle lagreAsteroidRectangle;

        public bool isVisible;
        Random random = new Random();
        public float randX;
        public float randY;

        public int health;

        public LargeAsteroid(Texture2D newTexture, Vector2 newPosition )
        {
            //asteroidHeight = 65;
            //asteroidWidth = 65;
            texture = newTexture;
            position = newPosition;
            speed = 2;
            isVisible = true;
            randX = random.Next(0 + asteroidWidth, screenWidth - asteroidWidth);
            randY = random.Next(-800, -70);
            health = 3;
        }


        public void LoadContent(ContentManager Content)
        {
        }


        public void Update(GameTime gameTime)
        {
            //sets asteroid rectangle
            lagreAsteroidRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width , texture.Height );

            //set origin of asteroid
            //origin.X = texture.Width / 2; // for rotation
            //origin.Y = texture.Height / 2; // for rotation

            //make asteroid move down screen
            position.Y = position.Y + speed;

            //if asteroid goes of screen it resets at new random location
            if (position.Y >= screenHeight)
            {
                position.Y = random.Next(-800, -70); //random y co-ordinate between -800 and -70
                position.X = random.Next(10, 1350); //random x co-ordinate between 10 and 1350
            }

            //rotation
            /*float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
