using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private HealthSystem health;
   
    void Update()
    {
        healthText.text = health.CurrentHealth.ToString();
    }
}
