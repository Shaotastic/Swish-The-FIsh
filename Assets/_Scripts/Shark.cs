using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Shark : MonoBehaviour
{
    [SerializeField] Emotion m_Emotion;

    [SerializeField] float m_Size;

    [SerializeField] float m_SizeIncrement;

    [SerializeField] float m_DisappearTime;

    [Header("To display emotions")]
    [SerializeField] TextMeshPro m_TextMesh;

    [SerializeField] SpriteRenderer m_Sprite;

    [SerializeField] Quaternion rot;

    private void Awake()
    {
        m_Size = 1;

        GetAndSetEmotion();

        GameManager.Instance.OnScorePoint += AteRightFish;
        GameManager.Instance.OnErrorPoint += AteWrongFish;
        GameManager.Instance.OnGameReset += Instance_OnGameReset;

        rot = transform.rotation;
    }

    private void GetAndSetEmotion()
    {
        m_Emotion = EmotionManager.Instance.GetRandomEmotion();

        if (m_Emotion)
        {
            m_TextMesh.text = m_Emotion.name;
            m_Sprite.sprite = m_Emotion.sprite;
            StartCoroutine(DelayFadeOut());
        }
    }

    private void Instance_OnGameReset()
    {
        m_Size = 1;
        transform.DOScale(m_Size, 0);
        FadeEmotion(1, 0);
        GetAndSetEmotion();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        ClampTargetPosition(transform);
    }

    private void LateUpdate()
    {
        m_TextMesh.transform.rotation = rot;
    }

    public void EatFish(Fish fish)
    {
        if (EmotionManager.Instance.CompareEmotion(m_Emotion, fish.GetEmotion()))
            GameManager.Instance.ScorePoint();
        else
            GameManager.Instance.ErrorPoint();

        FishManager.Instance.RemoveFish(fish);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Fish"))
        {
            EatFish(collision.GetComponent<Fish>());
        }
    }

    IEnumerator DelayFadeOut()
    {        
        yield return new WaitForSeconds(m_DisappearTime);
        FadeEmotion(0, 1);
    }

    void FadeEmotion(float value, float time)
    {
        m_TextMesh.DOFade(value, time);
        m_Sprite.DOFade(value, time);
    }

    void AteRightFish()
    {
        if (m_Size < 3)
            m_Size += m_SizeIncrement;
        transform.DOScale(m_Size, 0.5f);
    }

    void AteWrongFish()
    {
        if (m_Size > 0.5f)
            m_Size -= m_SizeIncrement;
        transform.DOScale(m_Size, 0.5f);
    }

    void ClampTargetPosition(Transform target)
    {
        Vector3 clampPosition = Camera.main.WorldToViewportPoint(transform.position);

        clampPosition.x = Mathf.Clamp01(clampPosition.x);
        clampPosition.y = Mathf.Clamp01(clampPosition.y);

        target.position = Camera.main.ViewportToWorldPoint(clampPosition);
    }
}
