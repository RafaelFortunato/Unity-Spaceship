using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private float maxAsteroidDelay = 2, asteroidDelay = 2;
    private float maxEnemyShipDelay = 3, enemyShipDelay = 6;
    private float maxEnemyCaptainDelay = 10, enemyCaptainDelay = 20;

    void Update()
    {
        asteroidDelay -= Time.deltaTime;
        if (asteroidDelay <= 0)
        {
            CreateAsteroid();
            asteroidDelay = maxAsteroidDelay;
            maxAsteroidDelay = Mathf.Max(maxAsteroidDelay - 0.1f, 0.5f);
        }

        enemyShipDelay -= Time.deltaTime;
        if (enemyShipDelay <= 0)
        {
            CreateEnemyShip();
            maxEnemyShipDelay = Mathf.Max(maxEnemyShipDelay - 0.1f, 0.5f);
            enemyShipDelay = maxEnemyShipDelay;
        }

        enemyCaptainDelay -= Time.deltaTime;
        if (enemyCaptainDelay <= 0)
        {
            CreateEnemyCaptain();
            maxEnemyCaptainDelay = Mathf.Max(maxEnemyCaptainDelay - 0.2f, 1f);
            enemyCaptainDelay = maxEnemyCaptainDelay;
        }
    }

    void CreateAsteroid()
    {
        PoolManager.Instance.CreateAsteroid();
    }

    void CreateEnemyShip()
    {
        PoolManager.Instance.CreateEnemyShip();
    }

    void CreateEnemyCaptain()
    {
        PoolManager.Instance.CreateEnemyCaptain();
    }
}
