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
    public class PlayerScore
    {
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePosition;
        public int playerScore;

        public PlayerScore()
        {
            playerScore = 0;
            playerScorePosition = new Vector2(1250,10);
            playerScoreFont = null;
        }


        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("Arial");
        }


        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(playerScoreFont, "Score: " + playerScore, playerScorePosition, Color.Red);
        }
    }
}
