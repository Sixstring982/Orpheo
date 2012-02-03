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
    class HealthBar
    {
        public static void Render(SpriteBatch sb, int health)
        {
            sb.Draw(AssetManager.Health_Bar_Empty,
                new Rectangle(600, 0, (int)(AssetManager.Health_Bar_Empty.Width),
                AssetManager.Health_Bar_Empty.Height), Color.White);
            sb.Draw(AssetManager.Health_Bar_Fill, 
                new Rectangle(600, 0, (int)(AssetManager.Health_Bar_Fill.Width * (health / 100.0)),
                AssetManager.Health_Bar_Fill.Height),
                new Rectangle(0, 0, (int)(AssetManager.Health_Bar_Fill.Width * (health / 100.0)),
                AssetManager.Health_Bar_Fill.Height), Color.White);
        }
    }
}
