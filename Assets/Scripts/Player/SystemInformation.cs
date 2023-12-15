using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SystemInformation : MonoBehaviour
{

    public PlayerHealth hp;
    public Text String;
    public GameObject objectToSpawn; 
    public Transform playerTransform; 


    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<PlayerHealth>();
        QualitySettings.vSyncCount = 0;
        // Limit the framerate to 60
        Application.targetFrameRate = 60;

    }

    void Update()
    {
        int chgLmt = (int)hp.chargeLimit;
        int chgLmtMax = (int)hp.chargeLimitMax;
        int chg = (int)hp.charge;
        int chargMax = (int)hp.maxCharg;
        String.text = "Charging: |" + chg.ToString() + "/" + chargMax + "|\ncharging_limit: |_" + chgLmt.ToString()+ "/" + chgLmtMax+ "_|\nHealth: [" + hp.currentHealth.ToString() + "/" + hp.maxHealth.ToString()+"]";
    }

}