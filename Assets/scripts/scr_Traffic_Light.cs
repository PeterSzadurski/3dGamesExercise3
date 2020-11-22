using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Traffic_Light : MonoBehaviour
{
    private bool _IsGreen = false;
    private IEnumerator _Coroutine;

    [SerializeField]
    private float _LightTime = 3f;


    public bool GetIsGreen()
    {
        return _IsGreen;
    }

    private IEnumerator TrafficLightRoutine()
    {
        while (true)
        {
            // endless traffic light loop
            yield return new WaitForSeconds(_LightTime);
            _IsGreen = !_IsGreen;
        }
    }
    void Start()
    {
        Debug.Log("Traffic Start");
        StartCoroutine(TrafficLightRoutine());
    }
}
