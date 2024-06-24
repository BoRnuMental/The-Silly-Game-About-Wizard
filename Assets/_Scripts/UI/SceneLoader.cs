using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    private float _delay = 1f;
    private FadeInOut _fade;

    [Inject] 
    private void Construct(FadeInOut fade)
    {
        _fade = fade;
    }
    public void LoadScene(string name)
    {
        _fade.FadeIn();
        StartCoroutine(WaitAndLoad(name));
    }

    private IEnumerator WaitAndLoad(string name)
    {
        yield return new WaitForSecondsRealtime(_delay);
        DOTween.KillAll();
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
