using DG.Tweening;
using UnityEngine;
using Zenject;

public class PauseState : GameState
{
    private GameObject _pauseMenu;
    private GameObject _settings;


    [Inject]
    private void Construct(
        [Inject(Id = "PauseMenu")] GameObject pauseMenu, 
        [Inject(Id = "Settings")] GameObject settings)
    {
        _pauseMenu = pauseMenu;
        _settings = settings;
    }
    public override void Enter()
    {
        Debug.Log("Enter in Pause State");
        DOTween.TogglePauseAll();
        _pauseMenu.SetActive(true);
        _settings.SetActive(false);
        Time.timeScale = 0;
    }

    public override void Exit()
    {
        Debug.Log("Leave Pause State");
        DOTween.TogglePauseAll();
        _pauseMenu.SetActive(false);
        _settings.SetActive(false);
        Time.timeScale = 1;      
    }
}
