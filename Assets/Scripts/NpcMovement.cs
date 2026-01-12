using UnityEngine;
using UnityEngine.AI;

public class NpcMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("Agent tuning")]
    [SerializeField] int avoidancePriority = 50;        // 0..99, lower = higher priority
    
    

    NavMeshAgent _agent;
    float _nextUpdateTime;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            _agent = gameObject.AddComponent<NavMeshAgent>();
        }

        // 2D setup used in your project
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        // Basic avoidance tuning (avoidance type can also be changed in the Inspector)
        _agent.avoidancePriority = Mathf.Clamp(avoidancePriority, 0, 99);

        if (target != null)
            _agent.SetDestination(target.position);
    }

    private void Update()
    {
        _agent.SetDestination(target.position);
    }
}
