using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ballgame{
    public class BallController : MonoBehaviour
    {
        [Header("Moving Section")]
        [Range(0, 500)]
        [SerializeField] private float speed; // The speed of the ball when moves.
        [SerializeField] Vector3 jumpVector; // Axis of the ball when jumps.
        private Rigidbody rb;

        [Header("Jump Section")]
        [SerializeField] private LayerMask groundLayer; // Layer of the ground.
        [Range(0, 5)]
        [SerializeField] private float groundRadius; // The radius of ground check detection.
        [Range(0, 500)]
        [SerializeField] private float jumpModifier; // The strength modifier for the jump.
        [SerializeField] private bool grounded; // Check if we are on ground.
        

        [Header("Jetpack Section")]
        [SerializeField] private bool jetpack; // Check if we are not in ground and we could launch jetpack.
        [Range(0, 5000)]
        [SerializeField] private float jetForce; // The impulse force of the jetpack jump.
        [Range(0, 100)]
        [SerializeField] private float fuel; // The current fuel.
        [Range(0, 100)]
        [SerializeField] private float maxFuel; // The max fuel player have.
        [SerializeField] Slider fuelSlider; // UI Slider remaining fuel.
        [SerializeField] GameObject trailEffect; // A simple effect for jetpack jump.

        [Header("Points Section")]
        public int coins; // Coins Variable.
        [SerializeField] private TMP_Text coinsText; // Text UI for the coins.

        // This is for the Win event, this subscribes to the GameController Win function.
        public delegate void Delegate();
        public event Delegate Win;

        void Start() 
        {
            rb = GetComponent<Rigidbody>();
            coins = 0;
        }

        void FixedUpdate() 
        {
            Move();
        }

        //Function for make the ball move and roll
        void Move(){
            float moveX = Input.GetAxis("Horizontal");    
            float moveZ = Input.GetAxis("Vertical");   

            Vector3 move = new Vector3(moveX, 0, moveZ);

            rb.AddForce(move * speed);

            //Jump && Jetpack
            grounded = Physics.CheckSphere(transform.position, groundRadius, (int)groundLayer); // Checks with an Physic sphere if we're on ground.

            if(!grounded){
                jetpack = true;
            }else{
                jetpack = false;
                fuel = Mathf.Min(maxFuel, fuel + Time.fixedDeltaTime); // Recharge the fuel amount if we are on ground.

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Jump(); // Executes Jump function.
                }
            }

            if(jetpack && Input.GetKey(KeyCode.F) && fuel > 0){
                Jetpack(); // Executes Jetpack function.
            }

            fuelSlider.value = fuel; //Asigns the fuel value to the UI slider.

            trailEffect.SetActive(jetpack); // Set active the jetpack effect if jetpack is true.
        }

        // Jump function.
        void Jump()
        {
            rb.AddForce(jumpVector * jumpModifier, ForceMode.Impulse);
        }

        // Jetpack function.
        void Jetpack()
        {
            rb.AddForce(jumpVector * jetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            fuel = Mathf.Max(0, fuel - Time.fixedDeltaTime); // Decrease the jetpack's fuel when using.
        }

        // Coins updating function.
        public void UpdateCoins()
        {
            coinsText.text = "Coins: " + coins.ToString();  // Assign the value of the coins variable to the UI text.
        }

        private void OnCollisionEnter(Collision other) 
        {
            if(other.gameObject.tag == "Win")
            {
                Win(); // Launch Win event if player collision with Win platform.
            }
        }
    }
}

