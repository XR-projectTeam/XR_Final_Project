using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
	public GameObject monsterPrefab;
	public float initialSpawnWait = 5f; // 트리거 후 소환까지 대기 시간

	private bool hasSpawned = false;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log($"[Trigger] 닿은 오브젝트: {other.name}, 태그: {other.tag}");

		if ((other.CompareTag("Player") || other.transform.root.CompareTag("Player")) && !hasSpawned)
		{
			Debug.Log("[Spawner] 플레이어 감지됨! 몬스터 소환 대기 시작");
			StartCoroutine(SpawnMonsterOnce());
		}
	}

	IEnumerator SpawnMonsterOnce()
	{
		hasSpawned = true;

		yield return new WaitForSeconds(initialSpawnWait);
		Debug.Log($"[{initialSpawnWait}초 후] 몬스터 인스턴스 생성 시도");

		if (monsterPrefab != null)
		{
			Instantiate(monsterPrefab, transform.position, Quaternion.identity);
			Debug.Log("몬스터 생성 완료!");
		}
		else
		{
			Debug.LogWarning("프리팹이 비어 있음!");
		}
	}
}
