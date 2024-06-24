using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private Image _fade;
    private float _fadeTime = 1f;

    private void Awake()
    {
        _fade = GetComponent<Image>();
    }
    public void FadeIn()
    {
        _fade.DOFade(1f, _fadeTime).SetUpdate(true);
    }

    public void FadeOut()
    {
        _fade.DOFade(0f, _fadeTime).SetUpdate(true);
    }
}
