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
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public Rectangle sourceRectangle;
        public float timer;
        public float interval;
        public int currentFrame;
        public int screenHeight;
        public int screenWidth;
        public int spriteHeight;
        public int SpriteWidth;
        public bool isVisible;

        public Explosion(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            timer = 0f;
            interval = 20f;
            currentFrame = 1;
            spriteHeight = 128;
            SpriteWidth = 128;
            isVisible = true;
        }


        public void LoadContent(ContentManager Content)
        {

        }


        public void Update(GameTime gameTime)
        {
            //increase timer by number of miliseconds since update was last called
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //check timer is bigger that interval
            if (timer > interval)
            {
                //show next frame of animation
                currentFrame++;
                //reset timer
                timer = 0;
            }

            //if on last frame make explosion animation not visible and reset to the first frame
            if (currentFrame == 17)
            {
                isVisible = false;
                currentFrame = 0;
            }

            //set rectangle of the current frame of animation
            sourceRectangle = new Rectangle(currentFrame * SpriteWidth, 0, SpriteWidth, spriteHeight);

            //set center of the frame
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            }

        }
    }
}
