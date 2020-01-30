using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private static readonly float SPEED_Y = 0.3f;

    private Rigidbody2D rigidbody2D;
    private float shotDelay = 3;
    private float amplitude;
    private float posX;

    public Transform cannon;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        posX = Random.Range(-7f, 7f);
        amplitude = Random.Range(1f, 3f);

        rigidbody2D.transform.position = new Vector2(posX, 6f);

        CancelInvoke();
        Invoke("FreeObj", 30f);
    }

    private void FreeObj()
    {
        PoolManager.Instance.DestroyEnemyShip(gameObject);
    }

    void Update()
    {
        var x = Mathf.Sin(Time.time * amplitude) + posX;
        var y = transform.position.y - (Time.deltaTime * SPEED_Y);
        transform.position = new Vector2(x, y);

        shotDelay -= Time.deltaTime;
        if (shotDelay <= 0)
        {
            PoolManager.Instance.CreateBullet(cannon, true);
            shotDelay = Random.Range(2f, 4f);
        }
    }
}
