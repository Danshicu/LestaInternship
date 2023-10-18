using UnityEngine;

namespace Traps
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalTrap : MonoBehaviour, ITrap
    {
        public void TrapLogic()
        {
            //This type of traps is fully driven by physics
        }
    }
}