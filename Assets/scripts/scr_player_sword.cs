using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_sword : MonoBehaviour
{
    private Animator _Anim;
    [SerializeField]
    private scr_CollisionDamage _CollisionDamage;
    void Start()
    {
        _Anim = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            _Anim.Play("Slash");
        }
        if (_Anim.GetCurrentAnimatorStateInfo(1).IsTag("Slashing"))
        {
            _CollisionDamage._Attack = true;
        }
        else
        {
            _CollisionDamage._Attack = false;
        }
    }
}
