using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace CodenameHorror
{
    public abstract class Rune : Entity
    {
	    public int rechargeTime;
    	public int powerLevel;
        protected bool acted = false;
        private int placeTimer;
        private Texture2D tex;
    
    	public abstract void activated(Entity activator);
	    

        public static int[] runeToolkit = new int[3];

        public enum RuneType {
            Blood,
            Freeze,
            Rend,
            Vortex,
            Anchor,
            Link,
            SumDemon,
            SumLegion,
            SumVictims,
            Mark,
            Teleport
        }


        public Texture2D getTex()
        {
            return tex;
        }

	    public Rune(Texture2D tex, Vector2 position, float _collideRadius, int _rechargeTime, int _powerLevel) : base(null, position, _collideRadius)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
		    this.rechargeTime = _rechargeTime;
            this.powerLevel = _powerLevel;
            this.tex = tex;
            placeTimer = 0;
    	}

        public override void Render(SpriteBatch canvas)
        {
            if (placeTimer < 20)
            {
                placeTimer++;
                canvas.Draw(tex, new Rectangle((int)(position.X + 32 - (32 * (placeTimer / 20.0))), (int)(position.Y + 32 - (32 * (placeTimer / 20.0))),
                    (int)(tex.Width * (placeTimer / 20.0)), (int)(tex.Height * (placeTimer / 20.0))), Color.White);
            }
            else
                canvas.Draw(tex, new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height), Color.White);
        }
    }
}