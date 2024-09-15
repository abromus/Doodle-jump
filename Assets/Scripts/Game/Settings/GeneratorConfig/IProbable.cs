namespace DoodleJump.Game.Settings
{
    internal interface IProbable
    {
        public float SpawnChance { get; }

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor);

        public void SetSpawnChance(float chance);
#endif
    }
}
