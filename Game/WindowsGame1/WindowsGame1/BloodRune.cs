using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    class BloodRune : Rune
    {
        public BloodRune(AnimManager manager, Vector2 position, int powerLevel)
            : base(AssetManager.Rune_Texture_Blood, position, 32f, 30, powerLevel)
        {

        }

        public override int update(int code)
        {
            return 0;
        }

        public override void activated(Entity ent)
        {
        }
    }
}