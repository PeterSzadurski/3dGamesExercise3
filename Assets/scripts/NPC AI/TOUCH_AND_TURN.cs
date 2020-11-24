using UnityEngine;

public class TOUCH_AND_TURN : MonoBehaviour
{
    [SerializeField]
    SmartObject _TargetSmartObj;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<enum_SmartObject>() != null)
        {
            if (other.GetComponent<enum_SmartObject>().GetSmartObject() == _TargetSmartObj)
            {
                gameObject.transform.Rotate(0, 90, 0);
            }
        }
    }

}
