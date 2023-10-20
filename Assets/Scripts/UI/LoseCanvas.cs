using System;
using Tools;
using UnityEngine;

namespace UI
{
    public class LoseCanvas : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.PlayerEvents.OnPlayerLose += ChangeVisibility;
            ChangeVisibility();
        }

        private void ChangeVisibility()
        {
            gameObject.GetComponent<Canvas>().enabled = !gameObject.GetComponent<Canvas>().enabled;
        }

        public void Restart()
        {
            LevelManager.ReloadCurrentLevel();
        }

        private void OnDisable()
        {
            EventManager.PlayerEvents.OnPlayerLose -= ChangeVisibility;
        }
    }
}