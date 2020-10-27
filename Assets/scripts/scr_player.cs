using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class scr_player : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator _Anim;
    [SerializeField]
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
    private Vector3 _CamPosDefault = new Vector3(0, 0.2f, -0.33f);
    private Vector3 _CamPosDown = new Vector3(0, 0.39f, -0.07f);
    private Vector3 _CamPosUp = new Vector3(0, 0.018f, -0.138f);

    private Vector3 _CamPos = new Vector3();

    private RaycastHit _Scanner;

    private Rigidbody _RB;

    [SerializeField]
    private UnityEngine.UI.Image _Crosshair;


    private float _AxisV;
    private float _AxisH;
    [SerializeField]
    private Transform _MainCam;


    // enum CamEnum {ThirdPerson, Left, Right }

    [SerializeField]
    private GameObject _maincinema;

    // Dictionary<CamEnum, CinemachineVirtualCameraBase> _Cameras = new Dictionary<CamEnum, CinemachineVirtualCameraBase>();


    // Player Equipment
    [SerializeField]
    private GameObject _KatanaHand;
    [SerializeField]
    private GameObject _KatanaHolster;
    [SerializeField]
    private GameObject _PistolHand;
    [SerializeField]
    private GameObject _PistolHolster;



    [SerializeField]
    private Transform _CamContainer;

    private List<Equipment.EquipEnum> _Equipment = new List<Equipment.EquipEnum>();
    private Equipment.EquipEnum _CurrentEquipment;

    private GameObject _CurrentHand = null;
    private GameObject _CurrentHolster = null;

    void Start()
    {
        _Anim = this.GetComponent<Animator>();
        _RB = this.GetComponent<Rigidbody>();

        _CurrentEquipment = Equipment.EquipEnum.Nothing;
        _Equipment.Add(Equipment.EquipEnum.Nothing);


        // _Cameras.Add(CamEnum.FirstPerson, GameObject.FindGameObjectWithTag("FirstPersonCam").GetComponent<Camera>());
        // _Cameras[CamEnum.FirstPerson].gameObject.SetActive(false);

        //_Cameras.Add(CamEnum.Left, GameObject.FindGameObjectWithTag("LeftCam").GetComponent<CinemachineVirtualCamera>());

        // _Cameras.Add(CamEnum.Right, GameObject.FindGameObjectWithTag("RightCam").GetComponent<CinemachineVirtualCamera>());

        // _Cameras.Add(CamEnum.ThirdPerson, GameObject.FindGameObjectWithTag("ThirdPersonCam").GetComponent<CinemachineVirtualCameraBase>());

        //   _VirtCamera = _Cameras[CamEnum.ThirdPerson];


        //_FirstPersonCam.gameObject.SetActive(false);
        // _MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();



    }

    // Update is called once per frame
    void Update()
    {


        // mouse Y clamp
        float mY = (Input.GetAxisRaw("Mouse Y")) * _MouseSensitivity;
        _XRot += mY;
        _XRot = Mathf.Clamp(_XRot, _MinRotX, _MaxRotX);

        _AxisV = (Input.GetAxisRaw("Vertical"));
        _AxisH = (Input.GetAxisRaw("Horizontal"));

        // setup the proper animations
        _Anim.SetInteger("AxisV", (int)_AxisV);
        _Anim.SetInteger("AxisH", (int)_AxisH);

        // player movement
        // this.gameObject.transform.Translate(new Vector3(h * Time.deltaTime, 0, v * Time.deltaTime));

        this.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * _MouseSensitivity, 0);

        // Rotate Camera Up/Down
        _MainCam.transform.rotation = Quaternion.Euler(-_XRot, _MainCam.transform.rotation.eulerAngles.y, _MainCam.transform.rotation.eulerAngles.z);
        _FirstPersonCam.transform.rotation = Quaternion.Euler(-_XRot, _MainCam.transform.rotation.eulerAngles.y, _MainCam.transform.rotation.eulerAngles.z);

        // if (_FirstPersonCam.tag == "ThirdPersonCam")
        //{
        float camPull = _XRot / _MaxRotX;

        if (camPull > 0)
        {
            _CamPos = Vector3.Lerp(_CamPosDefault, _CamPosUp, camPull);
        }
        else
        {
            _CamPos = Vector3.Lerp(_CamPosDefault, _CamPosDown, -camPull);
        }

        _MainCam.transform.localPosition = _CamPos;


        //}
        // Crosshair


        if (Physics.Raycast(_FirstPersonCam.transform.position, _FirstPersonCam.transform.forward, out _Scanner, 30f) && (_Scanner.transform.gameObject.tag == "Interactable"))
        {

            _Crosshair.color = Color.green;
            if (Input.GetButtonDown("Interact"))
            {
                _Scanner.transform.gameObject.GetComponent<scr_Interact>().Interact();
            }
        }
        else
        {
            _Crosshair.color = Color.white;

        }


        // equipment swap
        if (Input.GetButtonDown("Hotkey0"))
            SwitchEquipment(0);
        if (Input.GetButtonDown("Hotkey1"))
            SwitchEquipment(1);
        if (Input.GetButtonDown("Hotkey2"))
            SwitchEquipment(2);

    }
    void FixedUpdate()
    {
        // move player
        Vector3 move = new Vector3(_AxisH * Time.deltaTime, 0, _AxisV * Time.deltaTime);
        move = transform.TransformDirection(move);
        _RB.MovePosition(transform.position + move);
    }

    void SwitchEquipment(int i)
    {
        if (_Equipment.Contains((Equipment.EquipEnum)i))
        {
            switch (i)
            {
                case 0:
                    _KatanaHand.SetActive(false);
                    _PistolHand.SetActive(false);
                    this.GetComponent<scr_player_gun>().enabled = false;
                    this.GetComponent<scr_player_sword>().enabled = false;
                    if (_Equipment.Contains(Equipment.EquipEnum.Pistol))
                    {
                        _PistolHolster.SetActive(true);
                    }
                    if (_Equipment.Contains(Equipment.EquipEnum.Katana))
                    {
                        _KatanaHolster.SetActive(true);
                    }
                    break;
                case 1:
                    _KatanaHand.SetActive(true);
                    _KatanaHolster.SetActive(false);
                    this.GetComponent<scr_player_sword>().enabled = true;
                    if (_Equipment.Contains(Equipment.EquipEnum.Pistol))
                    {
                        _PistolHand.SetActive(false);
                        this.GetComponent<scr_player_gun>().enabled = false;
                        _PistolHolster.SetActive(true);
                    }
                    break;
                case 2:
                    _PistolHand.SetActive(true);
                    _PistolHolster.SetActive(false);
                    this.GetComponent<scr_player_gun>().enabled = true;
                    this.GetComponent<scr_player_sword>().enabled = false;
                    if (_Equipment.Contains(Equipment.EquipEnum.Katana))
                    {
                        _KatanaHand.SetActive(false);
                        _KatanaHolster.SetActive(true);
                    }
                    break;
            }
            _CurrentEquipment = (Equipment.EquipEnum)i;
            _Anim.SetInteger("Equipment", i);

        }
    }

    /*  void ChangeCamera(CamEnum cam)
      {
          _VirtCamera.Priority = 10;
          _VirtCamera = _Cameras[cam];
          _VirtCamera.Priority = 20;
      }*/

    /*void SwapPerspective()
    {
        _FirstPersonCam.gameObject.SetActive(!_FirstPersonCam.isActiveAndEnabled);
        _MainCam.gameObject.SetActive(!_MainCam.isActiveAndEnabled);
    }
    */

    public bool AddEquipment(Equipment.EquipEnum equip)
    {
        if ((!_Equipment.Contains(equip)))
        {
            switch (equip)
            {
                case Equipment.EquipEnum.Katana:
                    _KatanaHolster.SetActive(true);
                    break;
                case Equipment.EquipEnum.Pistol:
                    _PistolHolster.SetActive(true);
                    break;

            }
            _Equipment.Add(equip);
            return true;
        }
        return false;
    }
}



