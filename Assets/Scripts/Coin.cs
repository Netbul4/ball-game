using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ballgame{
    public class Coin : MonoBehaviour
    {
      [SerializeField] float speed; // Rotation speed for the coin mesh.
      [SerializeField] int earnScore; // Coin value;


      void Update()
      {
          transform.Rotate(0, 0, 1 * speed * Time.fixedDeltaTime); // Rotating the coin mesh.
      }

      void OnTriggerEnter(Collider other)
      {
        // If player collides with the coin, then we add to the earned Coins and destroy this Coin.
        if(other.CompareTag("Player"))
        {
            other.GetComponent<BallController>().coins++;
            other.GetComponent<BallController>().UpdateCoins();
            Destroy(this.gameObject);
        }
      }
    }
}

