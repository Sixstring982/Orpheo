
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public abstract class Entity
    {

        protected AnimManager animManager;
        protected Vector2 position;
        protected float rotation;
        protected float collideRadius;
        protected int renderCode;

        public Entity(AnimManager manager, Vector2 _position, float _collideRadius)
        {
            this.animManager = manager;
            this.position = _position;
            this.collideRadius = _collideRadius;
        }

        public float getCollideRadius()
        {
            return this.collideRadius;
        }

        public abstract int update(int code);

        public virtual void Render(SpriteBatch canvas)
        {

            animManager.doRender(renderCode, canvas, (int)position.X, (int)position.Y, rotation);

        }
        public void setPos(Vector2 _position)
        {
            this.position = _position;
        }

        public Vector2 getPos()
        {
            return this.position;
        }


        public bool Collide(Entity b)
        {
            float c = this.collideRadius;
            {
                float n = b.getCollideRadius();
                if (c < n) c = n;
            }

            

            bool x = false;
            return x;
        }

    }
}
