using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ballgame
{
    public class EnemyController : MonoBehaviour
    {
      [Header("Main Variables")]
      [SerializeField] private NavMeshAgent agent;
      [SerializeField] private float detectRadius;
      [SerializeField] private Transform corruptedBall;
      [SerializeField] private Transform player;
      [SerializeField] private float rotationSpeed;

      public delegate void Delegate();
      public event Delegate KillPlayer;

      private void Start()
      {
        agent = GetComponent<NavMeshAgent>();
      }

      private void Update()
      {
        corruptedBall.transform.Rotate(0, 0, rotationSpeed * 2 * Time.fixedDeltaTime);

        if(Vector3.Distance(player.position, transform.position) <= detectRadius)
        {
            agent.SetDestination(player.position);
        }
      }

      private void OnTriggerEnter(Collider other)
      {
        KillPlayer();
      }
    }
}

