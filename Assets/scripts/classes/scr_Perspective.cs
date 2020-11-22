using UnityEngine;

public class scr_Perspective : cls_Sense
{
    [SerializeField]
    private float _FieldOfView = 45f;
    [SerializeField]
    private float _ViewDistance = 100f;

    private Vector3 _RayDirection;

    [SerializeField]
    private Transform _FollowTransform;


    protected override void Initialize()
    {
    }
    protected override void UpdateSense()
    {
        _TimePassed += Time.deltaTime;
        if (_TimePassed >= _DetectionRate)
        {
            DetectSmartObject();
        }
    }

    void DetectSmartObject()
    {
        RaycastHit hit;
        _RayDirection = _FollowTransform.position - transform.position;

        if ((Vector3.Angle(_RayDirection, transform.forward)) < _FieldOfView)
        {
            if (Physics.Raycast(transform.position, _RayDirection, out hit, _ViewDistance))
            {
                    Debug.Log("Hit Name: " + hit.transform.gameObject.name);

                enum_SmartObject smartObject = hit.collider.GetComponent<enum_SmartObject>();
                if (smartObject != null)
                {
                    Debug.Log("Smart Object Type: " + smartObject.GetSmartObject());
                    if (smartObject.GetSmartObject() == _SmartObject)
                    {
                        switch (_SmartObject)
                        {
                            case SmartObject.TRAFFIC_LIGHT:
                                if (hit.transform.gameObject.GetComponent<scr_Traffic_Light>().GetIsGreen())
                                {
                                    gameObject.GetComponentInParent<scr_moving_object>().enabled = true;
                                }
                                else
                                {
                                    gameObject.GetComponentInParent<scr_moving_object>().enabled = false;
                                }
                                break;
                            default: break;
                        }
                    }
                }
            }
        }

    }
    void OnDrawGizmos()
    {
        if (_FollowTransform == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, _FollowTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * _ViewDistance);

        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += _FieldOfView * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= _FieldOfView * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}
