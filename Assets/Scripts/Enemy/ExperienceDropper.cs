using UnityEngine;

public class ExperienceDropper : MonoBehaviour
{
    public GameObject experienceOrbPrefab;

    public int totalExperience = 25;
    public int expPerOrb = 5;

    public float scatterRadius = 0.5f;

    public void DropExperience()
    {
        if (experienceOrbPrefab == null)
        {
            Debug.LogError("Experience Orb Prefab не назначен!");
            return;
        }

        int orbCount = Mathf.CeilToInt((float)totalExperience / expPerOrb);

        for (int i = 0; i < orbCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * scatterRadius;

            GameObject orb = Instantiate(
                experienceOrbPrefab,
                (Vector2)transform.position + randomOffset,
                Quaternion.identity
            );

            ExperienceOrb experienceOrb = orb.GetComponent<ExperienceOrb>();

            if (experienceOrb != null)
                experienceOrb.expAmount = expPerOrb;
        }
    }
}