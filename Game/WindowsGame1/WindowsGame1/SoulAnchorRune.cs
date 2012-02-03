using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public class SoulAnchorRune : Rune
    {

        public SoulAnchorRune(AnimManager manager, Vector2 position, float _collideRadius, int _rechargeTime, int _powerLevel)
            : base(AssetManager.Rune_Texture_Anchor, position, _collideRadius, _rechargeTime, _powerLevel)
        {

        }

        public override int update(int code)
        {
            return 0;
        }
        public override void activated(Entity activator)
        {
            return;
        }

    }
}
