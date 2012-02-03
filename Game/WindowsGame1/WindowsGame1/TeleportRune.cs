using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CodenameHorror
{
    public class TeleportRune : Rune
    {
        Player boss;
        public TeleportRune(Player boss, AnimManager manager, Vector2 position) : 
            base(AssetManager.Rune_Texture_Teleport, position, 32f, 0,0)
        {
            this.boss = boss;
        }

        public override int update(int code)
        {
            return 0;
        }

        public override void activated(Entity activator)
        {
            MarkRune m = null;
            for(int i = 0; i < Living.gameParent.GetRuneList().Count; i++)
            {
                if(Living.gameParent.GetRuneList()[i] is MarkRune)
                {
                    m = (MarkRune)Living.gameParent.GetRuneList()[i];
                    break;
                }
            }
            if (m != null) return;
            activator.setPos(new Vector2(m.getPos().X + 64, m.getPos().Y + 64));

            Sparker s = new Sparker(50, new Vector2(activator.getPos().X - 32,
                    activator.getPos().Y - 32));
            s.SetGradient(Color.Magenta, Color.Maroon);
            s.Fire();
            Living.gameParent.GetSparkerList().Add(s);
        }
    }
}
