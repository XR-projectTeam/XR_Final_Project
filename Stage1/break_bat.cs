using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class break_bat : MonoBehaviour
{
	public float swingThreshold = 1.5f; // �ֵθ��ٰ� ������ �ӵ�
	private Vector3 lastPosition;
	private Vector3 velocity;

	void Update()
	{
		velocity = (transform.position - lastPosition) / Time.deltaTime;
		lastPosition = transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Target") && velocity.magnitude > swingThreshold)
		{
			Debug.Log("�ֵѷ��� Ÿ���� Ÿ��!");
			// 1. �׳� ����:
			Destroy(other.gameObject);

			// 2. �Ǵ� Rigidbody�� ������:
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.AddForce(velocity.normalized * 10f, ForceMode.Impulse);
			}
		}
	}
}
