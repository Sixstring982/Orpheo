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
    class Praetorian : AICharacter
    {
        private int chargeCounter = 0;
        private int cooldownCounter = 0;
        public Praetorian(AnimManager manager, Vector2 position)
            : base(manager, position, 26f)
        {
            health = 450;
            speed = 0.7f;
            soulPower = 10;
        }

        private void die()
        {
            renderCode = 3;
        }

        public override int update(int code)
        {
            if (cooldownCounter > 0) cooldownCounter++;
            if (cooldownCounter > 150) cooldownCounter = 0;
            if (chargeCounter > 0) chargeCounter++;
            if (chargeCounter > 50)
            {
                chargeCounter = 0;
                speed = 0.7f;
                cooldownCounter = 1;
                renderCode = 1;
            }
            if (this.target != null)
            {
                if (cooldownCounter == 0 && Vector2.Distance(this.position, this.target.getPos()) < 250)
                {
                    renderCode = 2;
                    speed = 2.8f;
                    chargeCounter = 1;
                }
                if (Collide(target) && speed > 0.7f)
                {
                    target.damage((int)(speed * soulPower * 3), DamageType.Blunt);
                    target.setPos(Vector2.Add(position, new Vector2(5, 5)));
                }
                else if (Collide(target)) target.damage((int)(speed * soulPower), DamageType.Blunt);
            }

            if (health == 0)
                die();
            return base.update(code);
        }
    }
}
