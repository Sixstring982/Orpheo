using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public class Soul : Entity
    {

        public int soulValue;
        public Soul(int worth, AnimManager manager, Vector2 position, float _collideRadius)
            : base(manager, position, _collideRadius)
        {
            this.soulValue = worth;

        }
        public override int update(int code)
        {
            if (Vector2.Distance(new Vector2(Living.gameParent.getPlayer().getPos().X+64, Living.gameParent.getPlayer().getPos().Y+64), position) < 40)
            {
                Living.gameParent.getPlayer().devour(this);
                return 1;
            }
            return 0;
        }

        public override void Render(SpriteBatch canvas)
        {
            canvas.Draw(AssetManager.SoulTexture, position, Color.White);
        }
    }
}
