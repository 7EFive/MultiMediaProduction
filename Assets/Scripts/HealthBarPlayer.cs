using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider healthBar;
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
        if (healthBar.value <= 0)
        {
            fillArea.enabled = false;
        }
    }
}
