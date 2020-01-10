using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicWallManager : MonoBehaviour
{
 
    void OnTriggerEnter(Collider tmp)
    {
        if (tmp.gameObject.CompareTag("MainCamera"))
        {
            tmp.transform.GetComponent<camera_control>().Reset();
        }
    }
}
