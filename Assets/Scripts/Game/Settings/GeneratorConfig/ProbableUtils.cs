namespace DoodleJump.Game.Settings
{
    internal static class ProbableUtils
    {
        internal static int GetConfigIndex(System.Collections.Generic.List<IProbable> probables, float spawnChanceFactor)
        {
            var spawnChance = UnityEngine.Random.value;
            var targetSpawnChance = spawnChance / spawnChanceFactor;
            var chances = new System.Collections.Generic.List<ChanceInfo>(8);

            for (int i = 0; i < probables.Count; i++)
            {
                var config = probables[i];
                var currentSpawnChance = config.SpawnChance;

                if (currentSpawnChance < targetSpawnChance)
                    continue;

                if (chances.Count == 0 || UnityEngine.Mathf.Approximately(currentSpawnChance, chances[0].SpawnChance))
                    chances.Add(new(i, currentSpawnChance));
                else
                    break;
            }

            if (0 < chances.Count)
            {
                var index = UnityEngine.Random.Range(0, chances.Count - 1);
                var info = chances[index];

                return info.Index;
            }
            else
            {
                return probables.Count - 1;
            }
        }
    }
}
