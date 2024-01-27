using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch_Rotate : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(0, 50 * Time.deltaTime, 0, Space.Self);
    }
}
