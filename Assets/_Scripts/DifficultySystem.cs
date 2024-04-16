using System.Collections;
using UnityEngine;

public class DifficultySystem : MonoBehaviour
{
    [Header("Magic balls speed")]
    [SerializeField] private float _magicBallSpeed;
    [SerializeField] private float _speedRange;

    [Header("Magic balls count per minute")]
    [SerializeField] private float _countPerMinute;
    [SerializeField] private float _increaseDifficultyTime;

    [Header("Increase difficulty step")]
    [SerializeField] private float _increaseMagicBallSpeedStep;
    [SerializeField] private float _increaseCountPerMinuteStep;

    public float MagicBallSpeed => _magicBallSpeed;
    public float SpeedRange => _speedRange;
    public float CountPerMinute => _countPerMinute;

    private void Awake()
    {
        StartCoroutine(WaitForIncreaseDifficulty());
    }

    public void IncreaseDifficulty()
    {
        StopCoroutine(WaitForIncreaseDifficulty());
        AutoIncreaseDifficulty();
        StartCoroutine(WaitForIncreaseDifficulty());
    }

    private void AutoIncreaseDifficulty()
    {
        _countPerMinute += _increaseCountPerMinuteStep;
        _magicBallSpeed += _increaseMagicBallSpeedStep;
    }

    private IEnumerator WaitForIncreaseDifficulty()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(_increaseDifficultyTime);
            AutoIncreaseDifficulty();
            Debug.Log(
                $"Increase Difficulty:\n" +
                $"Speed: {_magicBallSpeed}\n" +
                $"Count per minute: {_countPerMinute}");
        }     
    }

}
