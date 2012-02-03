using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    class SummonDemonRune : Rune
    {
        public SummonDemonRune(AnimManager manager, Vector2 position, int powerLevel)
            : base(AssetManager.Rune_Texture_Summon_Demon, position, 64f, 45, powerLevel)
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