using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance;

    [SerializeField] int m_StartingFish;
    [SerializeField] int m_FishCount;

    [SerializeField] List<Fish> m_FishType;

    [SerializeField] List<Fish> m_FishList;

    [SerializeField] Stack<int> m_FishIndexes;

    [SerializeField] float m_MinFishSpeed = 2;
    [SerializeField] float m_MaxFishSpeed = 8;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        m_FishIndexes = new Stack<int>(20);

        GameManager.Instance.OnGameStart += Instance_OnGameStart;
        GameManager.Instance.OnGameReset += Instance_OnGameReset;
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
    }

    private void Instance_OnGameOver()
    {
        CancelInvoke("SpawnFish");
    }

    private void Instance_OnGameReset()
    {
        for (int i = 0; i < m_FishList.Count; i++)
        {
            if (m_FishList[i])
            {
                Fish tempFish = m_FishList[i];
                //m_FishList.RemoveAt(i);
                Destroy(tempFish.gameObject);
            }
        }
        m_FishList.Clear();
        m_FishCount = 0;
        Instance_OnGameStart();
    }

    private void Instance_OnGameStart()
    {
        InvokeRepeating("SpawnFish", 0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_FishList = new List<Fish>();
    }

    public void SpawnFish()
    {
        //Selects a random fish from the list
        Fish temp = m_FishType[Random.Range(0, m_FishType.Count)];

        float x = Random.Range(0, 2) == 0 ? GameManager.Instance.AreaBoundary().xMin : GameManager.Instance.AreaBoundary().xMax;
        float y = Random.Range(GameManager.Instance.AreaBoundary().yMin, GameManager.Instance.AreaBoundary().yMax);

        temp.transform.position = new Vector2(x, y);
        temp.InitializeFish(EmotionManager.Instance.GetRandomEmotion(), RandomWaypoint(), Random.Range(m_MinFishSpeed, m_MaxFishSpeed));

        if (m_FishList.Count == m_StartingFish)
        {
            if (m_FishIndexes.Count == 0)
                return;

            if (m_FishList.Contains(m_FishList[m_FishIndexes.Peek()]))
            {
                temp.name = "Fish " + ++m_FishCount;
                m_FishList[m_FishIndexes.Pop()] = Instantiate(temp,transform);
            }
        }
        else
        {
            temp.name = "Fish " + ++m_FishCount;
            m_FishList.Add(Instantiate(temp, transform));
        }
    }

    public Vector3 RandomWaypoint()
    {
        Vector3 newPoint = new Vector3();

        newPoint.x = Random.Range(GameManager.Instance.AreaBoundary().xMin, GameManager.Instance.AreaBoundary().xMax);
        newPoint.y = Random.Range(GameManager.Instance.AreaBoundary().yMin, GameManager.Instance.AreaBoundary().yMax);

        return newPoint;
    }

    public void RemoveFish(Fish fish)
    {
        int index = m_FishList.IndexOf(fish);

        Destroy(fish.gameObject);
        
        m_FishIndexes.Push(index);
    }
}
