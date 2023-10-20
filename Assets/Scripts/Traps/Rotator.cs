using UnityEngine;

namespace Traps
{
	public class Rotator : MonoBehaviour, ITrap
	{
		[SerializeField] private float speed;

		void FixedUpdate()
		{
			TrapLogic();
		}

		public void TrapLogic()
		{
			transform.Rotate(0f, 0f, speed * Time.fixedDeltaTime, Space.Self);
		}
	}
}
