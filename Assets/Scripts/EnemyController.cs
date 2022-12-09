using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ballgame
{
    public class EnemyController : MonoBehaviour
    {
      [Header("Main Variables")]
      [SerializeField] private NavMeshAgent agent; // AI Agent.
      [SerializeField] private float detectRadius; // radius for Player's detection.
      [SerializeField] private Transform corruptedBall; // Transform component for the enemy's mesh.
      [SerializeField] private Transform player; // Player's Transform component.
      [SerializeField] private float rotationSpeed; // The rotation speed for the mesh.

      // Delegate event for GameOver.
      public delegate void Delegate();
      public event Delegate KillPlayer;

      private void Start()
      {
        agent = GetComponent<NavMeshAgent>();
      }

      private void Update()
      {
        corruptedBall.transform.Rotate(0, 0, rotationSpeed * 2 * Time.fixedDeltaTime); // Rotating enemy's mesh.

        // If player's transform is near to the Enemy, then pursues Player.
        if(Vector3.Distance(player.position, transform.position) <= detectRadius)
        {
            agent.SetDestination(player.position);
        }
      }

      private void OnTriggerEnter(Collider other)
      {
        if(other.tag == "Player") KillPlayer(); // If Enemy touch Player, then call KillPlayer event.
      }
    }
}

