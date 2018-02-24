// Date   : 24.02.2018 15:39
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;

public class BulletCam : MonoBehaviour {

    [SerializeField]
    private Bullet bullet;

    private float lerpStartTime;

    [SerializeField]
    private float duration = 1f;

    private bool bulletOnTheMove;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private Cinemachine.CinemachineExternalCamera oldCamera;

    private Vector3 bulletStartingPosition;

    void Start()
    {
        bulletStartingPosition = bullet.transform.localPosition;
    }

    void Update()
    {
        if (bulletOnTheMove)
        {
            float percentageComplete = (Time.unscaledTime - lerpStartTime) / duration;
            bullet.transform.position = Vector3.Lerp(startPosition, targetPosition, percentageComplete);
            Debug.Log(oldCamera.isActiveAndEnabled);
            if (percentageComplete >= 1f)
            {
                Debug.Log("already!");
                bullet.gameObject.SetActive(false);
                GameManager.main.SetCameraControlState(true);
                bulletOnTheMove = false;
                Time.timeScale = 1f;
                oldCamera.gameObject.SetActive(true);
                bullet.transform.localPosition = bulletStartingPosition;

            }
        }
    }

    public void StartCam(Vector3 target, Vector3 direction)
    {
        if (!bulletOnTheMove)
        {
            
            GameManager.main.SetCameraControlState(false);
            oldCamera.gameObject.SetActive(false);
            Debug.Log(oldCamera.isActiveAndEnabled);
            bullet.transform.forward = direction;
            bullet.transform.localPosition = Vector3.zero;
            bullet.gameObject.SetActive(true);
            startPosition = bullet.transform.position;
            targetPosition = target;
            bulletOnTheMove = true;
            
            lerpStartTime = Time.unscaledTime;
            Time.timeScale = 0f;
        }
    }
}