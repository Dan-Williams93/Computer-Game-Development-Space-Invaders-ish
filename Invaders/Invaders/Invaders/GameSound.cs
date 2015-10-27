using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Invaders
{
    public class GameSound
    {
        public SoundEffect playerShootSound;
        public SoundEffect explosionSound;
        public SoundEffect wolfSound;


        public GameSound()
        {
            playerShootSound = null;
            explosionSound = null;
            wolfSound = null;
        }

        public void LoadContent(ContentManager Content)
        {
            playerShootSound = Content.Load<SoundEffect>("playerShoot");
            explosionSound = Content.Load<SoundEffect>("explode"); ;
            wolfSound = Content.Load<SoundEffect>("wolf"); ;
        }
    
    


    }
}
