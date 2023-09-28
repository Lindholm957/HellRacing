using UnityEngine;

namespace Game.GamePlay
{
    public class StartPosition : MonoBehaviour
    {
        public Vector3 GetPosition() => gameObject.transform.position;
        public bool IsFree = true;
    }
}