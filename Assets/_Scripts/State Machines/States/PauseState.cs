using DG.Tweening;
using UnityEngine;
using Zenject;

public class PauseState : GameState
{
    private GameObject _pauseMenu;

    [Inject]
    private void Construct([Inject(Id = "PauseMenu")] GameObject pauseMenu)
    {
        _pauseMenu = pauseMenu;
    }
    public override void Enter()
    {
        Debug.Log("Enter in Pause State");
        DOTween.TogglePauseAll();
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public override void Exit()
    {
        Debug.Log("Leave Pause State");
        DOTween.TogglePauseAll();
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;      
    }
}
