using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class scr_player : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator _Anim;
    private Camera _FirstPersonCam;

    private CinemachineVirtualCameraBase _VirtCamera;

    [SerializeField]
    private float _MouseSensitivity = 1;

    // Camera Clamps
    private float _MinRotX = -60;
    private float _MaxRotX = 60;

    // current x Rotation
    private float _XRot = 0;

    // Camera will dolly around player based off rotation
    private Vector3 _CamPosDefault = new Vector3(0, 2.5f, -4);
    private Vector3 _CamPosDown = new Vector3(0, 4.66f, -0.88f);
    private Vector3 _CamPosUp = new Vector3(0, -1.11f, -3.12f);

    private Vector3 _CamPos = new Vector3();

    private RaycastHit _Scanner;

    private Rigidbody _RB;

    [SerializeField]
    private UnityEngine.UI.Image _Crosshair;


    private float _AxisV;
    private float _AxisH;

    private Camera _MainCam;
    enum CamEnum {ThirdPerson, Left, Right }

    [SerializeField]
    private GameObject _maincinema;

    Dictionary<CamEnum, CinemachineVirtualCameraBase> _Cameras = new Dictionary<CamEnum, CinemachineVirtualCameraBase>();

    
    void Start()
    {
        _Anim = this.GetComponent<Animator>();
        _RB = this.GetComponent<Rigidbody>();

       // _Cameras.Add(CamEnum.FirstPerson, GameObject.FindGameObjectWithTag("FirstPersonCam").GetComponent<Camera>());
       // _Cameras[CamEnum.FirstPerson].gameObject.SetActive(false);

        _Cameras.Add(CamEnum.Left, GameObject.FindGameObjectWithTag("LeftCam").GetComponent<CinemachineVirtualCamera>());

        _Cameras.Add(CamEnum.Right, GameObject.FindGameObjectWithTag("RightCam").GetComponent<CinemachineVirtualCamera>());

        _Cameras.Add(CamEnum.ThirdPerson, GameObject.FindGameObjectWithTag("ThirdPersonCam").GetComponent<CinemachineVirtualCameraBase>());

        _VirtCamera = _Cameras[CamEnum.ThirdPerson];
      

        _FirstPersonCam = GameObject.FindGameObjectWithTag("FirstPersonCam").GetComponent<Camera>();
        _FirstPersonCam.gameObject.SetActive(false);
        _MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        // mouse Y clamp
        /*     float mY = (Input.GetAxisRaw("Mouse Y")) * _MouseSensitivity;
             _XRot += mY;
             _XRot = Mathf.Clamp(_XRot, _MinRotX, _MaxRotX);
        */
        _AxisV = (Input.GetAxisRaw("Vertical"));
        _AxisH = (Input.GetAxisRaw("Horizontal"));
        
        // setup the proper animations
        _Anim.SetInteger("AxisV", (int)_AxisV);
        _Anim.SetInteger("AxisH", (int)_AxisH);

        // player movement
        // this.gameObject.transform.Translate(new Vector3(h * Time.deltaTime, 0, v * Time.deltaTime));

        this.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * _MouseSensitivity, 0);
        
        // Rotate Camera Up/Down
        _FirstPersonCam.transform.rotation = Quaternion.Euler(-_XRot, _FirstPersonCam.transform.rotation.eulerAngles.y, _FirstPersonCam.transform.rotation.eulerAngles.z);

        if (_FirstPersonCam.tag == "ThirdPersonCam" && 1 == 2)
        {
            float camPull = _XRot / _MaxRotX;

            if (camPull > 0)
            {
                _CamPos = Vector3.Lerp(_CamPosDefault, _CamPosUp, camPull);
            }
            else
            {
                _CamPos = Vector3.Lerp(_CamPosDefault, _CamPosDown, -camPull);
            }

            _FirstPersonCam.transform.localPosition = _CamPos;


        }
        // Crosshair
        if (Physics.Raycast(transform.position, this.transform.forward, out _Scanner, 5f) && (_Scanner.transform.gameObject.tag == "Interactable"))
        {
            _Crosshair.color = Color.green;
            if (Input.GetButtonDown("Fire1"))
            {
                _Scanner.transform.gameObject.GetComponent<scr_door>().enabled = true;
            }
        }
        else
        {
            _Crosshair.color = Color.white;

        }

        // Camera Change
        if (Input.GetKeyDown(KeyCode.H))
            ChangeCamera(CamEnum.Left);
        if (Input.GetKeyDown(KeyCode.F))
            SwapPerspective();
        if (Input.GetKeyDown(KeyCode.G))
            ChangeCamera(CamEnum.ThirdPerson);
        if (Input.GetKeyDown(KeyCode.J))
            ChangeCamera(CamEnum.Right);
    }
    void FixedUpdate()
    {
        // move player
        Vector3 move = new Vector3(_AxisH * Time.deltaTime, 0, _AxisV * Time.deltaTime);
        move = transform.TransformDirection(move);
        _RB.MovePosition(transform.position + move);
    }

    void ChangeCamera(CamEnum cam)
    {
        /*_Cam.gameObject.SetActive(false);
        _Cam = _Cameras[cam];
        _Cam.gameObject.SetActive(true);
        */
        _VirtCamera.Priority = 10;
        _VirtCamera = _Cameras[cam];
        _VirtCamera.Priority = 20;
    }

    void SwapPerspective()
    {
        _FirstPersonCam.gameObject.SetActive(!_FirstPersonCam.isActiveAndEnabled);
        _MainCam.gameObject.SetActive(!_MainCam.isActiveAndEnabled);
    }
}



