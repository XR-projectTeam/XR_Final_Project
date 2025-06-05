using UnityEngine;

public class MonsterTriggerActivator : MonoBehaviour
{
	public MonsterSpawner3 spawner; // Inspector���� ����

	public float delayBeforeSpawn = 3f;

	private bool triggered = false;

	private void OnTriggerEnter(Collider other)
	{
		if (!triggered && other.CompareTag("Player"))
		{
			triggered = true;
			Debug.Log("[TriggerActivator] �÷��̾� ������, ���� ��ȯ ���");
			spawner.SpawnMonsterDelayed(delayBeforeSpawn);
		}
	}
}
