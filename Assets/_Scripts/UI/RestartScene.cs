using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class RestartScene : MonoBehaviour
{
    private FadeInOut _fade;

    [Inject]
    private void Construct(FadeInOut fade)
    {
        _fade = fade;
    }
    public void Restart()
    {
        _fade.FadeIn();
        StartCoroutine(WaitAndLoad());
    }

    public IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Gameplay");
    }
}