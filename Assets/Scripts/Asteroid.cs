using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        rigidbody2D.velocity = Vector2.zero;

        rigidbody2D.transform.position = new Vector2(Random.Range(-5f, 5f), 6f);
        rigidbody2D.AddForce(new Vector2(Random.Range(-.3f, .3f), Random.Range(-.1f, -.3f)) * 300f);
        rigidbody2D.angularVelocity = Random.Range(-60f, 60f);

        CancelInvoke();
        Invoke("FreeObj", 15f);
    }

    private void FreeObj()
    {
        PoolManager.Instance.DestroyAsteroid(gameObject);
    }
}
