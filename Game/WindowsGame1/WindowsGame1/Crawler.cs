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
    class Crawler : AICharacter
    {

        public Crawler(AnimManager manager, Vector2 position) : base(manager, position, 26f)
        {
            health = 280;
        }

        private void die()
        {
            renderCode = 3;
        }

        public override int update(int code)
        {

            if (health == 0)
                die();
            return base.update(code);
        }
    }
}
