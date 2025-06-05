using UnityEngine;
using UnityEngine.AI;

public class NavMeshMonster : MonoBehaviour
{
    [Header("추적 설정")]
    public Transform player;
    public float updatePathInterval = 0.1f;

    private NavMeshAgent agent;
    private float lastUpdateTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.speed = 25f;
            agent.angularSpeed = 120f;
            agent.stoppingDistance = 1.5f;
        }

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null || agent == null) return;

        if (Time.time - lastUpdateTime > updatePathInterval)
        {
            if (agent.enabled && agent.isOnNavMesh)
            {
                agent.SetDestination(player.position);
            }
            lastUpdateTime = Time.time;
        }
    }

    void OnDrawGizmos()
    {
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
            }
        }
    }
}
