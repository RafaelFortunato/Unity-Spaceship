using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static float BULLET_SPEED = 500f;
    private static float BULLET_DURATION = 3f;

    private bool fromEnemy;
    private Rigidbody2D rigidbody2D;
    private AudioSource audioSource;

    public GameObject hitEffect;
    public GameObject muzzleFlash;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(bool enemy)
    {
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0;
        rigidbody2D.AddForce(transform.up * BULLET_SPEED);

        fromEnemy = enemy;

        Instantiate(muzzleFlash, transform.position - new Vector3(0, 0, 5), Quaternion.identity);

        audioSource.Play();

        CancelInvoke();
        Invoke("FreeObj", BULLET_DURATION);
    }

    private void FreeObj()
    {
        PoolManager.Instance.DestroyBullet(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletHit();
            PoolManager.Instance.DestroyBullet(other.gameObject);
        }

        if (fromEnemy)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHit();
            }
        }
        else
        {
            if (other.CompareTag("Asteroid"))
            {
                AddScore(10);
                PlayExplosionSound(other);
                PoolManager.Instance.DestroyAsteroid(other.gameObject);
            }
            else if (other.CompareTag("EnemyShip"))
            {
                AddScore(50);
                PlayExplosionSound(other);
                PoolManager.Instance.DestroyEnemyShip(other.gameObject);
            }
            else if (other.CompareTag("EnemyCaptain"))
            {
                var captain = other.GetComponent<EnemyCaptain>();
                if (captain.RemoveLife())
                {
                    other.GetComponent<AudioSource>()?.Play();

                    AddScore(200);
                    PlayExplosionSound(other);
                    PoolManager.Instance.DestroyEnemyCaptain(other.gameObject);
                }
                else
                {
                    BulletHit();
                }
            }
        }
    }

    private void PlayExplosionSound(Collider2D other)
    {
        var collisionSound = other.GetComponent<AudioSource>();
        if (collisionSound != null)
        {
            AudioSource.PlayClipAtPoint(collisionSound.clip, other.transform.position);
        }
    }

    private void AddScore(int score)
    {
        GameManager.Instance.AddScore(score);
        BulletHit();
    }

    private void PlayerHit()
    {
        GameManager.Instance.RemoveLife();
        BulletHit();
    }

    private void BulletHit()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        PoolManager.Instance.DestroyBullet(gameObject);
    }
}
