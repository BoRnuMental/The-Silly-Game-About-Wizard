using UnityEngine;

public class DisablePopup : MonoBehaviour
{
    public void Disable()
    {
        transform.parent.gameObject.SetActive(false);
    }
}