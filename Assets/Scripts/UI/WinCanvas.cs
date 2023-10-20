using System;
using TMPro;
using Tools;
using UnityEngine;

namespace UI
{
    public class WinCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeTextField;

        private void OnEnable()
        {
            EventManager.GameStateEvents.OnTimerStop += SetTime;
            EventManager.PlayerEvents.OnPlayerWin += ChangeVisibility;
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

        private void SetTime(float time)
        {
            var seconds = Mathf.FloorToInt(time);
            var miliseconds = Mathf.FloorToInt((time-seconds)*1000);
            timeTextField.text = String.Format("{0}.{1:000} секунд", seconds, miliseconds);
        }
        
        private void OnDisable()
        {
            EventManager.PlayerEvents.OnPlayerWin -= ChangeVisibility;
            EventManager.GameStateEvents.OnTimerStop -= SetTime;
        }
    }
}