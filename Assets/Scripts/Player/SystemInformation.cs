using UnityEngine;
using UnityEngine.UI;

public class SystemInformation : MonoBehaviour
{

    public PlayerHealth hp;

    public Text String;
    


    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<PlayerHealth>();
        
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