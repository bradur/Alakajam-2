// Date   : 24.02.2018 15:39
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletCam : MonoBehaviour {

    [SerializeField]
    private Bullet bullet;

    private List<Enemy> enemies;

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
            if (percentageComplete >= 1f)
            {
                bullet.gameObject.SetActive(false);
                GameManager.main.SetCameraControlState(true);
                bulletOnTheMove = false;
                
                oldCamera.GetComponent<Cinemachine.CinemachineExternalCamera>().enabled = true;
                foreach (Enemy enemy in enemies)
                {
                    enemy.enableRagdoll();
                }
                Time.timeScale = 1f;
                GameManager.main.SetSecurityCameraControlState(true);
                GameManager.main.GetKills(numberOfHits);
                if (GameManager.main.GetNumberOfBullets() == 0)
                {
                    UIManager.main.ShowMessage("Out of bullets! Press R to restart.");
                }
            }
        }
    }

    private int numberOfHits = 0;

    public void StartCam(Vector3 start, Vector3 target, Vector3 direction, int numberOfHits, List<Enemy> enemies)
    {
        bullet.transform.localPosition = bulletStartingPosition;
        if (!bulletOnTheMove)
        {
            GameManager.main.SetSecurityCameraControlState(false);
            this.numberOfHits = numberOfHits;
            GameManager.main.SetCameraControlState(false);
            oldCamera.GetComponent<Camera>().enabled = false;
            oldCamera.GetComponent<Cinemachine.CinemachineExternalCamera>().enabled = false;

            bullet.transform.forward = direction;
            bullet.transform.position = start;
            bullet.gameObject.SetActive(true);
            startPosition = bullet.transform.position;
            targetPosition = target;
            bulletOnTheMove = true;
            this.enemies = enemies;
            
            lerpStartTime = Time.unscaledTime;
            Time.timeScale = 0f;
            Debug.Log("Old camera: " + oldCamera.GetComponent<Cinemachine.CinemachineExternalCamera>().enabled);
        }
    }
}
