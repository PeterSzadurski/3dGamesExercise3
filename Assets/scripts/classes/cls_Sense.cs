using UnityEngine;
public class cls_Sense : MonoBehaviour
{
    public SmartObject _SmartObject = SmartObject.UNITY_CHAN;
    public float _DetectionRate = 1.0f;

    protected float _TimePassed = 0.0f;

    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }

    void Start() {
        _TimePassed = 0;
        Initialize();
    }
    void Update()
    {
        UpdateSense();
    }
}
