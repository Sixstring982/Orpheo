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
                activator.setPos(boss.teleportMarker.getPos());
                
                //Do something to draw particle effect at this point.
            
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Rune_Texture_Teleport, Living.getFuckingOffset(position), Color.White);
        }
    }
}
