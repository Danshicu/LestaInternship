using System;
using UnityEngine;

namespace Tools
{
    public class TimeManager : MonoBehaviour
    {
        private void OnEnable()
        {
            Time.timeScale = 1f;
            EventManager.PlayerEvents.OnPlayerLose += StopTime;
            EventManager.PlayerEvents.OnPlayerWin += StopTime;
        }

        private void StopTime()
        {
            Time.timeScale = 0f;
        }
        
        private void OnDisable()
        {
            EventManager.PlayerEvents.OnPlayerLose -= StopTime;
            EventManager.PlayerEvents.OnPlayerWin -= StopTime;
        }
        
    }
}