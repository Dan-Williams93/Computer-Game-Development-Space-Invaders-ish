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
    public class SoundManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
     
        public Cue gameSong;
        public Cue menuSong;
        public Cue deathSong;
        
        
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue gameMusic;

        public SoundManager(Game g) : base(g)
        {
            gameMusic = null;
            
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public override void Initialize()
        {
            
            audioEngine = new AudioEngine("Content\\InvadersAudio.xgs"); 
            waveBank = new WaveBank(audioEngine, "Content\\Wave Bank.xwb"); 
            soundBank = new SoundBank(audioEngine, "Content\\Sound Bank.xsb");

            
            if (gameSong == null) 
            {
                gameSong = soundBank.GetCue("in game");
                 
            }

            if (menuSong == null)
            {
                menuSong = soundBank.GetCue("Menu");
                
            }

            if (deathSong == null)
            {
                deathSong = soundBank.GetCue("Death");
            }
            
        }


        public void playPauseMusic()
        {
            // When called pauses or resumes the game music 
            if (gameMusic.IsPaused)
            {
                gameMusic.Resume();
            }
            else
            {
                gameMusic.Pause();
            }

           
        }

        public override void  Update(GameTime gameTime)
        {
            audioEngine.Update(); // Call the audio engine update
        }





        /*public void changeCurrentSong(int song)
        {

            switch (song)
            {
                case 1: currentSong = menuSong;
                    currentSong.Play();
                    break;

                case 2: 
                    currentSong = gameSong;
                    
                    currentSong.Play();
                    break;

                case 3: currentSong = deathSong;
                    currentSong.Play();
                    break;
            }
        }*/

    }
}
