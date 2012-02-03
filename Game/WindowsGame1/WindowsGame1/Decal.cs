using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CodenameHorror
{
    public class Decal
    {

        public Texture2D decal;
        public Vector2 position;

        public Decal(Texture2D decal, Vector2 position)
        {
            this.decal =  decal;
            this.position = position;
        }


        public void draw(SpriteBatch canvas)
        {
            canvas.Draw(decal, position, Color.White);
        }

        public static Texture2D decalFactory()
        {
            Random rand = new Random();
            if ((rand.Next() % 2) > 0)
            {
                return AssetManager.Blood_Splat_01;
            }
            else if ((rand.Next() % 2) > 0)
            {
                return AssetManager.Blood_Splat_02;
            }
            else
            {
                return AssetManager.Blood_Splat_03;
            }
        }



        public static Texture2D gibFactory()
        {
            Random rand = new Random();
            if ((rand.Next() % 2) > 0)
            {
                return AssetManager.Gib_01;
            }
            else if ((rand.Next() % 2) > 0)
            {
                return AssetManager.Gib_02;
            }
            else if ((rand.Next() % 2) > 0)
            {
                return AssetManager.Gib_03;
            }
            else
            {
                return AssetManager.Gib_04;
            }
        }
    }


}
