using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSpecificGrid : MonoBehaviour
{
    [SerializeField] GameObject dark;
    [SerializeField] GameObject front;
    // Not a really usefull script
    void Start()
    {
        dark.SetActive(true);
        front.SetActive(true);
    }

}
