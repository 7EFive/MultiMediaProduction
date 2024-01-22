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
        //displayed value charging limit
        int chgLmt = (int)hp.chargeLimit;
        //displayed value max of charging limit
        int chgLmtMax = (int)hp.chargeLimitMax;
        //displayed value main charg amaount
        int chg = (int)hp.charge;
        //displayed value main charg max amaount
        int chargMax = (int)hp.maxCharg;
        //initialized String with displayed values
        String.text = "Charging: |" + chg.ToString() + "/" + chargMax + "|\ncharging_limit: |_" + chgLmt.ToString()+ "/" + chgLmtMax+ "_|\nHealth: [" + hp.currentHealth.ToString() + "/" + hp.maxHealth.ToString()+"]";
    }

}