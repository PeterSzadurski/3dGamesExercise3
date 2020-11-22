using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_carAI : MonoBehaviour
{
    private float _SwitchTime = 3f;
    private float _CurrentTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // moves object forward for a specified time
        this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
        _CurrentTime += Time.deltaTime;
        if (_CurrentTime >= _SwitchTime)
        {
            // object is turned around, time reset
            this.gameObject.transform.Rotate(0, 180, 0);
            _CurrentTime = 0;
        }
    }
}
