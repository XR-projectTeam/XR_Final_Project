using UnityEngine; // 반드시 필요
using UnityEngine.UI; // UI 슬라이더용 (만약 PlayerHealthUI.cs에서 필요하면)
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	public float knockbackForce = 0f;

	public string SceneName;

	private Rigidbody rb;
	private PlayerHealthUI healthUI;

	void Start()
	{
		currentHealth = maxHealth;
		rb = GetComponent<Rigidbody>();

		healthUI = FindObjectOfType<PlayerHealthUI>();
		if (healthUI != null)
			healthUI.UpdateHealthUI(currentHealth, maxHealth);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

		if (healthUI != null)
			healthUI.UpdateHealthUI(currentHealth, maxHealth);

		if (currentHealth <= 0)
		{
			Debug.Log("플레이어가 죽었습니다!");
			// 여기에 죽었을 때 추가 처리 가능
			SceneManager.LoadScene(SceneName);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Hazard"))
		{
			Debug.Log("위험 오브젝트에 닿음! 데미지 및 넉백 발생");

			Vector3 knockDir = (transform.position - other.transform.position).normalized;
			knockDir.y = 0.0f;

			if (rb != null)
			{
				rb.velocity = Vector3.zero;
				rb.AddForce(knockDir * knockbackForce, ForceMode.VelocityChange);
			}

			TakeDamage(20);
		}
	}
}
