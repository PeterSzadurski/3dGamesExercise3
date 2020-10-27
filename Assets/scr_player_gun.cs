using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_gun : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Animator _Anim;
    private Transform _Spine;
    private Transform _Head;
    [SerializeField]
    private Transform _CamPoint;
    
    [SerializeField]
    private float _MinSpineY;
    [SerializeField]
    private float _MaxSpineY;

    [SerializeField]
    Transform _Laser;

    LineRenderer _lr;

    [SerializeField]
    private Transform _Gun;

    [SerializeField]
    private Vector3 _Offset;

    [SerializeField]
    private Camera _MainCam;

    private  List<Equipment.EquipEnum> _Equipment = new List<Equipment.EquipEnum>();
    void Start()
    {
        _Anim = this.GetComponent<Animator>();
        _Spine = _Anim.GetBoneTransform(HumanBodyBones.Spine);
        _Head = _Anim.GetBoneTransform(HumanBodyBones.Head);
        _lr = _Laser.GetComponent<LineRenderer>();
        _Equipment.Add(Equipment.EquipEnum.Pistol);
    }

    void LateUpdate()
    {
        float originalY = _Spine.localEulerAngles.y + 30;
        float dist = _MainCam.farClipPlane;
         Vector3 screenPoint = new Vector3(Screen.width / 2, Screen.height / 2, dist);

        Vector3 point = _MainCam.ScreenToWorldPoint(screenPoint);

        _Spine.LookAt(point);
        //_Head.LookAt(_CamPoint);


        // _Spine.rotation = Quaternion.Euler(_Spine.transform.rotation.eulerAngles.x, _Spine.transform.rotation.eulerAngles.y, 13);
        Vector3 newEulerAngles = _Spine.localEulerAngles;

        //newEulerAngles.y = originalY;
        //_Spine.localEulerAngles = newEulerAngles;

        // Vector3 newEulerAngles = _Spine.localEulerAngles;

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
        //   _Spine.localEulerAngles = newEulerAngles;

        // newEulerAngles.y = originalY;

        //_Spine.localEulerAngles = newEulerAngles;
        //_Gun.LookAt(_CamPoint, transform.up);
        _Spine.rotation = _Spine.rotation * Quaternion.Euler(_Offset);

        _lr.SetPosition(0, _Laser.position);
        RaycastHit hit;
        if (Physics.Raycast(_Laser.transform.position, _Laser.transform.forward, out hit))
        {
            if (hit.collider)
            {
                _lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            _lr.SetPosition(1, _Laser.forward * 5000);
        }

    }
}
