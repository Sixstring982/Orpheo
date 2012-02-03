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
            if (boss.teleportMarker == null) return;
            activator.setPos(new Vector2(boss.teleportMarker.getPos().X + 64,
                boss.teleportMarker.getPos().Y + 64));

            Sparker s = new Sparker(50, new Vector2(activator.getPos().X - 32,
                    activator.getPos().Y - 32));
            s.SetGradient(Color.Magenta, Color.Maroon);
            s.Fire();
            Living.gameParent.GetSparkerList().Add(s);
        }
    }
}
