using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoUlt : MonoBehaviour
{
    PlayerHealth playerStats;
    PlayerMain player;
    // Start is called before the first frame update
    private void Start()
    {
        playerStats = gameObject.GetComponent<PlayerHealth>();
        player = gameObject.GetComponent<PlayerMain>();
    }
    private void Update()
    {
        if (!player.older && playerStats.charge>50)
        {
            playerStats.charge = 50;
        }
       
    }
}
