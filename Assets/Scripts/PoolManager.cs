using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Pooler bulletPool;
    private Pooler enemyShipPool;
    private Pooler enemyCaptainPool;
    private Pooler asteroidPool;

    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        bulletPool = new Pooler(Resources.Load<GameObject>("Prefab/Bullet"), 40);
        enemyShipPool = new Pooler(Resources.Load<GameObject>("Prefab/EnemyShip"), 20);
        enemyCaptainPool = new Pooler(Resources.Load<GameObject>("Prefab/EnemyCaptain"), 10);

        List<GameObject> asteroidPrefabs = new List<GameObject>();
        for (int i = 1; i <= 6; i++)
        {
            asteroidPrefabs.Add(Resources.Load<GameObject>("Prefab/Asteroid" + i));
        }
        asteroidPool = new Pooler(asteroidPrefabs, 60);
    }

    public GameObject CreateBullet(Transform parent, bool enemy)
    {
        var obj = bulletPool.Get(parent.position, parent.rotation);
        obj.GetComponent<Bullet>().Shoot(enemy);
        return obj;
    }

    public void DestroyBullet(GameObject obj)
    {
        obj.GetComponent<TrailRenderer>().Clear();
        bulletPool.Free(obj);
    }

    public GameObject CreateEnemyShip()
    {
        return enemyShipPool.Get();
    }

    public void DestroyEnemyShip(GameObject obj)
    {
        enemyShipPool.Free(obj);
    }

    public GameObject CreateEnemyCaptain()
    {
        return enemyCaptainPool.Get();
    }

    public void DestroyEnemyCaptain(GameObject obj)
    {
        enemyCaptainPool.Free(obj);
    }

    public GameObject CreateAsteroid()
    {
        return asteroidPool.Get();
    }

    public void DestroyAsteroid(GameObject obj)
    {
        asteroidPool.Free(obj);
    }
}
