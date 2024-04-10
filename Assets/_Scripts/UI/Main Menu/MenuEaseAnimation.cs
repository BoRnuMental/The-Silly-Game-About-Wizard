using DG.Tweening;
using UnityEngine;

public class MenuEaseAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    [SerializeField] private Ease _ease;

    private RectTransform _canvasRT;

    private void Awake()
    {
        _canvasRT = transform.parent.GetComponent<RectTransform>();
    }
    private void Start()
    {
        transform.localPosition = new Vector2(0f, _canvasRT.rect.height);
        DOTween.Sequence()
            .AppendInterval(_delay)
            .Append(transform.DOLocalMoveY(0f, _duration).SetEase(_ease));
    }
}
