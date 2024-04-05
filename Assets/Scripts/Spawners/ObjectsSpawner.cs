﻿using Spawners.ItemsSpawners;
using Spawners.ObjectsSpawners;

namespace Spawners
{
    public class ObjectsSpawner
    {
        public PinkScoreSpawner PinkScoreSpawner { get; }
        public GreenScoresSpawner GreenScoresSpawner { get; }
        public TempleKeeperSpawner TempleKeeperSpawner { get; }
        public LockSpawner LockSpawner { get; }
        public KeySpawner KeySpawner { get; }
        public TrapSpawner TrapSpawner { get; }
        public GhostSpawner GhostSpawner { get; }
        public BoosterSpawner BoosterSpawner { get; }
        public LifeSaverSpawner LifeSaverSpawner { get; }

        public ObjectsSpawner(
            PinkScoreSpawner pinkScoreSpawner, 
            GreenScoresSpawner greenScoresSpawner, 
            TempleKeeperSpawner templeKeeperSpawner, 
            LockSpawner lockSpawner, 
            KeySpawner keySpawner, 
            TrapSpawner trapSpawner, 
            GhostSpawner ghostSpawner,
            BoosterSpawner boosterSpawner,
            LifeSaverSpawner lifeSaverSpawner)
        {
            PinkScoreSpawner = pinkScoreSpawner;
            GreenScoresSpawner = greenScoresSpawner;
            TempleKeeperSpawner = templeKeeperSpawner;
            LockSpawner = lockSpawner;
            KeySpawner = keySpawner;
            TrapSpawner = trapSpawner;
            GhostSpawner = ghostSpawner;
            BoosterSpawner = boosterSpawner;
            LifeSaverSpawner = lifeSaverSpawner;
        }
    }
}