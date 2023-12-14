using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Spawn_watch : MonoBehaviour
{
    public GameObject player;
    PlayerHealth stats;
    public SpriteRenderer sprite;
    public Animator animte;
    public bool rise = false;
    public bool spin = false;
    private Vector2 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        stats = player.GetComponent<PlayerHealth>();
        sprite = GetComponent<SpriteRenderer>();
        animte = GetComponent<Animator>();
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (stats.cDJ)
        {
            
            if (stats.cDJA)
            {
                
                rise = true;
                animte.SetBool("rise", rise);
            }
            else
            {
                
                animte.SetBool("rise", rise);
            }
            if (stats.cDJA_end)
            {
                spin = true;
                animte.SetBool("spin", rise);
            }
            else
            {
                animte.SetBool("spin", rise);
            }
        }
        
        
    }
}
