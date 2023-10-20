using System;
using UnityEngine;

namespace Player
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int startHealth;
        public int CurrentHealth;

        private void Start()
        {
            CurrentHealth = startHealth;
        }

        public void Damage(int damage)
        {
            CurrentHealth -= damage;
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                EventManager.PlayerEvents.CallOnPlayerLose();
            }
        }
    }
}