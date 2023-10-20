using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Collider))]
    public class FinishLine : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                EventManager.PlayerEvents.CallOnPlayerWin();
            }
        }
    }
}
