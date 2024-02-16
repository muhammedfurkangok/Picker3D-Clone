using UnityEngine;

namespace Runtime.Commands
{
    public class OnLevelLoaderCommand
    {
        private Transform _levelHolder;
        public OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute(byte levelIndex)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {levelIndex}")); 
            //LEVEL SPAWN ETMEK İÇİN HARİKA
        }
    }
}