using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform shootPoint;   //발사 위치 
    public float shootInterval = 2f; //-> 설정 가능능

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            ShootBullet();
            timer = 0f;
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}
