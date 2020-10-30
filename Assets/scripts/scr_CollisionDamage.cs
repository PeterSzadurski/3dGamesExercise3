using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_CollisionDamage : MonoBehaviour
{
    [SerializeField]
    private float _CollisionDamage;

    public bool _Attack;
    void OnCollisionEnter(Collision col)
    {

        Enter(col.gameObject);
    }

    private void Enter(GameObject other)
    {
        if (other.GetComponent<scr_breakable>() != null && _Attack)
        {
            other.GetComponent<scr_breakable>().TakeDamage(_CollisionDamage);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Enter(col.gameObject);
    }
}
