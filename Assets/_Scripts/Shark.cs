using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField] Emotion m_Emotion;

    [SerializeField] float m_Size;

    [SerializeField] bool m_ReadyToEat;

    [SerializeField] Fish m_CurrentFish;
    // Start is called before the first frame update
    void Start()
    {
        m_Size = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_ReadyToEat)
            EatFish();
    }

    public void EatFish()
    {
        if (!m_CurrentFish)
            return;

        //if (EmotionManager.Instance.CompareEmotion(m_Emotion, m_CurrentFish.GetEmotion()))
        //{
        //
        //}

        Destroy(m_CurrentFish.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            Debug.Log("Innn");
            m_ReadyToEat = true;
            m_CurrentFish = collision.GetComponent<Fish>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_ReadyToEat = false;
        m_CurrentFish = null;
    }
}
