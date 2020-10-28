using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bullet : MonoBehaviour
{
    private Rigidbody _Rb;
    [SerializeField]
    private float _RemoveTime = 15;
    // Start is called before the first frame update
    void Start()
    {
        _Rb = GetComponent<Rigidbody>(); 
        StartCoroutine(RemoveBullet());
    }

    void OnCollisionEnter(Collision col)
    {
        // stop all force
        _Rb.isKinematic = true;
        StopCoroutine(RemoveBullet());
        _RemoveTime = 0.1f;
        StartCoroutine(RemoveBullet());


    }
    IEnumerator RemoveBullet()
    {
        yield return new WaitForSeconds(_RemoveTime);
        Destroy(gameObject);
    }


}
