using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class monsterfollow2 : MonoBehaviour
{
	public int maxHealth = 100;
	private int currentHealth;

	public float knockbackForce = 30f;
	public float knockbackDuration = 0.3f;

	private Rigidbody rb;
	private NavMeshAgent agent;
	private Animator animator;
	private Collider col;

	private bool isDead = false;
	private bool isKnockedBack = false;
	private float knockbackTimer = 0f;

	void Start()
	{
		currentHealth = maxHealth;
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		col = GetComponent<Collider>();

		// Y축만 회전 허용
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	void Update()
	{
		if (isKnockedBack)
		{
			knockbackTimer += Time.deltaTime;

			if (knockbackTimer >= knockbackDuration)
			{
				isKnockedBack = false;

				if (!isDead && agent != null)
				{
					agent.enabled = true;

					GameObject player = GameObject.FindGameObjectWithTag("Player");
					if (player != null)
					{
						agent.SetDestination(player.transform.position);
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (isDead) return;

		// 즉사 태그에 닿으면 즉시 사망 처리
		if (other.CompareTag("DeadZone"))
		{
			Debug.Log("즉사 구역에 닿음!");
			Die();
			return;
		}

		// 기존 넉백 처리 (무기나 플레이어에 닿을 때)
		if (!isKnockedBack && other.CompareTag("Player"))
		{
			Debug.Log("무기 충돌 감지됨!");

			Vector3 knockbackDir = (transform.position - other.transform.position).normalized;
			knockbackDir.y = 0f;

			if (agent != null)
				agent.enabled = false;

			transform.position += Vector3.up * 0.1f; // 바닥 끼임 방지
			rb.velocity = Vector3.zero;
			rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);

			isKnockedBack = true;
			knockbackTimer = 0f;

			TakeDamage(10);
		}
	}

	void TakeDamage(int amount)
	{
		currentHealth -= amount;

		if (currentHealth <= 0 && !isDead)
		{
			Die();
		}
	}

	void Die()
	{
		isDead = true;
		Debug.Log("괴물 사망!");

		if (agent != null && agent.enabled && agent.isOnNavMesh)
		{
			agent.ResetPath();
			agent.enabled = false;
		}

		if (animator != null)
		{
			animator.applyRootMotion = true;
			animator.SetTrigger("Die");
		}

		StartCoroutine(HandleDeath());
	}

	IEnumerator HandleDeath()
	{
		// 넉백 작동 시간 확보
		yield return new WaitForSeconds(knockbackDuration);

		foreach (Collider c in GetComponentsInChildren<Collider>())
		{
			c.enabled = false;
		}

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.isKinematic = true;

		yield return new WaitForSeconds(1.5f); // 죽음 애니메이션 길이에 맞춰 조정
		Destroy(gameObject);
	}
}
