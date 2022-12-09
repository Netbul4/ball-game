using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ballgame
{
    public class GameController : MonoBehaviour
    {
        EnemyController[] enemies;
        [SerializeField] int sceneIndex;

        private void Start()
        {
            enemies = FindObjectsOfType<EnemyController>();

            foreach(EnemyController enemy in enemies)
            {
                enemy.KillPlayer += GameOver;
            }
        }

        public void NextLevel()
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDisable()
        {
            foreach(EnemyController enemy in enemies)
            {
                enemy.KillPlayer -= GameOver;
            }
        }
    }
}

