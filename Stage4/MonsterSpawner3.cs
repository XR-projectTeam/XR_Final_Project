using UnityEngine;
using System.Collections;

public class MonsterSpawner3 : MonoBehaviour
{
	public GameObject monsterPrefab;

	public void SpawnMonsterDelayed(float delay)
	{
		StartCoroutine(SpawnAfterDelay(delay));
	}

	private IEnumerator SpawnAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);

		if (monsterPrefab != null)
		{
			Instantiate(monsterPrefab, transform.position, transform.rotation);
			Debug.Log($"[Spawner4] ���� ���� �Ϸ� at {transform.position}");
		}
		else
		{
			Debug.LogWarning("[Spawner4] ���� �������� ��� ����!");
		}
	}
}
