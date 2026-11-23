using UnityEngine;

public class RandomInstantiation : MonoBehaviour
{
    [Header ("Floor")]
    [SerializeField] private Collider floorCollider;
    [Header ("Prefabs")]

    [SerializeField] private int totalItemAmount;
    [SerializeField] private GameObject[] cleaningPrefabs;
    void Start()
    {
        Bounds floorBounds = floorCollider.bounds;
      


        for(int i=0; i < totalItemAmount; i++)
        {
        float randomX = Random.Range(floorBounds.min.x,floorBounds.max.x);
        float randomZ = Random.Range(floorBounds.min.z,floorBounds.max.z);
        Vector3 position = new Vector3(randomX,floorBounds.max.y+0.5f,randomZ);
        Instantiate(cleaningPrefabs[Random.Range(0,cleaningPrefabs.Length)],position,Quaternion.identity);  
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
