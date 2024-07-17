using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal abstract class Platform : MonoBehaviour, IPlatform
    {
        public Vector3 Position => transform.position;

        public void Init(Vector3 position)
        {
            transform.position = position;

            gameObject.SetActive(true);
        }

        public void Clear()
        {
            gameObject.SetActive(false);
        }
    }
}
