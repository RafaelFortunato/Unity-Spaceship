using DG.Tweening;
using UnityEngine;

public class EnemyCaptain : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private float shotDelay = 3;
    private float speed = 100;
    private int lives = 3;

    public Transform cannonL;
    public Transform cannonR;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        rigidbody2D.transform.position = new Vector2(Random.Range(-5f, 5f), 6f);
        spriteRenderer.color = Color.white;
        lives = 3;

        CancelInvoke();
        Invoke("FreeObj", 40f);
    }

    private void FreeObj()
    {
        PoolManager.Instance.DestroyEnemyCaptain(gameObject);
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        int x = player.transform.position.x > transform.position.x ? 1 : -1;
        rigidbody2D.velocity = new Vector2(x, -0.3f) * speed * Time.deltaTime;

        shotDelay -= Time.deltaTime;
        if (shotDelay <= 0)
        {
            PoolManager.Instance.CreateBullet(cannonL, true);
            PoolManager.Instance.CreateBullet(cannonR, true);
            shotDelay = Random.Range(1.5f, 3f);
        }
    }

    public bool RemoveLife()
    {
        spriteRenderer.DOColor(Color.red,.7f).SetEase(Ease.Flash, 8);

        lives--;
        return lives <= 0;

    }
}
