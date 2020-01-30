using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int MAX_X = 8;
    private static readonly int MIN_X = -8;
    private static readonly int MAX_Y = 0;
    private static readonly int MIN_Y = -4;

    private Rigidbody2D rigidbody2D;
    private AudioSource audioSource;

    public float speed = 50;
    private Vector2 movement;
    public Transform cannon;
    public GameObject hitEffect;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MovePlayer();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoolManager.Instance.CreateBullet(cannon, false);
        }
    }

    private void MovePlayer()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement * (speed * Time.deltaTime);
        rigidbody2D.position = new Vector2(Mathf.Clamp(rigidbody2D.position.x, MIN_X, MAX_X), Mathf.Clamp(rigidbody2D.position.y, MIN_Y, MAX_Y));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid") || other.CompareTag("EnemyShip") || other.CompareTag("EnemyCaptain"))
        {
            GameManager.Instance.RemoveLife();
            ExplosionAnimation(other.gameObject);
        }
    }

    private void ExplosionAnimation(GameObject obj)
    {
        Instantiate(hitEffect, obj.transform.position, Quaternion.identity);
        Destroy(obj);
    }

    public void PlayerDeath()
    {
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        ExplosionAnimation(gameObject);
    }
}
