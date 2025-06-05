using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro; // 꼭 필요!

public class MonsterDamageReceiver : MonoBehaviour
{
	public int maxHealth = 100;
	private int currentHealth;

	public float knockbackForce = 30f;
	public float knockbackDuration = 0.3f;
	public AudioClip hitSound;        // 충돌 시 재생할 사운드
	public TextMeshProUGUI victoryText; // ← 이 줄을 추가해야 Inspector에 뜸


	private AudioSource audioSource; // 사운드 재생용
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

		// ❗ Y축만 회전 허용
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
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

					// 넉백 후 다시 플레이어 추적 재개
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
		if (isDead || isKnockedBack) return;

		if (other.CompareTag("Target")) // 무기나 플레이어에 "Player" 태그 필요
		{
			Debug.Log("무기 충돌 감지됨!");

			if (hitSound != null && audioSource != null)
			{
				audioSource.PlayOneShot(hitSound);
			}

			Vector3 knockbackDir = (transform.position - other.transform.position).normalized;
			knockbackDir.y = 0f;

			if (agent != null)
				agent.enabled = false;

			transform.position += Vector3.up * 0.1f; // 바닥 끼임 방지
			rb.velocity = Vector3.zero;
			rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);

			isKnockedBack = true;
			knockbackTimer = 0f;

			TakeDamage(100);


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
			animator.SetTrigger("Die"); // Animator에서 Die 트리거를 통해 상태 전이
		}

		StartCoroutine(HandleDeath());
	}

	IEnumerator HandleDeath()
	{
		// 넉백 작동 시간 확보
		yield return new WaitForSeconds(knockbackDuration);

		// 자식 포함 모든 Collider 비활성화
		foreach (Collider c in GetComponentsInChildren<Collider>())
		{
			c.enabled = false;
		}

		// 움직임 정지
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.isKinematic = true;

		// 애니메이션 재생 대기 후 제거
		yield return new WaitForSeconds(1.5f); // 죽음 애니메이션 길이에 맞춰 조정
		Destroy(gameObject);
	}
}
