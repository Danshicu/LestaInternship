using UnityEngine;
[RequireComponent(typeof(Collider))]

public class GroundCheckCollider : MonoBehaviour
{
  
   public bool OnGround
   {
      get;
      private set;
   }

   private void OnTriggerEnter(Collider other)
   {
      OnGround = true;
   }

   private void OnTriggerExit(Collider other)
   {
      OnGround = false;
   }

   private void OnTriggerStay(Collider other)
   {
      OnGround = true;
   }
}
