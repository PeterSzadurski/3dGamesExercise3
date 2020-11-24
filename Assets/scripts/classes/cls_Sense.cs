using UnityEngine;
public class cls_Sense : MonoBehaviour
{
    public SmartObject _SmartObject = SmartObject.UNITY_CHAN;
    public float _DetectionRate = 1.0f;

    protected float _TimePassed = 0.0f;

  //  [SerializeField]
   // protected cls_AI_Action _Ai_Action = new cls_AI_Action();

    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }
    protected virtual void DefaultAction() { }

    void Start() {
        _TimePassed = 0;
        Initialize();
    }
    void Update()
    {
        UpdateSense();
    }


}
