using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotions
{
    Happy,
    Sad
}

[CreateAssetMenu(fileName = "New Emotion", menuName = "Emotions")]
public class Emotion : ScriptableObject
{
    public Emotions mainEmotion;

    public List<Emotions> oppositeEmotions;

    [SerializeField] private Sprite sprite;
}
