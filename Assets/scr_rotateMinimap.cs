using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_rotateMinimap : MonoBehaviour
{
    [SerializeField]
    private GameObject _MapUi;

    private Transform _Player;
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 compassAngle = new Vector3();
        compassAngle.z = _Player.transform.eulerAngles.y;
        _MapUi.transform.eulerAngles = compassAngle;
    }
}
