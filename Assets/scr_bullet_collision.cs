using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bullet_collision : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
    }
}
