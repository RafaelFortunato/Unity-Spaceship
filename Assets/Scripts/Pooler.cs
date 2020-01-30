using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class allows us to create multiple instances of a prefabs and reuse them.
/// It allows us to avoid the cost of destroying and creating objects.
/// </summary>
public class Pooler
{
    protected Stack<GameObject> m_FreeInstances = new Stack<GameObject>();
    protected List<GameObject> m_Original;

    public Pooler(GameObject original, int initialSize)
    {
        Init(new List<GameObject>{original}, initialSize);
    }

    public Pooler(List<GameObject> originals, int initialSize)
    {
        Init(originals, initialSize);
    }

    private void Init(List<GameObject> originals, int initialSize)
    {
        m_Original = originals;
        m_FreeInstances = new Stack<GameObject>(initialSize);

        for (int i = 0; i < initialSize; ++i)
        {
            GameObject obj = GetRandomOriginal();
            obj.SetActive(false);
            m_FreeInstances.Push(obj);
        }
    }

    private GameObject GetRandomOriginal()
    {
        return Object.Instantiate(m_Original.OrderBy(n => Random.value).FirstOrDefault());
    }

    public GameObject Get(Vector3 pos, Quaternion quat)
    {
        GameObject ret = Get();
        ret.transform.position = pos;
        ret.transform.rotation = quat;
        return ret;
    }

    public GameObject Get()
    {
        GameObject ret = m_FreeInstances.Count > 0 ? m_FreeInstances.Pop() : GetRandomOriginal();
        ret.SetActive(true);
        return ret;
    }

    public void Free(GameObject obj)
    {
        obj.transform.SetParent(null);
        obj.SetActive(false);
        m_FreeInstances.Push(obj);
    }
}