using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TrashBase : MonoBehaviour
{
    public int id;
    public TrashData trashData;

    public float detectionRange = 5f;
    public float moveSpeed = 5f;
    public float fleeingTime = 3f;
    public float fleeDistance = 10f;
    public Transform player;
    private NavMeshAgent agent;
    private bool isFleeing = false;

    private void Awake()
    {
        trashData =  ConfigData.Instance.GetData(id);
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

    }

    void Update()
    {
        if (player == null || agent == null)
            return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && !isFleeing)
        {
            StartCoroutine(FleeFromPlayer());
        }
    }

    IEnumerator FleeFromPlayer()
    {
        isFleeing = true;
        float fleeTimer = 0f;

        while (fleeTimer < fleeingTime)
        {
            Vector3 fleePosition = FindFleePosition();
            if (fleePosition != Vector3.zero)
            {
                agent.SetDestination(fleePosition);
            }

            fleeTimer += Time.deltaTime;
            yield return null;
        }
        agent.ResetPath();
        isFleeing = false;
    }

    Vector3 FindFleePosition()
    {
        Vector3 directionFromPlayer = transform.position - player.position;
        directionFromPlayer.Normalize();
        Vector3 targetPosition = transform.position + directionFromPlayer * fleeDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, fleeDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        for (int i = 0; i < 8; i++)
        {
            float angle = Random.Range(-90f, 90f);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * directionFromPlayer;
            Vector3 newPosition = transform.position + direction * fleeDistance;
            if (NavMesh.SamplePosition(newPosition, out hit, fleeDistance, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fleeDistance);
    }
}