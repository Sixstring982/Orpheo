using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CodenameHorror
{
    public class AnimSprite
    {
        private Texture2D spriteSheet;
        private int frameNum = 0;
        private int totalFrames;
        private Vector2 offset; 
        private float fps = 10.0f;
        private DateTime lastFrame = DateTime.Now;

        public AnimSprite(Texture2D _spriteSheet, int _totalFrames, Vector2 _offset)
        {
            this.spriteSheet = _spriteSheet;
            this.offset = _offset;
            this.totalFrames = _totalFrames;
        }

        public Vector2 getOffset(){
            return offset;
        }
        public Texture2D next()
        {
            if ((DateTime.Now - lastFrame).TotalSeconds > (1 / fps))
            {
                frameNum++; if (frameNum >= totalFrames) frameNum = 0;
                lastFrame = DateTime.Now;
            }
            
            return Crop(spriteSheet, new Rectangle((spriteSheet.Width/totalFrames)*frameNum,0,(spriteSheet.Width/totalFrames), spriteSheet.Height));
        }

        public void reset()
        {
            frameNum = 0;
        }

        public static Texture2D Crop(Texture2D image, Rectangle source)
        {
            var graphics = image.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, source.Width, source.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret); // draw to image
            graphics.Clear(new Color(0, 0, 0, 0));

            sb.Begin();
            sb.Draw(image, Vector2.Zero, source, Color.White);
            sb.End();

            graphics.SetRenderTarget(null); // set back to main window

            return (Texture2D)ret;
        }
    }
}
