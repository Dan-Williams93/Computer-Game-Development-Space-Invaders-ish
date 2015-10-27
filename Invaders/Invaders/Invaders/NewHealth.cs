using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Invaders
{
    public class NewHealth
    {
        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public Rectangle healthRec;
        public Random random;
        public float timer;
        public bool isVisible;


        public NewHealth()
        {
            texture = null;
            position = new Vector2 (720,0);
            speed = 5;
            timer = 0;
            isVisible = false;
            random = new Random();
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("health1");
        }

        public void Update(GameTime gameTime)
        {
            healthRec = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= 15)
            {
                position.Y = position.Y + speed;
                isVisible = true;
            }

            if (position.Y >= 900)
            {
                position.X = random.Next(0, 1400);
                position.Y = 0;
                timer = 0;
                isVisible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
