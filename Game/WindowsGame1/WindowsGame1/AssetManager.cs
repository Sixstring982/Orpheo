using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    public class AssetManager
    {
        public static AnimSprite Player_Anim_Idle;         //Finished
        public static AnimSprite Player_Anim_Walk;         //Finished
        public static AnimSprite Player_Anim_Sweep;        //Finished
        public static AnimSprite Player_Anim_Stab;         //Finished
        public static AnimSprite Player_Anim_Throw;        //Finished
        public static AnimSprite Player_Anim_Draw;         //Finished
        public static AnimSprite Player_Anim_Suicide;      //
        public static AnimSprite Player_Anim_Death;        //Finished

        public static AnimSprite Crawler_Anim_Idle;        //Finished
        public static AnimSprite Crawler_Anim_Walk;        //Finished
        public static AnimSprite Crawler_Anim_Attack;      //Finished
        public static AnimSprite Crawler_Anim_Death;       //Finished

        public static AnimSprite Praet_Anim_Idle;          //Finished
        public static AnimSprite Praet_Anim_Walk;          //Finished
        public static AnimSprite Praet_Anim_Attack;        //Finished
        public static AnimSprite Praet_Anim_Death;         //Finished

        public static AnimSprite Divider_Anim_Idle;          //Finished
        public static AnimSprite Divider_Anim_Walk;          //Finished
        public static AnimSprite Divider_Anim_Attack;        //

        public static AnimSprite Divider_Right_Anim_Idle;   //
        public static AnimSprite Divider_Right_Anim_Walk;   //Finished
        public static AnimSprite Divider_Right_Anim_Attack; //

        public static AnimSprite Divider_Left_Anim_Idle;    //
        public static AnimSprite Divider_Left_Anim_Walk;    //Finished
        public static AnimSprite Divider_Left_Anim_Attack;  //

        public static AnimSprite Divider_Main_Anim_Idle;    //
        public static AnimSprite Divider_Main_Anim_Walk;    //Finished
        public static AnimSprite Divider_Main_Anim_Attack;  //
        public static AnimSprite Divider_Main_Anim_Death;   //

        public static AnimSprite Cherub_Anim_Idle;          //Finished
        public static AnimSprite Cherub_Anim_Walk;          //Finished
        public static AnimSprite Cherub_Anim_Attack;        //
        public static AnimSprite Cherub_Anim_Death;         //

        public static Texture2D Player_Texture_Corpse;      //Finished
        public static Texture2D Crawler_Texture_Corpse;     //Finished
        public static Texture2D Praet_Texture_Corpse;       //Finished
        public static Texture2D Cherub_Texture_Corpse;      //

        public static Texture2D Rune_Texture_Teleport;     //Finished
        public static Texture2D Rune_Texture_Rend;         //Finished
        public static Texture2D Rune_Texture_Anchor;       //Finished
        public static Texture2D Rune_Texture_Mark;          //Finished
        public static Texture2D Rune_Texture_Blood;         //Finished
        public static Texture2D Rune_Texture_Freeze;        //Finished
        public static Texture2D Rune_Texture_Swarm;         //Finished
        public static Texture2D Rune_Texture_Vortex;        //Finished
        public static Texture2D Rune_Texture_Link;          //Finished
        public static Texture2D Rune_Texture_Summon_Demon;  //Finished
        public static Texture2D Rune_Texture_Summon_Legion; //Finished
        public static Texture2D Rune_Texture_Summon_Victims;//Finished

        public static Texture2D Health_Bar_Empty;
        public static Texture2D Health_Bar_Fill;

        public static Texture2D Blood_Splat_01;
        public static Texture2D Blood_Splat_02;
        public static Texture2D Blood_Splat_03;

        public static Texture2D Gib_01;
        public static Texture2D Gib_02;
        public static Texture2D Gib_03;
        public static Texture2D Gib_04;

        public static Texture2D SoulTexture;
        public static Texture2D InventoryBackground;
        public static Texture2D InstructionsScreen0;
        public static Texture2D InstructionsScreen1;
        public static Texture2D InstructionsScreen2;
        public static Texture2D DeathScreen;

        public static Texture2D SplashScreen;

    }
}
