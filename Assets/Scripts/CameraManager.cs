using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CameraManager : SingletonPersistent<CameraManager>
{
    [SerializeField] CinemachineVirtualCamera _playerCamera;
    [SerializeField] CinemachineVirtualCamera _plantCamera;

    private bool playerFocus = true;
    public void SwitchToPlant(Transform transform)
    {
        if (playerFocus)
        {
            _playerCamera.Priority = 0;
            _plantCamera.Priority = 1;
            _plantCamera.Follow = transform;
            playerFocus = false;
            StartCoroutine(SlightReturnToCamera());
        }
    }
    private IEnumerator SlightReturnToCamera()
    {
        yield return new WaitForSeconds(2f);
        SwitchToPlayer();
    }

    public void SwitchToPlayer()
    {
        if (!playerFocus)
        {
            _playerCamera.Priority = 1;
            _plantCamera.Priority = 0;
            playerFocus = true;
        }
    }

}
