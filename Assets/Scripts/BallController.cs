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
        [SerializeField] private float speed;
        [SerializeField] Vector3 jumpVector;
        private Rigidbody rb;

        [Header("Jump Section")]
        [SerializeField] private bool isJumping;
        [SerializeField] private LayerMask groundLayer;
        [Range(0, 5)]
        [SerializeField] private float groundRadius;
        [Range(0, 500)]
        [SerializeField] private float jumpModifier;
        [SerializeField] private bool grounded;
        

        [Header("Jetpack Section")]
        [SerializeField] private bool jetpack;
        [Range(0, 5000)]
        [SerializeField] private float jetForce;
        [Range(0, 100)]
        [SerializeField] private float fuel;
        [Range(0, 100)]
        [SerializeField] private float maxFuel;
        [SerializeField] Slider fuelSlider;
        [SerializeField] GameObject trailEffect;

        [Header("Points Section")]
        public int coins;
        [SerializeField] private TMP_Text coinsText;

        void Start() 
        {
            rb = GetComponent<Rigidbody>();
            coins = 0;
        }

        void FixedUpdate() 
        {
            Move();
        }

        void Move(){
            float moveX = Input.GetAxis("Horizontal");    
            float moveZ = Input.GetAxis("Vertical");   

            Vector3 move = new Vector3(moveX, 0, moveZ);

            rb.AddForce(move * speed);

            //Jump && Jetpack
            grounded = Physics.CheckSphere(transform.position, groundRadius, (int)groundLayer);

            if(!grounded){
                jetpack = true;
            }else{
                jetpack = false;
                fuel = Mathf.Min(maxFuel, fuel + Time.fixedDeltaTime);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }

            if(jetpack && Input.GetKey(KeyCode.F) && fuel > 0){
                Jetpack();
            }

            fuelSlider.value = fuel;
            bool jetpackTrail;
            trailEffect.SetActive(jetpack);
        }

        void Jump()
        {
            rb.AddForce(jumpVector * jumpModifier, ForceMode.Impulse);
        }

        void Jetpack()
        {
            rb.AddForce(jumpVector * jetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            fuel = Mathf.Max(0, fuel - Time.fixedDeltaTime);
        }

        public void UpdateCoins()
        {
            coinsText.text = "Coins: " + coins.ToString();
        }
    }
}

