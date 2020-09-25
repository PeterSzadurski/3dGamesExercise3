using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator _Anim;
    [SerializeField]
    private Camera _Cam;

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

    [SerializeField]
    private UnityEngine.UI.Image _Crosshair;

    void Start()
    {
        _Anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int v, h;

        // mouse Y clamp
        float mY = (Input.GetAxisRaw("Mouse Y")) * _MouseSensitivity;
        _XRot += mY;
        _XRot = Mathf.Clamp(_XRot, _MinRotX, _MaxRotX);

        v = (int)(Input.GetAxisRaw("Vertical"));
        h = (int)(Input.GetAxisRaw("Horizontal"));

        // setup the proper animations
        _Anim.SetInteger("AxisV", v);
        _Anim.SetInteger("AxisH", h);

        // player movement
        this.gameObject.transform.Translate(new Vector3(h * Time.deltaTime, 0, v * Time.deltaTime));
        this.transform.Rotate(0, Input.GetAxisRaw("Mouse X") * _MouseSensitivity, 0);

        // Rotate Camera Up/Down
        _Cam.transform.rotation = Quaternion.Euler(-_XRot, _Cam.transform.rotation.eulerAngles.y, _Cam.transform.rotation.eulerAngles.z);

        float camPull = _XRot / _MaxRotX;

        if (camPull > 0)
        {
            _CamPos = Vector3.Lerp(_CamPosDefault, _CamPosUp, camPull);
        }
        else
        {
            _CamPos = Vector3.Lerp(_CamPosDefault, _CamPosDown, -camPull);
        }

        _Cam.transform.localPosition = _CamPos;


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
    }

}

