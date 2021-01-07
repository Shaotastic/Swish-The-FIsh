using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PositiveEmotion
{ 
    Happy,
}

public enum NegativeEmotion
{
    Sad,
}

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

    public Sprite sprite;
}
