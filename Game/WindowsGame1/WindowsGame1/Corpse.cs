using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public class Corpse
    {
        private Texture2D texture;
        private Vector2 location;

        public Corpse(Texture2D _texture, Vector2 _location)
        {
            this.texture = _texture;
            this.location.X = _location.X - 64;
            this.location.Y = _location.Y - 64;

        }

        public void render(SpriteBatch canvas){
            canvas.Draw(texture, location, Color.White);
        }


    }
}
