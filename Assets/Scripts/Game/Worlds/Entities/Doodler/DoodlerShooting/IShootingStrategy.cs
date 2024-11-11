namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IShootingStrategy
    {
        public UnityEngine.Vector3 GetDirection(UnityEngine.Vector3 doodlerPosition, UnityEngine.Vector3 shootPosition);

        public NoseInfo GetNoseInfo(UnityEngine.Vector3 shootDirection, IDoodlerNose nose);
    }
}
