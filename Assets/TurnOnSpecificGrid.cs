using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSpecificGrid : MonoBehaviour
{
    [SerializeField] GameObject dark;
    // Not a really usefull script
    void Start()
    {
        dark.SetActive(true);
    }

}
