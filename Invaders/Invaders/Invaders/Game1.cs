using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Invaders
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum State
        {
            intro,
            menu,
            help,
            playing,
            gameover
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        State gameState = State.intro;

        public Texture2D introImage;
        public Texture2D wolfImage;
        public Texture2D menuImage;
        public Texture2D helpImage;
        public Texture2D gameOverImage;
        public float timer;

        Random random = new Random();

        Background bg = new Background();
        border B = new border();
        Player p = new Player();
        PlayerScore ps = new PlayerScore();
        NewHealth nh = new NewHealth();

        List<LargeAsteroid> largeAsteroidList = new List<LargeAsteroid>();
        List<MediumAsteroid> mediumAsteroidList = new List<MediumAsteroid>();
        List<SmallAsteroid> smallAsteroidList = new List<SmallAsteroid>();
        List<GreenEnemy> greenEnemyList = new List<GreenEnemy>();
        List<RedEnemy> redEnemyList = new List<RedEnemy>();
        List<OrangeEnemy> orangeEnemyList = new List<OrangeEnemy>();
        List<BlueEnemy> blueEnemyList = new List<BlueEnemy>();
        List<PinkEnemy> pinkEnemyList = new List<PinkEnemy>();
        List<Explosion> explosionList = new List<Explosion>();

        //sound
        //SoundManager sm = new SoundManager();

        GameSound gs;
        public static SoundManager sm;

        //games run 
        public int gamesRun;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 500;//900; //600
            graphics.PreferredBackBufferWidth = 500;//1440; //1000
            
            introImage = null;
            wolfImage = null;
            menuImage = null;
            helpImage = null;
            gameOverImage = null;
            timer = 0;

            sm = new SoundManager(this); // Create a new audio object from our new class 
            Components.Add(sm);

            gs = new GameSound();

            gamesRun = 0;
        }

        
        protected override void Initialize()
        {
            
            base.Initialize();
        }

       
        protected override void LoadContent()
        {            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg.LoadContent(Content);
            B.LoadContent(Content);
            p.LoadContent(Content);
            ps.LoadContent(Content);
            nh.LoadContent(Content);

            introImage = Content.Load<Texture2D>("intro");
            wolfImage = Content.Load<Texture2D>("large wolf");
            menuImage = Content.Load<Texture2D>("Main Menu");
            gameOverImage = Content.Load<Texture2D>("Game Over");
            helpImage = Content.Load<Texture2D>("Help Menu");

            gs.LoadContent(Content);
            gs.wolfSound.Play();
        }

      
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here <shame>
        }

        
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            switch (gameState)
            {
                case State.playing:
                    {
                        bg.speed = 5;
                        nh.Update(gameTime);                       

                         // Allows the game to exit
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                        {
                            this.Exit();
                        }

                        //collision with large asteroid
                        foreach (LargeAsteroid la in largeAsteroidList)
                        { 
                            if (la.lagreAsteroidRectangle.Intersects(p.playerRectangle))
                            {
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(la.position.X + la.texture.Width / 2, la.position.Y)));
                                gs.explosionSound.Play();//ecplosion sound

                                if (B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    la.isVisible = false;
                                }
                                else
                                {
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        la.isVisible = false;
                                    }
                                }
                            }

                            if (la.lagreAsteroidRectangle.Intersects(p.playerRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                la.isVisible = false;
                            }

                            //player bullet and asteroid
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                    
                                if (p.bulletList[i].bulletRectangle.Intersects(la.lagreAsteroidRectangle))
                                {
                                    la.health = la.health - 1;
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible                       
                                }
                            }

                            if (la.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(la.position.X + la.texture.Width/2, la.position.Y)));
                                ps.playerScore = ps.playerScore + 30;
                                la.isVisible = false;
                            }

                            la.Update(gameTime);
                        }

                        //collision with medium asteroid
                        foreach (MediumAsteroid ma in mediumAsteroidList)
                        {
                            if (p.playerRectangle.Intersects(ma.mediumAsteroidRectangle))
                            {
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(ma.position.X + ma.texture.Width / 2, ma.position.Y)));
                                gs.explosionSound.Play();//ecplosion sound

                                if (B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    ma.isVisible = false;
                                }
                                else
                                {
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        ma.isVisible = false;
                                    }
                                }
                            }

                            if (p.playerRectangle.Intersects(ma.mediumAsteroidRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                ma.isVisible = false;
                            }

                            //player bullet and asteroid
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].bulletRectangle.Intersects(ma.mediumAsteroidRectangle))
                                {
                                    ma.health = ma.health - 1;
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible                       
                                }
                            }

                            if (ma.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(ma.position.X + ma.texture.Width / 2, ma.position.Y)));
                                ps.playerScore = ps.playerScore + 20;
                                ma.isVisible = false;
                            }

                            ma.Update(gameTime);
                        }

                        //collision with small asteroid
                        foreach (SmallAsteroid sa in smallAsteroidList)
                        {
                            if (p.playerRectangle.Intersects(sa.smallAsteroidRectangle))
                            {
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(sa.position.X + sa.texture.Width / 2, sa.position.Y)));
                                gs.explosionSound.Play();//ecplosion sound

                                if (B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    sa.isVisible = false;
                                }
                                else
                                {
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        sa.isVisible = false;
                                    }
                                }
                            }

                            if (p.playerRectangle.Intersects(sa.smallAsteroidRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                sa.isVisible = false;
                            }

                            //player bullet and asteroid
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].bulletRectangle.Intersects(sa.smallAsteroidRectangle))
                                {
                                    sa.health = sa.health - 1;
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible                       
                                }
                            }

                            if (sa.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(sa.position.X + sa.texture.Width / 2, sa.position.Y)));
                                ps.playerScore = ps.playerScore + 10;
                                sa.isVisible = false;
                            }

                            sa.Update(gameTime);
                        }

                        //collision with green enemy
                        foreach (GreenEnemy ge in greenEnemyList)
                        {                
                            if (p.playerRectangle.Intersects(ge.greenEnemyRectangle))
                            {
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(ge.position.X + ge.texture.Width / 2, ge.position.Y)));
                                gs.explosionSound.Play();//ecplosion sound

                                //take first third
                                if(B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    ge.isVisible = false;
                                }
                                else
                                {
                                    //take second life
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        ge.isVisible = false;
                                    }
                                }
                            }

                            //take first life
                            if(p.playerRectangle.Intersects(ge.greenEnemyRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                ge.isVisible = false;
                            }

 
                            //collision between bullet and enemy
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].bulletRectangle.Intersects(ge.greenEnemyRectangle))
                                {
                                    ge.health = ge.health - 1;// minus one from health
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible                       
                                }
                            }

                            if (ge.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(ge.position.X, ge.position.Y)));
                                ps.playerScore = ps.playerScore + 50;
                                ge.isVisible = false;
                            }


                            //collision between enemy bullet and player
                            for (int i = 0; i < ge.bulletList.Count; i++)
                            {
                    
                                if (p.playerRectangle.Intersects(ge.bulletList[i].bulletRectangle))
                                {
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                    gs.explosionSound.Play();//ecplosion sound

                                    //take first third
                                    if (B.healthIsVisibleTwo == false)
                                    {
                                        B.healthIsVisibleOne = false;
                                        ge.bulletList[i].isVisible = false;
                                    }
                                    else
                                    {
                                        //take second life
                                        if (B.healthIsVisibleThree == false)
                                        {
                                            B.healthIsVisibleTwo = false;
                                            ge.bulletList[i].isVisible = false;
                                        }
                                    }
                                }


                                //take first life
                                if (p.playerRectangle.Intersects(ge.bulletList[i].bulletRectangle))
                                {
                                    gs.explosionSound.Play();//ecplosion sound
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                    B.healthIsVisibleThree = false;
                                    ge.bulletList[i].isVisible = false;
                                }
                            }                

                            ge.Update(gameTime);             
                        }

                        //collision with red enemy
                        foreach (RedEnemy re in redEnemyList)
                        {
                            if (p.playerRectangle.Intersects(re.redEnemyRectangle))
                            {
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(re.position.X + re.texture.Width / 2, re.position.Y)));
                                gs.explosionSound.Play();//ecplosion sound

                                if (B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    re.isVisible = false;
                                }
                                else
                                {
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        re.isVisible = false;
                                    }
                                }
                            }

                            if (p.playerRectangle.Intersects(re.redEnemyRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                re.isVisible = false;
                            }


                            //collision between bullet and enemy
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].bulletRectangle.Intersects(re.redEnemyRectangle))
                                {
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible  
                                    re.health = re.health -1;
                                }
                            }

                            //if health is 0 the enemy is killed
                            if (re.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(re.position.X + re.texture.Width / 2, re.position.Y)));
                                ps.playerScore = ps.playerScore + 20;
                                re.isVisible = false;
                            }

                            //collision between enemy bullet and player
                            for (int i = 0; i < re.enemyBulletList.Count; i++)
                            {
                                if (p.playerRectangle.Intersects(re.enemyBulletList[i].bulletRectangle))
                                {
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                    gs.explosionSound.Play();//ecplosion sound
                                    
                                    //take first third
                                    if (B.healthIsVisibleTwo == false)
                                    {
                                        B.healthIsVisibleOne = false;
                                        re.enemyBulletList[i].isVisible = false;
                                    }
                                    else
                                    {
                                        //take second life
                                        if (B.healthIsVisibleThree == false)
                                        {
                                            B.healthIsVisibleTwo = false;
                                            re.enemyBulletList[i].isVisible = false;
                                        }
                                    }
                                }
                    
                                //take first life
                                if (p.playerRectangle.Intersects(re.enemyBulletList[i].bulletRectangle))
                                {
                                    gs.explosionSound.Play();//ecplosion sound
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                    B.healthIsVisibleThree = false;
                                    re.enemyBulletList[i].isVisible = false;
                                }
                            }

                            re.Update(gameTime);
                        }

                        //collision with orange enemy
                        foreach (OrangeEnemy oe in orangeEnemyList)
                        {
                            if (p.playerRectangle.Intersects(oe.orangeEnemyRectangle))
                            {
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(oe.position.X + oe.texture.Width / 2, oe.position.Y)));
                                gs.explosionSound.Play();//ecplosion sound

                                if (B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    oe.isVisible = false;
                                }
                                else
                                {
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        oe.isVisible = false;
                                    }
                                }
                            }

                            if (p.playerRectangle.Intersects(oe.orangeEnemyRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                oe.isVisible = false;
                            }


                            //collision between bullet and enemy
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].bulletRectangle.Intersects(oe.orangeEnemyRectangle))
                                {
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible  
                                    oe.health = oe.health - 1;
                                }
                            }

                            //if health is 0 the enemy is killed
                            if (oe.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(oe.position.X + oe.texture.Width / 2, oe.position.Y)));
                                ps.playerScore = ps.playerScore + 50;
                                oe.isVisible = false;
                            }

                            //collision between enemy bullet and player
                            for (int i = 0; i < oe.bulletList.Count; i++)
                            {
                                if (p.playerRectangle.Intersects(oe.bulletList[i].bulletRectangle))
                                {
                                    gs.explosionSound.Play();//ecplosion sound
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                    //take first third
                                    if (B.healthIsVisibleTwo == false)
                                    {
                                        B.healthIsVisibleOne = false;
                                        oe.bulletList[i].isVisible = false;
                                    }
                                    else
                                    {
                                        //take second life
                                        if (B.healthIsVisibleThree == false)
                                        {
                                            B.healthIsVisibleTwo = false;
                                            oe.bulletList[i].isVisible = false;
                                        }
                                    }
                                }


                                //take first life
                                if (p.playerRectangle.Intersects(oe.bulletList[i].bulletRectangle))
                                {
                                    B.healthIsVisibleThree = false;
                                    oe.bulletList[i].isVisible = false;
                                }
                            }

                            oe.Update(gameTime);
                        }

                        //collision with blue enemy
                        foreach (BlueEnemy be in blueEnemyList)
                        {
                            if (p.playerRectangle.Intersects(be.blueEnemyRectangle))
                            {
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(be.position.X + be.texture.Width / 2, be.position.Y)));
                                gs.explosionSound.Play();//ecplosion sound

                                if (B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    be.isVisible = false;
                                }
                                else
                                {
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        be.isVisible = false;
                                    }
                                }
                            }

                            if (p.playerRectangle.Intersects(be.blueEnemyRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                be.isVisible = false;
                            }


                            //collision between bullet and enemy
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].bulletRectangle.Intersects(be.blueEnemyRectangle))
                                {
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible  
                                    be.health = be.health - 1;
                                }
                            }

                            //if health is 0 the enemy is killed
                            if (be.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(be.position.X + be.texture.Width / 2, be.position.Y)));
                                ps.playerScore = ps.playerScore + 100;
                                be.isVisible = false;
                            }


                            //collision between enemy bullet and player
                            for (int i = 0; i < be.bulletList.Count; i++)
                            {
                                if (p.playerRectangle.Intersects(be.bulletList[i].bulletRectangle))
                                {
                                    gs.explosionSound.Play();//ecplosion sound
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                    //take first third
                                    if (B.healthIsVisibleTwo == false)
                                    {
                                        B.healthIsVisibleOne = false;
                                        be.bulletList[i].isVisible = false;
                                    }
                                    else
                                    {
                                        //take second life
                                        if (B.healthIsVisibleThree == false)
                                        {
                                            B.healthIsVisibleTwo = false;
                                            be.bulletList[i].isVisible = false;
                                        }
                                    }
                                }


                                //take first life
                                if (p.playerRectangle.Intersects(be.bulletList[i].bulletRectangle))
                                {
                                    B.healthIsVisibleThree = false;
                                    be.bulletList[i].isVisible = false;
                                }
                            }


                            be.Update(gameTime);
                        }

                        //collision with pink enemy
                        foreach (PinkEnemy pe in pinkEnemyList)
                        {
                            if (p.playerRectangle.Intersects(pe.pinkEnemyRectangle))
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(pe.position.X + pe.texture.Width / 2, pe.position.Y)));

                                if (B.healthIsVisibleTwo == false)
                                {
                                    B.healthIsVisibleOne = false;
                                    pe.isVisible = false;
                                }
                                else
                                {
                                    if (B.healthIsVisibleThree == false)
                                    {
                                        B.healthIsVisibleTwo = false;
                                        pe.isVisible = false;
                                    }
                                }
                            }

                            if (p.playerRectangle.Intersects(pe.pinkEnemyRectangle))
                            {
                                B.healthIsVisibleThree = false;
                                pe.isVisible = false;
                            }


                            //collision between bullet and enemy
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].bulletRectangle.Intersects(pe.pinkEnemyRectangle))
                                {
                                    p.bulletList[i].isVisible = false; //makes player bullet not visible  
                                    pe.health = pe.health - 1;
                                }
                            }

                            //if health is 0 the enemy is killed
                            if (pe.health == 0)
                            {
                                gs.explosionSound.Play();//ecplosion sound
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(pe.position.X + pe.texture.Width / 2, pe.position.Y)));
                                ps.playerScore = ps.playerScore + 20;
                                pe.isVisible = false;
                            }


                            //collision between enemy bullet and player
                            for (int i = 0; i < pe.EnemyBulletList.Count; i++)
                            {
                                if (p.playerRectangle.Intersects(pe.EnemyBulletList[i].bulletRectangle))
                                {
                                    gs.explosionSound.Play();//ecplosion sound
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(p.position.X + p.texture.Width / 2, p.position.Y)));
                                    //take first third
                                    if (B.healthIsVisibleTwo == false)
                                    {
                                        B.healthIsVisibleOne = false;
                                        pe.EnemyBulletList[i].isVisible = false;
                                    }
                                    else
                                    {
                                        //take second life
                                        if (B.healthIsVisibleThree == false)
                                        {
                                            B.healthIsVisibleTwo = false;
                                            pe.EnemyBulletList[i].isVisible = false;
                                        }
                                    }
                                }


                                //take first life
                                if (p.playerRectangle.Intersects(pe.EnemyBulletList[i].bulletRectangle))
                                {
                                    B.healthIsVisibleThree = false;
                                    pe.EnemyBulletList[i].isVisible = false;
                                }
                            }
                            pe.Update(gameTime);
                        }

                        foreach (Explosion ex in explosionList)
                        {
                            ex.Update(gameTime);
                        }

                        if (B.healthIsVisibleOne == false && gamesRun <= 0)
                        {
                            gameState = State.gameover;
                            sm.gameSong.Pause();
                            sm.deathSong.Play();
                        }
                        else
                        {
                            if (B.healthIsVisibleOne == false && gamesRun >= 1)
                            {
                                gameState = State.gameover;
                                sm.gameSong.Pause();
                                sm.deathSong.Resume();
                            }
                        }

                        if(p.playerRectangle.Intersects(nh.healthRec))
                        {
                            if(B.healthIsVisibleTwo == false)
                            {
                                B.healthIsVisibleTwo = true;
                                nh.isVisible = false;
                                nh.position.X = random.Next(0, 350);
                                nh.position.Y = 0;
                                nh.timer = 0;  
                            }
                            else
                            {
                                B.healthIsVisibleThree = true;
                                nh.isVisible = false;
                                nh.position.X = random.Next(0, 350);
                                nh.position.Y = 0;
                                nh.timer = 0; 
                            }
                                
                        }
                    
  

                        bg.Update(gameTime);
                        p.Update(gameTime);
                        ps.Update(gameTime);
                        ManageExplosions();
                        LoadLargeAsteroids();
                        LoadMediumAsteroid();
                        LoadSmallAsteroid();
                        LoadGreenEnemy();
                        LoadRedEnemy();
                        LoadOrangeEnemy();
                        LoadBlueEnemy();
                        LoadPinkEnemy();
                        break;
                    }

                    //updating intro
                case State.intro:
                    {
                        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (timer >= 3)
                        {
                            sm.menuSong.Play();
                            gameState = State.menu;
                            timer = 0;
                        }
                        
                        break;
                    }

                                   //updating menu
                case State.menu:
                    {
                        bg.Update(gameTime);
                        bg.speed = 1;
                        

                        KeyboardState keyState = Keyboard.GetState();
                        GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
                        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);

                        if (keyState.IsKeyDown(Keys.A) && gamesRun <= 0 || gamePad.IsButtonDown(Buttons.A) && gamesRun <= 0)
                        {
                            gameState = State.playing;
                            sm.gameSong.Play();
                            sm.menuSong.Pause();
                        }
                        else
                        {
                            if (keyState.IsKeyDown(Keys.A) && gamesRun >= 1 || gamePad.IsButtonDown(Buttons.A) && gamesRun >= 1 && gamesRun >= 1)
                            {
                                gameState = State.playing;
                                sm.gameSong.Resume();
                                sm.menuSong.Pause();
                            }
                        }

                        if (keyState.IsKeyDown(Keys.X) || gamePad.IsButtonDown(Buttons.X))
                        {
                            gameState = State.help;
                        }

                        if (keyState.IsKeyDown(Keys.B) || gamePad.IsButtonDown(Buttons.B))
                        {
                            this.Exit();
                        }
                        
                        break;
                    }

                    //updating help
                case State.help:
                    {
                        KeyboardState keyState = Keyboard.GetState();
                        GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
                        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);

                        if (keyState.IsKeyDown(Keys.Y) || gamePad.IsButtonDown(Buttons.Y))
                        {
                            gameState = State.menu;
                        }

                        bg.Update(gameTime);
                        bg.speed = 1;

                        break;
                    }

                    //updating game over
                case State.gameover:
                    {
                        bg.Update(gameTime);
                        bg.speed = 1;
                        

                        KeyboardState keyState = Keyboard.GetState();
                        GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
                        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);

                        if (keyState.IsKeyDown(Keys.Y) || gamePad.IsButtonDown(Buttons.Y))
                        {
                            gameState = State.menu;
                            sm.menuSong.Resume(); 
                            sm.deathSong.Pause();
                            B.healthIsVisibleOne = true;
                            B.healthIsVisibleTwo = true;
                            B.healthIsVisibleThree = true;
                            nh.isVisible = false;
                            ps.playerScore = 0;
                            gamesRun = 1;

                            foreach (LargeAsteroid la in largeAsteroidList)
                            {
                                la.isVisible = false;
                            }

                            foreach (MediumAsteroid ma in mediumAsteroidList)
                            {
                                ma.isVisible = false;
                            }

                            foreach (SmallAsteroid sa in smallAsteroidList)
                            {
                                sa.isVisible = false;
                            }

                            foreach (BlueEnemy be in blueEnemyList)
                            {
                                be.isVisible = false;
                            }

                            foreach (GreenEnemy ge in greenEnemyList)
                            {
                                ge.isVisible = false;
                            }

                            foreach (OrangeEnemy oe in orangeEnemyList)
                            {
                                oe.isVisible = false;
                            }

                            foreach (PinkEnemy pe in pinkEnemyList)
                            {
                                pe.isVisible = false;
                            }

                            foreach (RedEnemy re in redEnemyList)
                            {
                                re.isVisible = false;
                            }

                            foreach (Bullet b in p.bulletList)
                            {
                                b.isVisible = false;
                            }

                            foreach (Explosion ex in explosionList)
                            {
                                ex.isVisible = false;
                            }

                            p.position = new Vector2(p.screenWidth / 2 - p.shipWidth / 2, p.screenHeight - 150);
                        }

                        if (keyState.IsKeyDown(Keys.A) || gamePad.IsButtonDown(Buttons.A))
                        {
                            gameState = State.playing;
                            sm.gameSong.Resume();
                            sm.deathSong.Pause();
                            //setting all objects back to normal start positions
                            B.healthIsVisibleOne = true;
                            B.healthIsVisibleTwo = true;
                            B.healthIsVisibleThree = true;
                            ps.playerScore = 0;
                            gamesRun = 1;

                            foreach (LargeAsteroid la in largeAsteroidList)
                            {
                                la.isVisible = false;
                            }

                            foreach (MediumAsteroid ma in mediumAsteroidList)
                            {
                                ma.isVisible = false;
                            }

                            foreach (SmallAsteroid sa in smallAsteroidList)
                            {
                                sa.isVisible = false;
                            }

                            foreach (BlueEnemy be in blueEnemyList)
                            {
                                be.isVisible = false;
                            }

                            foreach (GreenEnemy ge in greenEnemyList)
                            {
                                ge.isVisible = false;
                            }

                            foreach (OrangeEnemy oe in orangeEnemyList)
                            {
                                oe.isVisible = false;
                            }

                            foreach (PinkEnemy pe in pinkEnemyList)
                            {
                                pe.isVisible = false;
                            }

                            foreach (RedEnemy re in redEnemyList)
                            {
                                re.isVisible = false;
                            }

                            foreach (Bullet b in p.bulletList)
                            {
                                b.isVisible = false;
                            }

                            foreach (Explosion ex in explosionList)
                            {
                                ex.isVisible = false;
                            }

                            p.position = new Vector2(p.screenWidth / 2 - p.shipWidth / 2, p.screenHeight - 150);
                                
                        }
                        break;
                    }

            }

            base.Update(gameTime);
        }
       
        protected override void Draw(GameTime gameTime)

        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
            {

                case State.playing:
                    {
                        bg.Draw(spriteBatch);
                        nh.Draw(spriteBatch);

                        foreach (Explosion ex in explosionList)
                        {
                            ex.Draw(spriteBatch);
                        }


                        foreach (LargeAsteroid la in largeAsteroidList)
                        {
                            la.Draw(spriteBatch);
                        }

                        foreach (MediumAsteroid ma in mediumAsteroidList)
                        {
                            ma.Draw(spriteBatch);
                        }

                        foreach (SmallAsteroid sa in smallAsteroidList)
                        {
                            sa.Draw(spriteBatch);
                        }

                        foreach (GreenEnemy ge in greenEnemyList)
                        {
                            ge.Draw(spriteBatch);
                        }

                        foreach (RedEnemy re in redEnemyList)
                        {
                            re.Draw(spriteBatch);
                        }

                        foreach (OrangeEnemy oe in orangeEnemyList)
                        {
                            oe.Draw(spriteBatch);
                        }

                        foreach (BlueEnemy be in blueEnemyList)
                        {
                            be.Draw(spriteBatch);
                        }

                        foreach (PinkEnemy pe in pinkEnemyList)
                        {
                            pe.Draw(spriteBatch);
                        }

                        p.Draw(spriteBatch);
                        B.Draw(spriteBatch);                        
                        ps.Draw(spriteBatch);

                        break;
                    }

                    case State.intro:
                        {
                            spriteBatch.Draw(introImage, new Vector2(0,0), Color.White);
                            break;
                        }

                    case State.menu:
                        {
                            bg.Draw(spriteBatch);
                            
                            spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);                            
                            break;
                        }

                    case State.help:
                        {
                            bg.Draw(spriteBatch);
                            spriteBatch.Draw(helpImage, new Vector2(0, 0), Color.White);
                            break;
                        }

                    case State.gameover:
                        {
                            bg.Draw(spriteBatch);
                            spriteBatch.Draw(gameOverImage, new Vector2(0, 0), Color.White);
                            spriteBatch.DrawString(ps.playerScoreFont, " " + ps.playerScore.ToString(), new Vector2(720, 400), Color.Red);
                            break;
                        }

                    
            }

            


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadLargeAsteroids()
        {
            int randY = random.Next(-800, -70);
            int randX = random.Next(10, 1350);

            if (largeAsteroidList.Count() < 2)
            {
                largeAsteroidList.Add(new LargeAsteroid(Content.Load<Texture2D>("LargeAsteroid"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < largeAsteroidList.Count; i++)
            {
                if (!largeAsteroidList[i].isVisible)
                {
                    largeAsteroidList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadMediumAsteroid()
        {
            int randY = random.Next(-800, -70);
            int randX = random.Next(10, 1350);

            if (mediumAsteroidList.Count < 2)
            {
                mediumAsteroidList.Add(new MediumAsteroid(Content.Load<Texture2D>("MediumAsteroid"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < mediumAsteroidList.Count; i++)
            {
                if(!mediumAsteroidList[i].isVisible)
                {
                    mediumAsteroidList.RemoveAt(i);
                    i--;
                }
            }                
        }

        public void LoadSmallAsteroid()
        {
            int randX = random.Next(-800, -70);
            int randY = random.Next(10, 1350);

            if (smallAsteroidList.Count < 2)
            {
                smallAsteroidList.Add(new SmallAsteroid(Content.Load<Texture2D>("SmallAsteroid"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < smallAsteroidList.Count; i++)
            {
                if (!smallAsteroidList[i].isVisible)
                {
                    smallAsteroidList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadGreenEnemy()
        {
            int randX = random.Next(-800, -70);
            int randY = random.Next(10, 1350);

            if (greenEnemyList.Count < 1)
            {
                greenEnemyList.Add(new GreenEnemy(Content.Load<Texture2D>("Green"),Content.Load<Texture2D>("Enemybullet"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < greenEnemyList.Count; i++)
            {
                if (!greenEnemyList[i].isVisible)
                {
                    greenEnemyList.RemoveAt(i);
                    i--;
                }
            }



        }

        public void LoadRedEnemy()
        {
            int randX = random.Next(-800, -70);
            int randY = random.Next(10, 1350);

            if(redEnemyList.Count < 1)
            {
                redEnemyList.Add(new RedEnemy(Content.Load<Texture2D>("Red"), Content.Load<Texture2D>("Enemybullet"), new Vector2(randX, randY)));
            }


            for (int i = 0; i < redEnemyList.Count; i++)
            {
                if(!redEnemyList[i].isVisible)
                {
                    redEnemyList.RemoveAt(i);
                    i--;
                }
            }



        }

        public void LoadOrangeEnemy()
        {
            int randX = random.Next(-800, -70);
            int randY = random.Next(10, 1350);

            if (orangeEnemyList.Count < 1)
            {
                orangeEnemyList.Add(new OrangeEnemy(Content.Load<Texture2D>("Orange"), Content.Load<Texture2D>("Enemybullet"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < orangeEnemyList.Count; i++)
            {
                if(!orangeEnemyList[i].isVisible)
                {
                    orangeEnemyList.RemoveAt(i);
                    i--;
                }
            }

        }

        public void LoadBlueEnemy()
        {
            int randX = random.Next(-800, -70);
            int randY = random.Next(10, 1350);

            if (blueEnemyList.Count < 1)
            {
                blueEnemyList.Add(new BlueEnemy(Content.Load<Texture2D>("Blue"), Content.Load<Texture2D>("Enemybullet"),new Vector2(randX, randY)));
            }

            for (int i = 0; i < blueEnemyList.Count; i++)
            {
                if (!blueEnemyList[i].isVisible)
                {
                    blueEnemyList.RemoveAt(i);
                }
            }
        }

        public void LoadPinkEnemy()
        {
            int randX = random.Next(-800, -70);
            int randY = random.Next(10, 1350);

            if (pinkEnemyList.Count < 1)
            {
                pinkEnemyList.Add(new PinkEnemy(Content.Load<Texture2D>("Pink"), Content.Load<Texture2D>("Enemybullet"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < pinkEnemyList.Count; i++)
            {
                if (!pinkEnemyList[i].isVisible)
                {
                    pinkEnemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ManageExplosions()
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
