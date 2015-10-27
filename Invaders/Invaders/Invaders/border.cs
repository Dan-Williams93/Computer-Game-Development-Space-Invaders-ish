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
    public class border
    {

        public Texture2D barTexture;
        public Vector2 barPosition;

        public Texture2D healthTextureOne;
        public Vector2 healthPositionOne;
        public bool healthIsVisibleOne;

        public Texture2D healthTextureTwo;
        public Vector2 healthPositionTwo;
        public bool healthIsVisibleTwo;

        public Texture2D healthTextureThree;
        public Vector2 healthPositionThree;
        public bool healthIsVisibleThree;


        public border()
        {
            barTexture = null;
            barPosition = new Vector2(0, 0);

            healthPositionOne = new Vector2(100, 10);
            healthPositionTwo = new Vector2(150, 10);
            healthPositionThree = new Vector2(200, 10);

            healthIsVisibleOne = true;
            healthIsVisibleTwo = true;
            healthIsVisibleThree = true;
        }

        public void LoadContent(ContentManager Content)
        {
            barTexture = Content.Load<Texture2D>("topBar");
            healthTextureOne = Content.Load<Texture2D>("health1");
            healthTextureTwo = Content.Load<Texture2D>("health2");
            healthTextureThree = Content.Load<Texture2D>("health3");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barTexture, barPosition, Color.White);
            

            if (healthIsVisibleOne == true)
            {
                spriteBatch.Draw(healthTextureOne, healthPositionOne, Color.White);
            }

            if (healthIsVisibleTwo == true)
            {
                spriteBatch.Draw(healthTextureTwo, healthPositionTwo, Color.White);
            }

            if (healthIsVisibleThree == true)
            {
                spriteBatch.Draw(healthTextureThree, healthPositionThree, Color.White);
            }

        }


    }
}
