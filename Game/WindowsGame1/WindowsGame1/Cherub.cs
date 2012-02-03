using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    class Cherub : AICharacter
    {

        public Cherub(AnimManager manager, Vector2 position)
            : base(manager, position, 26f)
        {
            health = 10;
            speed = 3.1f;
            damageDealt = 60;
        }

        private void die()
        {

        }

        public override int update(int code)
        {
            if (this.target != null)
            
            if (Vector2.Distance(target.getPos(), position) < 32)
            {
                target.freeze(100);
            }
            if (health == 0)
                die();
            return base.update(code);
        }
    }
}
