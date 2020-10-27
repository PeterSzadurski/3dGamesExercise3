using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_sword : MonoBehaviour
{
    private Animator _Anim;
    void Start()
    {
        _Anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            _Anim.SetBool("slash", true);
        }
    }
}
