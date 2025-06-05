using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class break_bat : MonoBehaviour
{
	public float swingThreshold = 1.5f; // 휘두른다고 간주할 속도
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
			Debug.Log("휘둘러서 타겟을 타격!");
			// 1. 그냥 제거:
			Destroy(other.gameObject);

			// 2. 또는 Rigidbody로 날리기:
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.AddForce(velocity.normalized * 10f, ForceMode.Impulse);
			}
		}
	}
}
