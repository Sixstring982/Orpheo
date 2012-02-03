using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public class Sparker
    {
        private const double maxSparkSpeed = 4.0;

        class Spark
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
        }
        private int life;
        private List<Spark> sparks = new List<Spark>();
        private int maxSparks;
        private Vector2 location;
        private const float gravity = 0.1f;
        public static Texture2D particleTex;
        private Color grad1 = Color.Red, grad2 = Color.Yellow;
        private bool autoFire = false;
        private int afDelay = 0;
        private int afposition = 0;

        /// <summary>
        /// Creates a new sparker.
        /// </summary>
        /// <param name="intensity">5 to 100, the amount of sparks to throw.</param>
        public Sparker(int intensity, Vector2 location, bool autoFire = false, int autoFireDelay = 0, int afinitdelay = 0,
                       Texture2D tex = null)
        {
            if(tex != null) particleTex = tex;
            this.autoFire = autoFire;
            this.afDelay = autoFireDelay;
            this.afposition += afinitdelay;
            life = 0;
            this.location = location;
            if (intensity > 100) intensity = 100;
            if (intensity < 5) intensity = 5;
            maxSparks = intensity;
        }

        public void SetGradient(Color c1, Color c2)
        {
            grad1 = c1;
            grad2 = c2;
        }

        public void Fire()
        {
            Random rand = new Random();
            for (int i = 0; i < maxSparks; i++)
            {
                double speed = rand.NextDouble() * maxSparkSpeed;
                double rotation = Math.PI / 2;
                double colorMix = rand.NextDouble();
                Color c = new Color();
                c.A = 0xFF;
                c.R = (byte)((colorMix * grad1.R) + ((1 - colorMix) * grad2.R));
                c.B = (byte)((colorMix * grad1.B) + ((1 - colorMix) * grad2.B));
                c.G = (byte)((colorMix * grad1.G) + ((1 - colorMix) * grad2.G));
                rotation += rand.NextDouble() % Math.PI * (rand.Next() % 2 == 0 ? -1 : 1);
                Spark s = new Spark();
                s.position.X = location.X;
                s.position.Y = location.Y;
                s.velocity.X = (float)(Math.Cos(rotation) * speed);
                s.velocity.Y = (float)(Math.Sin(rotation) * -speed);
                s.color = c;
                sparks.Add(s);
            }
            life = 1;
        }

        public void Update()
        {
            if (autoFire)
            {
                if(life == 0)
                {
                    afposition++;
                    if (afposition > afDelay)
                    {
                        afposition = 0;
                        Fire();
                    }
                }
            }
            if (life > 0)
            {
                for (int i = 0; i < sparks.Count; i++)
                {
                    sparks[i].position.X += sparks[i].velocity.X;
                    sparks[i].position.Y += sparks[i].velocity.Y;
                    sparks[i].velocity.Y += gravity;
                    sparks[i].color.A--;
                    if (sparks[i].color.A == 255) sparks[i].color.A = 0;
                }
                life++;
                if (life > 100)
                {
                    sparks = new List<Spark>();
                    life = 0;
                }
            }
        }

        public bool isAlive()
        {
            if (!autoFire && life == 0)
                return false;
            return true;
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < sparks.Count; i++)
            {
                sb.Draw(particleTex, new Rectangle((int)sparks[i].position.X, (int)sparks[i].position.Y, particleTex.Width + 1, particleTex.Height + 1), sparks[i].color);
            }
        }
    }
}
