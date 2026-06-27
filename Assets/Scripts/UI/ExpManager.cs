using UnityEngine;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public RectTransform fill;
    public TMP_Text levelText;

    public void SetExperience(int currentExp, int expToNextLevel, int level)
    {
        float value = (float)currentExp / expToNextLevel;
        value = Mathf.Clamp01(value);

        fill.localScale = new Vector3(value, 1f, 1f);

        if (levelText != null)
            levelText.text = "Level " + level;
    }
}