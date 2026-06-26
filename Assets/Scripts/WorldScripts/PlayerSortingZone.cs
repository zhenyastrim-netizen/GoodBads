using UnityEngine;

public class PlayerSortingZone : MonoBehaviour
{
    public int orderInside = 5;
    public int orderOutside = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        SpriteRenderer[] renderers = other.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in renderers)
        {
            sr.sortingOrder = orderInside;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        SpriteRenderer[] renderers = other.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in renderers)
        {
            sr.sortingOrder = orderOutside;
        }
    }
}