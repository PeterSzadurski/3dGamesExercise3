using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public enum AI_ACTIONS { DO_NOTHING, WANDER, CHASE };
public class cls_AI_Action : MonoBehaviour
{
    scr_npc_properties _NpcProps;

    [SerializeField]
    private NavMeshAgent _Agent;

    [SerializeField]
    private Transform _ChaseTransform;

    private Vector3 _TargetPosition;
    void Start()
    {
        _NpcProps = GetComponent<scr_npc_properties>();
        if (_NpcProps != null)
            GetNextPosition();
    }
    public void DoNothing() { }

    public void Wander()
    {

        if (Vector3.Distance(_TargetPosition, transform.position) <= _NpcProps.targetPositionTolerance)
        {
            GetNextPosition();
        }
        _Agent.SetDestination(_TargetPosition);
    }
    public void Chase() {

        _Agent.SetDestination(_ChaseTransform.position);
    }

    public void  GetNextPosition()
    {
        _TargetPosition = new Vector3(Random.Range(_NpcProps.minX, _NpcProps.maxX), transform.position.y,
            Random.Range(_NpcProps.minZ, _NpcProps.maxZ));
    }



}
