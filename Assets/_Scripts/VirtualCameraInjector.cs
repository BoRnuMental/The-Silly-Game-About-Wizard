using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VirtualCameraInjector : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _camera.Follow = _player.transform;
    }
}
