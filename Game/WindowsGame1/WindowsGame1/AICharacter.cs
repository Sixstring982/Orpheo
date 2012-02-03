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
    public abstract class AICharacter : Living{
	protected bool playerAlly;
    protected Living target = null;
	protected int soulValue;
    protected float reach = 25.0f;
    protected int damageDealt = 10;

    public AICharacter(AnimManager manager, Vector2 position, float _collideRadius)
        : base(manager, position, _collideRadius)
    {

    }

    public override int update(int code)
    {
        foreach (Rune r in Living.gameParent.GetRuneList())
        {
            if (Vector2.Distance(r.getPos(), position) < r.getCollideRadius())
            {
                r.activated(this);
            }
        }

        if (health > 0)
        {
            renderCode = 1;
            if (target != null)
                moveTowards();
            else
                target = AcquireTarget();
        }
        else
        {
            renderCode = 3;
            return 1;
        }
        
        return 0;
    }

	public bool isAlly(){
		return playerAlly;
	}

    private Living AcquireTarget()
    {
        float xdist = 0.0f;
        float ydist = 0.0f;
        float pdist = 0.0f;
        float closestDist = float.MaxValue;
        int closestIdx = 0;
        for (int i = 0; i < Living.gameParent.GetEntityList().Count; i++)
        {
            if (Living.gameParent.GetEntityList()[i] is AICharacter)
            {
                if (((AICharacter)Living.gameParent.GetEntityList()[i]).isAlly() != this.playerAlly)
                {
                    xdist = position.X - Living.gameParent.GetEntityList()[i].getPos().X;
                    ydist = position.Y - Living.gameParent.GetEntityList()[i].getPos().Y;
                    pdist = (float)Math.Sqrt(xdist * xdist + ydist * ydist);
                    if (pdist < closestDist)
                    {
                        closestDist = pdist;
                        closestIdx = i;
                    }
                }
            }
        }
        return (Living)Living.gameParent.GetEntityList()[closestIdx];
    }

    public void moveTowards()
    {



        float tmpSpeed = speed;
        if (frozen)
        {
            speed = frozenSpeed;
        }
        float xdist = position.X - target.getPos().X;
        float ydist = position.Y - target.getPos().Y;
        float pdist = (float)Math.Sqrt(xdist * xdist + ydist * ydist);
        if (pdist < reach)
        {
            target.damage(10, DamageType.Blunt);
            return;
        }
        Vector2 angle = new Vector2(xdist, ydist);
        angle.Normalize();
        angle.X *= -speed;
        angle.Y *= -speed;
        rotation = (float)Math.Atan2(angle.X, angle.Y) * -1;
        move(angle);
        if (frozen)
        {
            speed = tmpSpeed;
            frozen = false;
        }
	}
	
	public void getNextTarget(){
		//Finds next target to kill
	}
}

}
