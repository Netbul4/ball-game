using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ballgame
{
    public class GameController : MonoBehaviour
    {
        BallController player; // Player's reference.
        EnemyController[] enemies; // Enemies reference.
        [SerializeField] int sceneIndex; // Index to change levels.

        private void Start()
        {
            player = GameObject.Find("ball").GetComponent<BallController>();
            enemies = FindObjectsOfType<EnemyController>();

            // Select all enemies and assign the Kill Player event.
            foreach(EnemyController enemy in enemies)
            {
                enemy.KillPlayer += GameOver;
            }

            player.Win += NextLevel; // Assign player's Win event.
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R)) GameOver(); // Restart the game if press 'R';
            if(Input.GetKeyDown(KeyCode.X)) Application.Quit(); // Quit the game if press 'X';
        }

        // Change level function.
        public void NextLevel()
        {
            SceneManager.LoadScene(sceneIndex);
        }

        // GameOver function.
        public void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene.
        }

        // OnDisable function for unsubscribe events.
        private void OnDisable()
        {
            foreach(EnemyController enemy in enemies)
            {
                enemy.KillPlayer -= GameOver;
            }

            player.Win -= NextLevel;
        }
    }
}

