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
    public class Background
    {
        public Texture2D texture;
        public Vector2 bgPositionOne;
        public Vector2 bgPositionTwo;
        public int speed;



        public Background()
        {
            texture = null;
            bgPositionOne = new Vector2(0, 0);
            bgPositionTwo = new Vector2(0, -900);
            speed = 5;
        }


        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Background");
            
        }


        public void Update(GameTime gameTime)
        {
            bgPositionOne.Y = bgPositionOne.Y + speed;
            bgPositionTwo.Y = bgPositionTwo.Y + speed;

            if (bgPositionOne.Y >= 900)
            {
                bgPositionOne.Y = 0;
                bgPositionTwo.Y = -900;               
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPositionOne, Color.White);
            spriteBatch.Draw(texture, bgPositionTwo, Color.White);
        }
    }
}
