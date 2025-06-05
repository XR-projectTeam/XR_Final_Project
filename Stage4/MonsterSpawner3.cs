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
			Debug.Log($"[Spawner4] 몬스터 생성 완료 at {transform.position}");
		}
		else
		{
			Debug.LogWarning("[Spawner4] 몬스터 프리팹이 비어 있음!");
		}
	}
}
