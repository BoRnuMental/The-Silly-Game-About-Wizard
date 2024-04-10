using UnityEngine;

public class InactiveZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out MagicBall magicBall))
        {
            magicBall.gameObject.SetActive(false);
        }
    }
}
