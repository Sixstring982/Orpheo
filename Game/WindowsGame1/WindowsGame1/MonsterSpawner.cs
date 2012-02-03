using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CodenameHorror
{
    class MonsterSpawner
    {
        int spawnTimer = 0;
        int spawnTimeMax = 0;
        int monstersSpawned = 0;
        int maxMonsters;
        public bool alive = true;
        Vector2 position;
        static Random rand = new Random();
        public MonsterSpawner(Vector2 position)
        {
            this.position = position;
            maxMonsters = (rand.Next() % 4) + 3;
            spawnTimeMax = (rand.Next() % 300) + 300;
            spawnTimer = 1;
        }

        public void Update()
        {
            if (spawnTimer > 0)
            {
                spawnTimer++;
                if (spawnTimer > spawnTimeMax)
                {
                    Living.gameParent.AddMonster(position);
                    monstersSpawned++;
                    spawnTimer = 1;
                }
                if (monstersSpawned >= maxMonsters)
                    alive = false;
            }
        }
    }
}
