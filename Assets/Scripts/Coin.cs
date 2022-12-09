using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ballgame{
    public class Coin : MonoBehaviour
    {
      [SerializeField] float speed;
      [SerializeField] int earnScore;


      void Update()
      {
          transform.Rotate(0, 0, 1 * speed * Time.fixedDeltaTime);
      }

      void OnTriggerEnter(Collider other)
      {
        if(other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            other.GetComponent<BallController>().coins++;
            other.GetComponent<BallController>().UpdateCoins();
        }
      }
    }
}

