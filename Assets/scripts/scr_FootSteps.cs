using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_FootSteps : MonoBehaviour
{
    [SerializeField]
    AudioClip[] _Steps;
    private AudioSource _Audio;
    void Start()
    {
        _Audio = GetComponent<AudioSource>();
    }

    private void Step()
    {
        _Audio.PlayOneShot(_Steps[Random.Range(0, _Steps.Length)]);
    }


}
