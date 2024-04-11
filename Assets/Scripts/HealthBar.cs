using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject fillAreaObject;
    Image fillArea;
    private void Start()
    {
        fillArea = fillAreaObject.GetComponentInChildren<Image>();
    }

    public void UpdateHealthBar(float currentHealth, float maxhealth)
    {
        healthBar.value = currentHealth / maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
        if (healthBar.value <= 0)
        {
            fillArea.enabled = false;
        }
    }
   
   
    
}
