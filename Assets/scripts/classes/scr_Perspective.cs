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

    [SerializeField]
    private AI_ACTIONS _InitialAction;

    private AiAction aiAction;

    cls_AI_Action cls_AI_Action;

    protected override void Initialize()
    {
        cls_AI_Action = GetComponentInParent<cls_AI_Action>();
        ResetAction();
    }

    private void ResetAction()
    {
        switch (_InitialAction)
        {
            case AI_ACTIONS.DO_NOTHING:
                aiAction = cls_AI_Action.DoNothing;
                break;
            case AI_ACTIONS.WANDER:
                aiAction = cls_AI_Action.Wander;
                break;
            case AI_ACTIONS.CHASE:
                cls_AI_Action.GetNextPosition();
                aiAction = cls_AI_Action.Chase;
                break;
            default:
                aiAction = cls_AI_Action.DoNothing;
                break;

        }
        Debug.Log("reset");
    }
    protected override void UpdateSense()
    {
        _TimePassed += Time.deltaTime;

        DetectSmartObject();

        aiAction();
    }

    void DetectSmartObject()
    {
        RaycastHit hit;
        _RayDirection = _FollowTransform.position - transform.position;

        if ((Vector3.Angle(_RayDirection, transform.forward)) < _FieldOfView)
        {
            if (Physics.Raycast(transform.position, _RayDirection, out hit, _ViewDistance))
            {
                enum_SmartObject smartObject = hit.collider.GetComponent<enum_SmartObject>();
                Debug.Log(gameObject.name + ": " + hit.collider.gameObject.name);
                if (smartObject != null)
                {

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
                            case SmartObject.PLAYER:
                                Debug.Log("Player");
                                aiAction = cls_AI_Action.Chase;
                                break;
                            default:

                                ;
                                break;
                        }
                    }
                }
                else
                {
                    ResetAction();
                }
            }
            else
            {
                ResetAction();
            }
        }
        else
        {
            ResetAction();
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

    private delegate void AiAction();



}
