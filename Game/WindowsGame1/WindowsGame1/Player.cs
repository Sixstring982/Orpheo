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
    public class Player : Living{
	    private int soulCount = 1;
        public MarkRune teleportMarker = null;
        private int deadTimer = 0;
        private int attackTimer = 0;
        private int selectedRune = 0;
        private float reach = 50.0f;
        private AttackType currentAttack = AttackType.None;
        private class RuneItem
        {
            public Rune rune;
            public Color sparkColor;
        }
        private List<RuneItem> RuneInventory = new List<RuneItem>();
        private List<Living> linkedSouls = new List<Living>();
        private int keyCount = 0;
        

        public void linkSoul(Living liveEnt)
        {
            linkedSouls.Add(liveEnt);
        }

        /*Stuff to add*/
        /*
         * Cool Soul animation
         *
         * */

        private enum AttackType
        {
            None,
            Swing,
            Stab,
            Hurl,
            Suicide
        }

        private static KeyboardState prevKeys;

        public void SetParent(Game1 parent)
        {
            gameParent = parent;
        }

        public override void damage(int amount, Living.DamageType damageType)
        {
            for (int i = 0; i < linkedSouls.Count; i++)
                {
                    linkedSouls[i].damage((int)(0.2 * amount * getRuneAdjustment())/linkedSouls[i].getSoulPower(), damageType);
                    amount -= (int)(0.2 * amount);
                }
            base.damage(amount, damageType);
        }

        public override void Render(SpriteBatch canvas)
        {

            base.Render(canvas);
            //canvas.DrawString(Living.DamageFont, string.Format("{0:0}", selectedRune), new Vector2(22, 22), Color.Black);
            //canvas.DrawString(Living.DamageFont, string.Format("{0:0}", selectedRune), new Vector2(20, 20), Color.White);
            canvas.Draw(AssetManager.InventoryBackground, new Vector2(0, 0), Color.White);
            Vector2 runeTexPos = new Vector2(1, 1);
            for(int i=0;i<RuneInventory.Count;i++){
                RuneItem ri = RuneInventory[i];
                Texture2D runeTex = ri.rune.getTex();
                Color drawColor = Color.DarkGray;
                if(i == selectedRune) drawColor = Color.White;
                if(runeTex != null)
                    canvas.Draw(runeTex, runeTexPos, drawColor);
                runeTexPos.X += 64;
            }

            canvas.Draw(AssetManager.SoulTexture, new Vector2(400, 1), Color.White);
            canvas.DrawString(Living.DamageFont, string.Format(":{0:0}", soulPower), new Vector2(448, 20), Color.White);



           
        }

        public void PickupKey()
        {
            keyCount++;
        }

        public bool HasKey()
        {
            return (keyCount > 0);
        }

        public void RemoveKey()
        {
            keyCount--;
        }

        public override int update(int code)
        {
            if (deathFlag) deathFlag = false;

            foreach (Rune r in Living.gameParent.GetRuneList())
            {
                if (Vector2.Distance(r.getPos(), position) < r.getCollideRadius())
                {
                    r.activated(this);
                }
            }
            if (attackTimer > 0)
            {

                attackTimer++;
                switch (currentAttack)
                {
                    case AttackType.Swing:
                        renderCode = 2;
                        if (attackTimer > 50)
                        {
                            renderCode = 0;
                            attackTimer = 0;
                        }
                        foreach (Entity e in Living.gameParent.GetEntityList())
                        {
                            if (e is Living)
                            {
                                if (!(e is Player))
                                {
                                    float xdist = position.X - e.getPos().X;
                                    float ydist = position.Y - e.getPos().Y;
                                    float pdist = (float)Math.Sqrt(xdist * xdist + ydist * ydist);
                                    if (pdist < reach)
                                        ((Living)e).damage(40, DamageType.Blunt);
                                }
                            }
                        }
                        break;

                    case AttackType.Stab:
                        renderCode = 3;
                        if (attackTimer > 40)
                        {
                            renderCode = 0;
                            attackTimer = 0;
                        }
                        foreach (Entity e in Living.gameParent.GetEntityList())
                        {
                            if (e is Living)
                            {
                                if (!(e is Player))
                                {
                                    for (int i = 0; i < 2; i++)
                                    {
                                        Vector2 spot = new Vector2((position.X) + i * (unitVectorRotation.X), (position.Y) + i * (unitVectorRotation.Y));

                                        if (Vector2.Distance(spot, e.getPos()) <= 32)
                                            ((Living)e).damage(20, DamageType.Blunt);

                                    }
                                }
                            }
                        }
                        break;

                    case AttackType.Hurl:
                        renderCode = 4;
                        if (attackTimer > 55)
                        {
                            renderCode = 0;
                            attackTimer = 0;
                        }
                        foreach (Entity e in Living.gameParent.GetEntityList())
                        {
                            if (e is Living)
                            {
                                if (!(e is Player))
                                {
                                    for (int i = 0; i < 8; i++) 
                                    {
                                        Vector2 spot = new Vector2((position.X) + i * (unitVectorRotation.X ), (position.Y) + i * (unitVectorRotation.Y ));
                                        
                                        if(Vector2.Distance(spot, e.getPos()) <= 32)
                                            ((Living)e).damage(20, DamageType.Blunt);

                                    }


                                }
                            }
                        }
                        break;

                    case AttackType.Suicide:
                        renderCode = 6;
                        if (attackTimer > 55)
                        {
                            renderCode = 0;
                            attackTimer = 0;
                            health = 0;
                        }
                        foreach (Entity e in Living.gameParent.GetEntityList())
                        {
                            if (e is Living)
                            {
                                if (!(e is Player))
                                {
                                        Vector2 spot = new Vector2((position.X) + (-unitVectorRotation.X ), (position.Y) +  (-unitVectorRotation.Y ));
                                        
                                        if(Vector2.Distance(spot, e.getPos()) <= 32)
                                            ((Living)e).damage(80, DamageType.Blunt);

                                    


                                }
                            }
                        }
                        return 1;

                }
            }
            else if (base.health == 0)
            {
                
                die();
                if (deadTimer == 63)
                    return 1;
            }
            else
            {
                CheckForRoomChange();
                processInput();
            }
            return 0;
        }

        private void CheckForRoomChange()
        {
            if (position.X < 0 && position.X > -290)
            {
                position.X = TileMap.tileSize * TileMap.MapWidth - 10;
                Living.gameParent.ChangeRoom(new Vector2(TileMap.tileSize * TileMap.MapWidth - 10, position.X),
                    Living.gameParent.currentMap.xLoc - 1, Living.gameParent.currentMap.yLoc);
            }
            else if (position.Y < 1 && position.Y > -290)
            {
                position.Y = TileMap.tileSize * TileMap.MapHeight - 10;
                Living.gameParent.ChangeRoom(new Vector2(TileMap.tileSize * TileMap.MapWidth - 10, position.X),
                    Living.gameParent.currentMap.xLoc, Living.gameParent.currentMap.yLoc - 1);
            }
            else if (position.X > TileMap.tileSize * TileMap.MapWidth)
            {
                position.X = 10;
                Living.gameParent.ChangeRoom(new Vector2(TileMap.tileSize * TileMap.MapWidth - 10, position.X),
                    Living.gameParent.currentMap.xLoc + 1, Living.gameParent.currentMap.yLoc);
            }
            else if (position.Y > TileMap.tileSize * TileMap.MapHeight)
            {
                position.Y = 10;
                Living.gameParent.ChangeRoom(new Vector2(TileMap.tileSize * TileMap.MapWidth - 10, position.X),
                    Living.gameParent.currentMap.xLoc, Living.gameParent.currentMap.yLoc + 1);
            }
        }

        public void devour(Soul soul)
        {
            this.soulCount++;
            this.health += soul.soulValue/4;
            this.soulPower += soul.soulValue;
            



        }

        private enum inputType
        {
            None,
            Movement,
            Attack,
            Rune
        }
        Vector2 unitVectorRotation;


        public void processInput()
        {

            unitVectorRotation = new Vector2((int)(-Math.Sin(rotation) * 2) * 64, (int)(Math.Cos(rotation) * 1.9) * 64);
            KeyboardState keyState = Keyboard.GetState();
            Vector2 movementVector = new Vector2(0, 0);
            inputType iT = inputType.None;

            if (keyState.IsKeyDown(Keys.Left)&&
                !prevKeys.IsKeyDown(Keys.Left))  //Stab
            {
                attackTimer = 1;
                currentAttack = AttackType.Stab;
                iT = inputType.Attack;
            }
            if (keyState.IsKeyDown(Keys.Down)&&
                !prevKeys.IsKeyDown(Keys.Down))  //Slash
            {
                attackTimer = 1;
                currentAttack = AttackType.Swing;
                iT = inputType.Attack;
            }
            if (keyState.IsKeyDown(Keys.Right)&&
                !prevKeys.IsKeyDown(Keys.Right)) //Throw
            {
                attackTimer = 1;
                currentAttack = AttackType.Hurl;
                iT = inputType.Attack;
            }
            if (keyState.IsKeyDown(Keys.X) &&
                !prevKeys.IsKeyDown(Keys.X))       //Suicide
            {
                attackTimer = 1;
                currentAttack = AttackType.Suicide;
                iT = inputType.Attack;
            }

            if (keyState.IsKeyDown(Keys.L) &&
                !prevKeys.IsKeyDown(Keys.L))//Soul up
            {
                selectedRune++;
                if (selectedRune >= RuneInventory.Count) selectedRune = 0;
            }
            if (keyState.IsKeyDown(Keys.K) &&
                !prevKeys.IsKeyDown(Keys.K))//Soul use
            {
                //Vector2 oldPlacement = new Vector2(position.X - 64, position.Y);

                
                Vector2 newPlacement = new Vector2(position.X - unitVectorRotation.X, position.Y - unitVectorRotation.Y);


                int midY = (int)((this.position.Y - 32) / TileMap.tileSize);
                int midX = (int)((this.position.X - 32) / TileMap.tileSize);

                if (gameParent.currentMap.data[midX][midY][2] == 0xFF) //THIS is sexier

                //This code is soo much sexyer, because it gets rid of possible bugs involving adding runes onto Level 3 Tiles
                //Vector2 sexyPlacement = new Vector2(position.X - 64, position.Y - 64);
                //no it isnt. all this does it move it to a corner, which is awful in every way.

                Living.gameParent.PlaceRune(RuneInventory[selectedRune].rune, newPlacement, RuneInventory[selectedRune].sparkColor);
            }
            if (keyState.IsKeyDown(Keys.J) &&
                !prevKeys.IsKeyDown(Keys.J))//Soul down
            {
                selectedRune--;
                if (selectedRune < 0) selectedRune = RuneInventory.Count - 1;
            }

            { 
            }

            if (keyState.IsKeyDown(Keys.W))    //North
            {
                movementVector.Y = -speed;
                iT = inputType.Movement;
            }
            if (keyState.IsKeyDown(Keys.A))    //West
            {
                movementVector.X = -speed;
                iT = inputType.Movement;
            }
            if (keyState.IsKeyDown(Keys.S))    //South
            {
                movementVector.Y = speed;
                iT = inputType.Movement;
            }
            if (keyState.IsKeyDown(Keys.D))    //East
            {
                movementVector.X = speed;
                iT = inputType.Movement;
            }
            
            calculateRotation(movementVector);
            if (iT == inputType.Movement)
            {
                move(movementVector);
                renderCode = 1;
            }
            if (iT == inputType.None)
            {
                renderCode = 0;
            }

            prevKeys = Keyboard.GetState();
        }
        /// <summary>
        /// new SoulAnchorRune(null, new Vector2(position.X - 64, position.Y), 30.0f, 0, 0), position)
        /// </summary>
        /// <param name="movementVector"></param>


        private void calculateRotation(Vector2 movementVector)
        {
            if (movementVector == Vector2.Zero) return;
            rotation = (float)Math.Atan2(movementVector.X, movementVector.Y)*-1;
            //rotation = MathHelper.ToDegrees((float)rad);
        }
        public bool deathFlag = false;
        private void die()
        {
            this.renderCode = 7;
            if (deadTimer == 0)
            {
                deadTimer = 1;
                rotation = 0.0f; //So that the death anims line up
            }
            else
            {
                deadTimer++;
                if (deadTimer > 64)
                {
                    this.position.X = -300;
                    this.position.Y = -300;
                }
                if (deadTimer > 100)
                {
                    deathFlag = true;
                    
                    health = 100;
                    this.renderCode = 1;
                    for (int i = 0; i < Living.gameParent.GetRuneList().Count; i++)
                    {
                        if (Living.gameParent.GetRuneList()[i].GetType() == new SoulAnchorRune(null, Vector2.Zero, 0.0f, 0, 0).GetType())
                        {
                            this.position = Living.gameParent.GetRuneList()[i].getPos();
                            this.position.X += 64;
                            this.position.Y += 64;
                            Living.gameParent.GetRuneList().RemoveAt(i);
                            Sparker sp = new Sparker(100, new Vector2(position.X - 32, position.Y));
                            sp.SetGradient(new Color(0xFF, 0x0, 0xFF, 0xFF), new Color(0xFF, 0x0, 0xFF, 0xFF));
                            sp.Fire();
                            Living.gameParent.GetSparkerList().Add(sp);
                            deadTimer = 0;
                            return;
                        }
                    }
                    deadTimer = 0;
                    this.position = new Vector2(TileMap.tileSize * TileMap.MapWidth / 2, TileMap.tileSize * TileMap.MapHeight / 2);
                }
            }
        }

	    public Player(AnimManager manager, Vector2 position, float _collideRadius) : base(manager, position, _collideRadius)
        {
            health = 100;

            RuneItem ri = new RuneItem();
            ri.rune = new SoulAnchorRune(null, Vector2.Zero, 0.0f, 20, 10);
            ri.sparkColor = Color.White;
            RuneInventory.Add(ri);

            
            ri = new RuneItem();
            ri.rune = new MarkRune(this, null, Vector2.Zero, 32f, 0, this.getRuneAdjustment());
            ri.sparkColor = Color.Red;
            RuneInventory.Add(ri);
            speed = 3.0f;

            RuneItem rj = new RuneItem();
            rj.rune = new TeleportRune(this, null, Vector2.Zero);
            rj.sparkColor = Color.Magenta;
            RuneInventory.Add(rj);
            speed = 1.6f;

            ri = new RuneItem();
            ri.rune = new FreezeRune(null, Vector2.Zero, this.getRuneAdjustment());
            ri.sparkColor = Color.Teal;
            RuneInventory.Add(ri);
            speed = 1.2f;

            ri = new RuneItem();
            ri.rune = new RendRune(this, null, Vector2.Zero);
            ri.sparkColor = Color.YellowGreen;
            RuneInventory.Add(ri);
            speed = 2.0f;

            ri = new RuneItem();
            ri.rune = new VortexRune(null, Vector2.Zero, this.getRuneAdjustment());
            ri.sparkColor = Color.LemonChiffon;
            RuneInventory.Add(ri);
            speed = 2.0f;
            


        }

        public void drawRune(int code)
        {

        }
	
	    public int getSoulValue()
        {
		    return soulPower;
	    }
	
	    public int getRuneAdjustment(){
		    return soulPower/soulCount;
	    }
	
	
    }
}
