using UnityEngine;
public enum SmartObject { PLAYER, UNITY_CHAN, TRAFFIC_LIGHT, STOP_SIGN}
public class enum_SmartObject : MonoBehaviour {
    [SerializeField]
    private SmartObject _SmartObject;

    public SmartObject GetSmartObject()
    {
        return _SmartObject;
    }
}


