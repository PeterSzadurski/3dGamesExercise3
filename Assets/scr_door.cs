using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_door : MonoBehaviour
{
    [SerializeField]
    private Vector3 _TargetRotation = new Vector3(0, 90, 0);
    [SerializeField]
    private float _TimeToOpen = 2f;
    private Vector3 _StartRotation = new Vector3(0, 0, 0);

    private GameObject _Hinge;
    private float _TimePassed = 0;

    void Start()
    {
        // _Hinge Empty Object needed to move rotation axis
        _Hinge = this.gameObject.transform.parent.gameObject;
        _StartRotation = _Hinge.transform.rotation.eulerAngles;
        _TargetRotation += _StartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // script is suppose to start disabled, rotates the door to the target rotation, speed determined by _TimeToOpen
        if ( _TimePassed < _TimeToOpen)
        {
            _TimePassed += Time.deltaTime;
            _Hinge.transform.rotation = Quaternion.Lerp(Quaternion.Euler(_StartRotation), Quaternion.Euler(_TargetRotation), _TimePassed / _TimeToOpen);
        }
        else
        {
            // resets the timer, swaps rotations around for closing
            Vector3 tmp = _TargetRotation;
            _TargetRotation = _StartRotation;
            _StartRotation = tmp;
            _TimePassed = 0;
            enabled = false;
        }
    }
}
