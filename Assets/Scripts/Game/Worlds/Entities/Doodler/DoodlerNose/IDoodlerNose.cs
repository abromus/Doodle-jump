namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerNose
    {
        public UnityEngine.Vector3 RotationOffset { get; }

        public UnityEngine.Vector3 RotationPointPosition { get; }

        public float Height { get; }

        public void Init();
    }
}
