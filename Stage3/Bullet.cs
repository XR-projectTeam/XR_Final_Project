using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 360f;

    private Vector3 rotationAxis;

    void Start()
    {
        // 회전 축과 회전 속도 랜덤 설정
        rotationAxis = Random.onUnitSphere;
        rotationSpeed = Random.Range(180f, 720f);

        // 날라라가는 방향 
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        // 회전 적용
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("피 닳음");
            Destroy(gameObject); // 해당 총알 제거
        }
        //else
         else if (collision.gameObject.CompareTag("wall")) //->태그 설정 필요요
        {
            Destroy(gameObject); // 벽 오브젝트 닿으면 제거거
        }
    }
}
