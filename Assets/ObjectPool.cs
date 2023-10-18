using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        if(pool.Count == 0)
        {
            GameObject obj = Instantiate(prefab, position, rotation);
            return obj;
        }

        GameObject pooledObject = pool.Dequeue();
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;
        pooledObject.SetActive(true);

        return pooledObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
