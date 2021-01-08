using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance;

    [SerializeField] int m_StartingFish;

    [SerializeField] List<Fish> m_FishList;

    [SerializeField] Rect m_SpawnBoundaries;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_FishList = new List<Fish>(m_StartingFish * 2);

        InvokeRepeating("SpawnFish", 0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFish()
    {
        GameObject temp = m_FishList[Random.Range(0, m_FishList.Count)].gameObject;

        float x = Random.Range(m_SpawnBoundaries.xMin, m_SpawnBoundaries.xMax);
        float y = Random.Range(m_SpawnBoundaries.yMin, m_SpawnBoundaries.yMax);
        temp.transform.position = new Vector2(x, y);
        
        Instantiate(temp);
    }
}
