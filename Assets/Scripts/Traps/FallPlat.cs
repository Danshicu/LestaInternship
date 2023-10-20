using System.Collections;
using UnityEngine;

namespace Traps
{
	[RequireComponent(typeof(Rigidbody))]
	public class FallPlat : MonoBehaviour, ITrap
	{
		[SerializeField] private float fallTime = 0.5f;
		private Rigidbody rigidbody;

		private void OnEnable()
		{
			rigidbody = GetComponent<Rigidbody>();
			rigidbody.isKinematic = true;
		}

		void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.GetComponent<PlayerController>() != null)
			{
				TrapLogic();
			}
		}

		IEnumerator Fall(float time)
		{
			yield return new WaitForSecondsRealtime(time);
			rigidbody.isKinematic = false;
			rigidbody.useGravity = true;
		}

		public void TrapLogic()
		{
			StartCoroutine(Fall(fallTime));
		}
	}
}
