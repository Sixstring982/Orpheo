﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    class SoulLinkRune : Rune
    {
        public SoulLinkRune(AnimManager manager, Vector2 position, int powerLevel)
            : base(AssetManager.Rune_Texture_Link, position, 64f, 60, powerLevel)
        {

        }

        public override int update(int code)
        {
            return 0;
        }

        public override void activated(Entity ent)
        {
        }
    }
}