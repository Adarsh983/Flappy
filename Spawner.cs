using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float spawnTime;
    private float timer;
    public float Range;

    void Awake()
    {
        timer = 0f;    
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0f)
        {
            Instantiate(spawnObject, new Vector2(transform.position.x , Random.Range(transform.position.y - Range , transform.position.y + Range)) , Quaternion.identity);
            timer = spawnTime;
        }
        timer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = new Vector2(0f, Range);
        Gizmos.DrawLine((Vector2)transform.position - pos , (Vector2)transform.position + pos);
    }
}
