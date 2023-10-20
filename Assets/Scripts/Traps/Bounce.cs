using UnityEngine;

namespace Traps
{
	[RequireComponent(typeof(Rigidbody))]
	public class Bounce : MonoBehaviour
	{
		[SerializeField] private float force;
		[SerializeField] private float stunTime;

		void OnCollisionEnter(Collision collision)
		{
			var player = collision.gameObject;
			if (player.GetComponent<PlayerController>() != null)
			{
				player.GetComponent<PlayerController>().Stun(stunTime);
				player.GetComponent<Rigidbody>().AddForce(-collision.GetContact(0).normal * force);
			}

		}
	}
}
