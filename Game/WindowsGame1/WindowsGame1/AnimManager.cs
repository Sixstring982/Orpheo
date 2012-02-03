using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace CodenameHorror
{
    public class AnimManager
    {
        protected AnimSprite[] sprites;
        protected int lastCode;
        protected Texture2D currentFrame;

        public AnimManager(AnimSprite[] _sprites)
        {
            this.sprites = new AnimSprite[_sprites.Length];
            for(int i = 0; i < _sprites.Length; i++)
                this.sprites[i] = _sprites[i];
        }

        public void doRender(int code, SpriteBatch canvas, int x, int y, float rotation = 0)
        {
            if (code != lastCode)
                sprites[lastCode].reset();
            lastCode = code;

            AnimSprite curSprite = sprites[code];
            currentFrame = sprites[code].next();
            Vector2 offset = sprites[code].getOffset();


             canvas.Draw(currentFrame, new Vector2(x-offset.X, y-offset.Y), null, Color.White,
                 rotation, offset, 1.0f, SpriteEffects.None, 0.0f);


        }
    }
}
