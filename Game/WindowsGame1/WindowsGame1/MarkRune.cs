using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public class MarkRune : Rune
    {
        public MarkRune(Player p, AnimManager manager, Vector2 position, float collideRadius, int rechargeTime, int powerLevel)
            : base(AssetManager.Rune_Texture_Mark, position, collideRadius, rechargeTime, powerLevel)
        {
        }

        public override void activated(Entity e)
        {
            //DO NOTHING WHEN STEPPED ON
        }

        public override int update(int code)
        {
            return 0;
        }
    }
}
