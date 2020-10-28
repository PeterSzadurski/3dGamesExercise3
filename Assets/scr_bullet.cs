using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveBullet());
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
    IEnumerator RemoveBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
