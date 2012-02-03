using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public abstract class Living : Entity
    {
        public static Vector2 getFuckingOffset(Vector2 position)
        {
            return new Vector2(position.X - 64, position.Y - 64);
        }

        public static Game1 gameParent;
        private class DamageIndicator
        {
            public int liveTime = 0;
            public int damageValue = 0;
            public Vector2 source;
            public Color color = Color.Red;
        }

        public enum DamageType
        {
            Spike,
            Burn,
            Blunt
        }

        public int health = 20;
        private Random rand = new Random();
        protected int soulPower = 20;
        protected int onID = 0;
        protected float speed = 1.3f;
        public static SpriteFont DamageFont;
        private List<DamageIndicator> damageIndicators = new List<DamageIndicator>();

        public int getSoulPower()
        {
            return soulPower;
        }




        public virtual void damage(int amount, DamageType type)
        {
            bool canTakeDamage = true;
            

            for (int i = 0; i < damageIndicators.Count; i++)
            {
                switch (type)
                {
                    case DamageType.Spike:
                        if (damageIndicators[i].liveTime < 10)
                            canTakeDamage = false;
                        break;
                    case DamageType.Burn:
                        if (damageIndicators[i].liveTime < 2)
                            canTakeDamage = false;
                        break;
                    case DamageType.Blunt:
                        if (damageIndicators[i].liveTime < 20)
                            canTakeDamage = false;
                        break;
                }
            }
            /*Check to see if a live timer has been alive longer than the minimum
            before taking damage*/
            if (canTakeDamage)
            {
                Living.gameParent.blood_spout_list.Add(new Sparker(((amount % 30) + 5),
                new Vector2(position.X - 32, position.Y - 32), false, 0, 0, Decal.gibFactory()));

                Living.gameParent.blood_splat_list.Add(new Decal(Decal.decalFactory(), new Vector2(position.X - 32, position.Y - 32)));
                if (health <= 0) canTakeDamage = false;

                health -= amount;
                if (health < 0) health = 0;
                DamageIndicator di = new DamageIndicator();
                di.damageValue = amount;
                di.liveTime = 1;
                di.source.X = base.position.X;
                di.source.Y = base.position.Y - 64;
                damageIndicators.Add(di);
                if (this is Player)
                    di.color = Color.Red;
                else
                    di.color = Color.Yellow;
            }
        }

        public bool frozen = false;
        protected float frozenSpeed = 0.02f;
        public void freeze(int power){

            frozen = true;
        }
        
        protected void move(Vector2 newVect)
        {
            
            
            
            int midY = (int)((this.position.Y - 32) / TileMap.tileHeight);
            if (midY < 0) midY = 0;
            this.position.X += newVect.X;
            bool moveX = true;
            int midX = (int)((this.position.X - 32) / TileMap.tileWidth);
            if (midX < 0) midX = 0;
            if (gameParent.currentMap.data[midX][midY][2] != 0xFF)
            {
                if (gameParent.currentMap.data[midX][midY][2] == 0x78)
                {
                    if (this is Player)
                    {
                        if (((Player)this).HasKey())
                        {
                            ((Player)this).RemoveKey();
                            gameParent.currentMap.data[midX][midY][2] = 0xFF;
                            gameParent.currentMap.data[midX][midY][1] = 0xFF;
                        }
                        else
                            moveX = false;
                    }
                    else
                        moveX = false;
                }
                else
                    moveX = false;
            }
            this.position.X -= newVect.X;
            midX = (int)((this.position.X - 32) / TileMap.tileWidth);
            this.position.Y += newVect.Y;
            midY = (int)((this.position.Y - 32) / TileMap.tileHeight);
            if (midY < 0) midY = 0;
            if (midX < 0) midX = 0;
            if (gameParent.currentMap.data[midX][midY][2] != 0xFF)
            {
                if (gameParent.currentMap.data[midX][midY][2] == 0x78)
                {
                    if (this is Player)
                    {
                        if (((Player)this).HasKey())
                        {
                            ((Player)this).RemoveKey();
                            gameParent.currentMap.data[midX][midY][2] = 0xFF;
                            gameParent.currentMap.data[midX][midY][1] = 0xFF;
                        }
                        else
                            this.position.Y -= newVect.Y;
                    }
                    else
                        this.position.Y -= newVect.Y;
                }
                else
                    this.position.Y -= newVect.Y;
            }

            if (moveX) this.position.X += newVect.X;

            midX = (int)((this.position.X - 32) / TileMap.tileWidth);
            midY = (int)((this.position.Y - 32) / TileMap.tileHeight);
            if (midY < 0) midY = 0;
            if (midX < 0) midX = 0;
            onID = gameParent.currentMap.data[midX][midY][1];
            switch (gameParent.currentMap.data[midX][midY][1]) /*Level Two Switches*/
            {
                case 0x48:
                case 0x49:
                case 0x4a:
                    damage(10, DamageType.Spike);
                    break;
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x35:
                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                case 0x3a:
                case 0x3b:
                case 0x3c:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                    damage(5, DamageType.Burn);
                    break;
                case 0x77:
                    if (this is Player)
                    {
                        ((Player)this).PickupKey();
                        gameParent.currentMap.data[midX][midY][1] = 0xFF;
                    }
                    break;
            }
            
        }

        public Living(AnimManager manager, Vector2 position, float _collideRadius)
            : base(manager, position, _collideRadius)
        {

        }

        

        public override void Render(SpriteBatch canvas)
        {
            base.Render(canvas);
            for(int i = 0; i < damageIndicators.Count; i++)
            {
                if (damageIndicators[i].liveTime > 0)
                {
                    damageIndicators[i].liveTime++;
                    damageIndicators[i].color.A -= 20;
                    if (damageIndicators[i].color.A == 255) damageIndicators[i].color.A = 0;
                    if (damageIndicators[i].liveTime > 100)
                    {
                        damageIndicators[i].liveTime = 0;
                        damageIndicators.Remove(damageIndicators[i]);
                        continue;
                    }

                    damageIndicators[i].source.Y--;
                    damageIndicators[i].source.X += (float)((8 * rand.NextDouble()) - 4);
                    canvas.DrawString(DamageFont, damageIndicators[i].damageValue.ToString(),
                        damageIndicators[i].source, damageIndicators[i].color);
                }
            }
        }
    }
}
