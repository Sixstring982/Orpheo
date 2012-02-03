using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public class RendRune : Rune
    {
        public static int ATTACK_DAMAGE = 30;
        private int startTimer = 35;

        public RendRune(Player p, AnimManager animManager, Vector2 position) :
            base(AssetManager.Rune_Texture_Rend, position, 32f, 0, p.getSoulValue())
        {
        }

        public override int update(int code)
        {
            startTimer--;
            if (startTimer < 0) startTimer = 0;
            if (acted) return 1;
            return 0;
        }

        public override void activated(Entity activator)
        {
            if (startTimer <= 0)
            {
                if (activator is Living)
                {
                    ((Living)activator).damage(ATTACK_DAMAGE * this.powerLevel, Living.DamageType.Spike);
                    this.acted = true;
                    Sparker s = new Sparker(100, new Vector2(position.X + 32, position.Y + 32));
                    s.SetGradient(Color.Yellow, Color.DarkGray);
                    s.Fire();
                    Living.gameParent.GetSparkerList().Add(s);
                }
            }
        }
    }
}
