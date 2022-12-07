using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ballgame{
    public class BallController : MonoBehaviour
    {
        [Header("Moving Section")]
        [Range(0, 500)]
        [SerializeField] float speed;
        Rigidbody rb;

        [Header("Jump Section")]
        [SerializeField] LayerMask groundLayer;
        [Range(0, 5)]
        [SerializeField] float groundRadius;
        [Range(0, 500)]
        [SerializeField] float jumpModifier;
        [SerializeField] bool grounded;
        

        [Header("Jetpack Section")]
        [SerializeField] bool jetpack;
        [Range(0, 5000)]
        [SerializeField] float jetForce;
        [Range(0, 100)]
        [SerializeField] float fuel;
        [Range(0, 100)]
        [SerializeField] float maxFuel;

        void Start() 
        {
            rb = GetComponent<Rigidbody>();
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

            //Jump
            grounded = Physics.CheckSphere(transform.position, groundRadius, (int)groundLayer);

            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            //Jetpack
            if(!grounded){
                jetpack = true;
            }else{
                jetpack = false;
            }

            if(grounded){
                 fuel = Mathf.Min(maxFuel, fuel + Time.fixedDeltaTime);
            }

            if(jetpack && Input.GetKey(KeyCode.Space) && fuel > 0){
                Jetpack();
            }
        }

        void Jump()
        {
            rb.AddForce(transform.up * jumpModifier, ForceMode.Impulse);
        }

        void Jetpack()
        {
            rb.AddForce(Vector3.up * jetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            fuel = Mathf.Max(0, fuel - Time.fixedDeltaTime);
        }
    }
}

