using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void Restart()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Gameplay");
    }
}