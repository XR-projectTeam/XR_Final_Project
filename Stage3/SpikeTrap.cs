using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float upHeight = -2;
    public float downHeight = -4;
    public float interval = 2f;
    public float moveSpeed = 2f;

    private bool isUp = false;
    private float timer = 0f;
    private Vector3 targetPos;

    void Start()
    {
        SetTarget(downHeight);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            isUp = !isUp;
            SetTarget(isUp ? upHeight : downHeight);
            timer = 0f;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    void SetTarget(float height)
    {
        Vector3 pos = transform.position;
        targetPos = new Vector3(pos.x, height, pos.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Hit!");
            // 데미지 처리 코드가 있다면 여기에 추가
        }
    }
}
