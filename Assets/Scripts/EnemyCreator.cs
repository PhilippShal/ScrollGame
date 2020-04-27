using System;
using System.Collections;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyCreator : MonoBehaviour
    {
        public Enemy EnemyToCreate;
        private Camera mainCamera;
        private Vector2 screenBounds;

        private void RefreshBounds()
        {
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }

        private IEnumerator SpawnEnemyOnTopEdge(float xShift, float delayTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(delayTime);
                var instance = Instantiate(EnemyToCreate, new Vector3(screenBounds.x * xShift, screenBounds.y, -9f), Quaternion.identity);
                // var newEnemy = instance.gameObject.GetComponent<Enemy>();                
            }
        }

        private void Start()
        {
            mainCamera = GameObject.Find(Constants.MainCameraName).GetComponent<Camera>();
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            StartCoroutine(SpawnEnemyOnTopEdge(0.7f, 2f));
            StartCoroutine(SpawnEnemyOnTopEdge(-0.7f, 2f));
        }

        private void Update()
        {
            RefreshBounds();
        }
    }
}
