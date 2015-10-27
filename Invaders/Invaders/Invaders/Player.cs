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
    public class Player
    {

        //screen variables
        public int screenWidth = 1440;
        public int screenHeight = 900;

        public Texture2D texture;
        public Vector2 position;
        public Vector2 motion;
        public int speed;
        public int shipWidth;
        public int shipHieght;
                
        //collision variables
        public Rectangle playerRectangle;
        public bool isColliding;

        //bullet variables
        public Texture2D bulletTexture;
        public float bulletDelay;
        public List<Bullet> bulletList;     


        //sound
        GameSound gs = new GameSound();
        public Player()
        {
            texture = null;
            shipHieght = 96;
            shipWidth = 96;
            position = new Vector2(screenWidth / 2 - shipWidth / 2, screenHeight - 150);
            speed = 10;            
            bulletList = new List<Bullet>();
            bulletTexture = null;
            bulletDelay = 1;
        }


        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Spaceship");            
            bulletTexture = Content.Load<Texture2D>("playerbullet");
            gs.LoadContent(Content);
        }


        public void Update(GameTime gameTime)
        {
            //drawing ship rectabgle
            playerRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);


            //control ship
            KeyboardState keyState = Keyboard.GetState();
            GamePadState GamePadState = GamePad.GetState(PlayerIndex.One);

            if(keyState.IsKeyDown(Keys.Up) || GamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                position.Y = position.Y - speed;
                motion.X = -1;
            }

            if (keyState.IsKeyDown(Keys.Down) || GamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                position.Y = position.Y + speed;
                motion.X = -1;
            }

            if (keyState.IsKeyDown(Keys.Left) || GamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                position.X = position.X - speed;
                motion.X = -1;
            }

            if (keyState.IsKeyDown(Keys.Right) || GamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                position.X = position.X + speed;
                motion.X = -1;
            }

            //stop ship going off screen
            if (position.Y >= screenHeight - shipHieght)
            {
                position.Y = screenHeight - shipHieght;
            }

            if (position.Y <= 0)
            {
                position.Y = 0;
            }

            if (position.X >= screenWidth - shipWidth)
            {
                position.X = screenWidth - shipWidth;
            }

            if (position.X <= 0)
            {
                position.X = 0;
            }
            
            //shoot. when shoot controller vibrate
            if (keyState.IsKeyDown(Keys.Space) || GamePadState.IsButtonDown(Buttons.RightTrigger))
            {
                Shoot();
                
                GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }
            UpdatePlayerBullet();

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
            //shoot only if the bullet delay resets
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }


            //if bullet delay has reset, draw new bullet and make it visible
            if (bulletDelay <= 0)
            {
                //sound
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + shipWidth / 2 - newBullet.texture.Width / 2, position.Y + 30); //positions bullet center of the player ship

                newBullet.isVisible = true;
                gs.playerShootSound.Play();
                if (bulletList.Count() < 5)
                {
                    bulletList.Add(newBullet);
                }
            }

            //resetting bullet delay

            if (bulletDelay == 0)
            {
                bulletDelay = 10;//10
            }
        }
        
        public void UpdatePlayerBullet()
        {
            foreach (Bullet b in bulletList)
            {
                b.bulletRectangle = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                b.position.Y = b.position.Y - b.speed;

                if (b.position.Y <= 0)
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
