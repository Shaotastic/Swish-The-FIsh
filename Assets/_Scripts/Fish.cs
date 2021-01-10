using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fish : MonoBehaviour
{
    [SerializeField] Emotion m_Emotion;

    [SerializeField] SpriteRenderer m_SpriteRenderer;

    [SerializeField] Vector3 m_Direction;

    [SerializeField] Vector3 m_CurrentWaypoint;

    [SerializeField] float m_Speed = 2;

    [SerializeField] Transform m_Threat;

    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        m_Threat = GameObject.FindGameObjectWithTag("Player").transform;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, m_CurrentWaypoint) <= 2)
            m_CurrentWaypoint = FishManager.Instance.RandomWaypoint();

        if (Vector3.Distance(transform.position, m_Threat.position) <= 4)
        {
            m_Direction = (transform.position - m_Threat.position).normalized;
            transform.DOMove(transform.position + m_Direction * 2, 1);
        }
        else
            m_Direction = (m_CurrentWaypoint - transform.position).normalized;

        transform.position += m_Direction * Time.fixedDeltaTime * m_Speed;

        if(m_Direction.x > 0.1f)
            transform.DORotate(new Vector3(0,90,0), 0.3f);
        else
            transform.DORotate(new Vector3(0, -90, 0), 0.3f);
    }

    private void LateUpdate()
    {
        m_SpriteRenderer.transform.rotation = rot;
    }

    public void InitializeFish(Emotion emotion, Vector3 wayPoint, float speed)
    {
        m_Emotion = emotion;
        m_SpriteRenderer.sprite = emotion.sprite;
        m_CurrentWaypoint = wayPoint;
        m_Speed = speed;

    }    

    public Emotion GetEmotion()
    {
        return m_Emotion;
    }
}
