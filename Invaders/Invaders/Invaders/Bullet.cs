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
    public class Bullet
    {

        public Texture2D texture;
        public Vector2 position;
        //public Vector2 origin;
        public float speed;
        public bool isVisible;

        public Rectangle bulletRectangle;

        public Bullet(Texture2D newTexture)
        {
            texture = newTexture;
            speed = 10;
            isVisible = false;            
        }


        public void LoadContent(ContentManager Content)
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
