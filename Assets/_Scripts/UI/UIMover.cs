using DG.Tweening;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField] Vector2 _endPosition;
    [SerializeField, Min(0f)] float _duration;
    [SerializeField, Min(0f)] float _delay; 
    [SerializeField] Ease _ease;

    private void Awake()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.DOAnchorPos(_endPosition, _duration).SetEase(_ease).SetDelay(_delay);
    }
}
