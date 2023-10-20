using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Traps
{
    [RequireComponent(typeof(Collider))]
    public class WindTrap : MonoBehaviour, ITrap
    {
        [SerializeField] private float WindForce;
        [SerializeField] private float timeToChangeDirection;
        private List<Rigidbody> rigidBodyes = new List<Rigidbody>();
        private Vector3 windDirection;

        private void OnEnable()
        {
            StartCoroutine(ChangeDirection());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                rigidBodyes.Add(other.gameObject.GetComponent<Rigidbody>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                rigidBodyes.Remove(other.gameObject.GetComponent<Rigidbody>());
            }
        }
        
        private void FixedUpdate()
        {
            TrapLogic();
        }

        public void TrapLogic()
        {
            foreach (var rigidbody in rigidBodyes)
            {
                rigidbody.AddForce(windDirection*WindForce);
            }
        }

        private void GenerateRandomDirection()
        {
            var tempRotation = new Vector3(Random.Range(-100, 100), 0f, Random.Range(-100, 100));
            windDirection = tempRotation.normalized;
        }

        private IEnumerator ChangeDirection()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeToChangeDirection);
                GenerateRandomDirection();
            }
        }
        
    }
}
