using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_gun : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator _Anim;
    private Transform _Spine;


    [SerializeField]
    private float _MinSpineY;
    [SerializeField]
    private float _MaxSpineY;

    [SerializeField]
    Transform _Laser;

    LineRenderer _lr;

    [SerializeField]
    private Vector3 _Offset;

    [SerializeField]
    private Camera _FirstPersonCam;

    [SerializeField]
    private ParticleSystem _MuzzleFlash;
    [SerializeField]
    private GameObject _MuzzleLight;

    [SerializeField]
    private Camera _ThirdPersonCam;

    private Vector3 _ThirdPersonDefault;

    [SerializeField]
    private Vector3 _Third_Person_Zoom = new Vector3(0.1f, 0, 0.12f);

    private bool _ShotFiring = false;

    [SerializeField]
    private float _TimeToAim = 0.3f;
    private float _AimTime = 0;
    private float _UnAimTime = 0;
    private bool _HasZoomed = false;

    [SerializeField]
    private GameObject _Bullet;
    [SerializeField]
    private Transform _ShootPoint;

    [SerializeField]
    private AudioClip _Sfx;
    [SerializeField]
    private AudioSource _Audio;
    void Start()
    {
        _Anim = this.GetComponent<Animator>();
        _Spine = _Anim.GetBoneTransform(HumanBodyBones.Spine);
        _lr = _Laser.GetComponent<LineRenderer>();
        _ThirdPersonDefault = _ThirdPersonCam.transform.localPosition;

    }

    void Update()
    {


        if (Input.GetButtonDown("Fire2"))
        {
            _HasZoomed = false;
            _UnAimTime = 0;
        }

        if (Input.GetButton("Fire2"))
        {

            if (_AimTime < _TimeToAim)
            {
                _AimTime += Time.deltaTime;
                _ThirdPersonCam.transform.localPosition = Vector3.Lerp(_ThirdPersonDefault, _Third_Person_Zoom, _AimTime / _TimeToAim);
            }
        }
        if (Input.GetButtonUp("Fire2"))
        {
            _HasZoomed = true;
        }

        if (_AimTime > 0 && _HasZoomed)
        {
            _AimTime -= Time.deltaTime;
            _UnAimTime += Time.deltaTime;
            _ThirdPersonCam.transform.localPosition = Vector3.Lerp(_Third_Person_Zoom, _ThirdPersonDefault, _UnAimTime / _TimeToAim);

        }


        if (Input.GetButtonDown("Fire1") && !_ShotFiring)
        {
            StartCoroutine(FireShot());
        }

    }

    private IEnumerator FireShot()
    {
        _ShotFiring = true;
        _MuzzleLight.SetActive(true);
        _MuzzleFlash.Play(true);
        _Audio.PlayOneShot(_Sfx);
        GameObject bullet = Instantiate(_Bullet, _ShootPoint);
        bullet.SetActive(true);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000);
        yield return new WaitForSeconds(0.1f);
        _ShotFiring = false;
        _MuzzleLight.SetActive(false);
        _MuzzleFlash.Stop(true);

        yield return null;
    }
    void LateUpdate()
    {
        float originalY = _Spine.localEulerAngles.y + 30;
        float dist = _FirstPersonCam.farClipPlane;
        Vector3 screenPoint = new Vector3(Screen.width / 2, Screen.height / 2, dist);

        Vector3 point = _FirstPersonCam.ScreenToWorldPoint(screenPoint);

        _Spine.LookAt(point);


        Vector3 newEulerAngles = _Spine.localEulerAngles;



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
