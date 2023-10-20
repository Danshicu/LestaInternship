using System.Collections;
using Player;
using UnityEngine;


namespace Traps
{
    [RequireComponent(typeof(Collider))]
    public class ExplosiveTrap : MonoBehaviour, ITrap
    {
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inActiveColor;
        [SerializeField] private Color activationColor;
        [SerializeField] private float toExploseTime;
        [SerializeField] private float refreshTime;
        [SerializeField] private float explosiveTime;
        [SerializeField] private int damage;
        [SerializeField] private float explosiveRadius;
        private bool isActive = true;
        private Renderer renderer;

        private void OnEnable()
        {
            renderer = GetComponent<Renderer>();
            renderer.material.color = activeColor;
        }

        private IEnumerator OnCollisionEnter(Collision other)
        {
            if (isActive)
            {
                if (other.gameObject.GetComponent<PlayerController>() != null)
                {
                    renderer.material.color = activationColor;
                    yield return new WaitForSeconds(toExploseTime);
                    TrapLogic();
                }
            }
        }

        private IEnumerator ReActivate()
        {
            yield return new WaitForSeconds(refreshTime);
            isActive = true;
            renderer.material.color = activeColor;
        }

        private IEnumerator Explose()
        {
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(explosiveTime);
            var hits = Physics.OverlapSphere(transform.position, explosiveRadius);
            foreach (var hittedObject in hits)
            {
                var healthSystem = hittedObject.gameObject.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.Damage(damage);
                }
            }

            renderer.material.color = inActiveColor;
            StartCoroutine(ReActivate());
        }
        
        public void TrapLogic()
        {
            StartCoroutine(Explose());
            
        }
    }
}