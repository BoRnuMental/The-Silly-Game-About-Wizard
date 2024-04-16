using DG.Tweening;
using UnityEngine;
using Zenject;

public class PauseState : GameState
{
    private GameObject _pauseMenu;
    private GameObject _settings;
    private Player _player;


    [Inject]
    private void Construct(
        [Inject(Id = "PauseMenu")] GameObject pauseMenu, 
        [Inject(Id = "Settings")] GameObject settings,
        Player player)
    {
        _pauseMenu = pauseMenu;
        _settings = settings;
        _player = player;
    }
    public override void Enter()
    {
        _player.enabled = false;
        DOTween.TogglePauseAll();
        _pauseMenu.SetActive(true);
        _settings.SetActive(false);
        Time.timeScale = 0;
    }

    public override void Exit()
    {
        _player.enabled = true;   
        DOTween.TogglePauseAll();
        _pauseMenu.SetActive(false);
        _settings.SetActive(false);
        Time.timeScale = 1;      
    }
}
