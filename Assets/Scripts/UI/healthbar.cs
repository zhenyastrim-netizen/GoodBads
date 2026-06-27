using UnityEngine;

public class healthbar : MonoBehaviour
{
    public RectTransform fill;

    public void SetHealth(int currentHp, int maxHp)
    {
        float value = (float)currentHp / maxHp;
        value = Mathf.Clamp01(value);

        fill.localScale = new Vector3(value, 1f, 1f);
    }
}