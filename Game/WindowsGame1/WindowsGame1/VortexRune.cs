using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    

    public class VortexRune : Rune
    {
        private int startupTimer = 45;
        private int activationDistance = 10;
        public VortexRune(AnimManager manager, Vector2 position, int powerLevel)
            : base(AssetManager.Rune_Texture_Vortex, position, 100f, 0, powerLevel)
        {

        }

        public override int update(int code)
        {
            startupTimer--;
            if (startupTimer == 0) startupTimer = 0;
            return 0;
        }

        public override void activated(Entity ent)
        {
            if (startupTimer > 0) return;
            if (ent is Living)
            {
                ent.setPos(Vector2.Lerp(position, ent.getPos(), 10*(0.001f/Vector2.Distance(ent.getPos(), this.position))));
                if (Vector2.Distance(ent.getPos(), this.position) < activationDistance)
                {
                    ((Living)ent).damage(5000, Living.DamageType.Burn);
                    Living.gameParent.GetSparkerList().Add(new Sparker(100,
                new Vector2(position.X - 32, position.Y - 32), false, 0, 0, Decal.gibFactory()));
                }
            }
            startupTimer = 45;
        }


        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Rune_Texture_Vortex, Living.getFuckingOffset(position), Color.White);
        }
    }
}