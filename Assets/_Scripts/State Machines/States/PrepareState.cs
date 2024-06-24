using Zenject;

public class PrepareState : GameState
{
    private PrepareTimer _timer;
    private FadeInOut _fade;

    [Inject] 
    private void Construct(PrepareTimer timer, FadeInOut fade)
    {
        _timer = timer;
        _fade = fade;
    }

    public override void Enter()
    {
        _timer.gameObject.SetActive(true);
        _fade.gameObject.SetActive(true);
        _fade.FadeOut();
    }

    public override void Exit()
    {
        _timer.gameObject.SetActive(false);
    }
}
