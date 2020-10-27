using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sr_weapon_look : MonoBehaviour
{
    [SerializeField]
    private Transform _Spine;
    [SerializeField]
    private float _MaxSpineY = 300f;
    [SerializeField]
    private float _MinSpineY = 60f;
    [SerializeField]
    private float _OffsetYAngle = 20.0f;

    [SerializeField]
    private Camera _Cam;
    [SerializeField]
    private Transform _CamPointer;

    private Animator anim;
    // Update is called once per frame
    void Start()
    {
    }
    void LateUpdate()
    {

        /*   float originalX = _Spine.localEulerAngles.x;

           Vector2 mouse = Input.mousePosition;
           float dist = _Cam.farClipPlane;

           Vector3 screenPoint = new Vector3(mouse.x, mouse.y, dist);

           Vector3 point = _Cam.ScreenToWorldPoint(screenPoint);

           _Spine.LookAt(point, Vector3.up);


           Vector3 newEulerAngles = _Spine.localEulerAngles;
            newEulerAngles.y += _OffsetYAngle;

            bool underMinY = newEulerAngles.y >= _MaxSpineY;
            bool overMaxY = newEulerAngles.y <= _MinSpineY;

            float absDistMax = Mathf.Abs(newEulerAngles.y - _MaxSpineY);
            float absDistMin = Mathf.Abs(newEulerAngles.y + _MinSpineY);

            if (underMinY && overMaxY)
            {
                if (absDistMax < absDistMin)
                    newEulerAngles.y = _MaxSpineY;
                else
                    newEulerAngles.y = _MinSpineY;
            }
            newEulerAngles.x = originalX;


           //newEulerAngles.y = _Cam.transform.localEulerAngles.y;
           //newEulerAngles.z = _Cam.transform.eulerAngles.z;
          // newEulerAngles.z = -_Cam.transform.eulerAngles.x;
           // update spine
         //  _Spine.localEulerAngles = newEulerAngles;
        */
        _Spine.LookAt(_CamPointer.position);
       // chest.LookAt(_CamPointer.position);

    }
}
