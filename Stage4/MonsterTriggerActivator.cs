using UnityEngine;

public class MonsterTriggerActivator : MonoBehaviour
{
	public MonsterSpawner3 spawner; // Inspector에서 연결

	public float delayBeforeSpawn = 3f;

	private bool triggered = false;

	private void OnTriggerEnter(Collider other)
	{
		if (!triggered && other.CompareTag("Player"))
		{
			triggered = true;
			Debug.Log("[TriggerActivator] 플레이어 감지됨, 몬스터 소환 명령");
			spawner.SpawnMonsterDelayed(delayBeforeSpawn);
		}
	}
}
