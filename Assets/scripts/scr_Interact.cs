using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Interact : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour _Script;

    public void Interact()
    {
        this._Script.enabled = true;
    }
}
