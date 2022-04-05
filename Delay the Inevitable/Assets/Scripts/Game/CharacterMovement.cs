using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Game
{
    public class CharacterMovement : MonoBehaviour
    {
        //[SerializeField] private Collider2D coll;
        [SerializeField] private CapsuleCollider2D coll;
        [SerializeField] private Rigidbody2D rb;

        private float _currentFlightModifier;
        
        // readonly
        [SerializeField] private bool _grounded;

        private const float VerticalVelocityThreshold = 0.05f;
        private const float HorizontalVelocityThreshold = 0.01f;
        private const string PlatformLayerName = "Platform";
        private const string GroundLayerName = "Ground";
        
        public bool IsMovingRight => rb.velocity.x > HorizontalVelocityThreshold;
        public bool IsMovingLeft => rb.velocity.x < -HorizontalVelocityThreshold;
        public float AbsoluteHorizontalVelocity => Mathf.Abs(rb.velocity.x);
        
        private bool IsJumping => !_grounded && rb.velocity.y > VerticalVelocityThreshold;
        private bool IsFalling => !_grounded && rb.velocity.y < -VerticalVelocityThreshold;
        private float JumpGravityMultiplier => MovementData?.JumpGravityMultiplier ?? 0f;
        private float FallGravityMultiplier => MovementData?.FallGravityMultiplier ?? 0f;
        private float HorizontalSpeed => MovementData?.MovementSpeed ?? 0f;
        private float JumpForce => MovementData?.JumpForce ?? 0f;
        private float FlightModifier => MovementData?.FlightModifier ?? 0f;
        
        public MovementData MovementData { get; set; }

        public event Action Jumped;

        public event Action Grounded;

        private void Reset()
        {
            //coll = GetComponent<Collider2D>();
            coll = GetComponent<CapsuleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _grounded = CheckGrounded();
            
            if (IsJumping)
            {
                ApplyGravityMultiplier(JumpGravityMultiplier);
                Jumped?.Invoke();
            }
            else if(_grounded)
            {
                Grounded?.Invoke();
            }

            if (IsFalling)
            {
                ApplyGravityMultiplier(FallGravityMultiplier);
                Jumped?.Invoke();
            }
            else if (_grounded)
            {
                Grounded?.Invoke();
            }
        }

        public void MoveHorizontally(float horizontalInput)
        {
            bool isGrounded = CheckGrounded();

            if (isGrounded)
            {
                rb.velocity = new Vector2(horizontalInput * HorizontalSpeed, rb.velocity.y);
                
                // _currentFlightModifier = FlightModifier;
                // UpdateCollider();
            }
            else
            {
                // could not figure out a way to take control form the player without making things too convoluted
                // for now, this should work just fine
                rb.velocity = new Vector2(horizontalInput * HorizontalSpeed, rb.velocity.y);

                // this is for control in the air. Currently is awful. Someone smart should take a look
                // rb.AddForce(new Vector2(horizontalInput * HorizontalSpeed * _currentFlightModifier, 0));
                // _currentFlightModifier = System.Math.Max(_currentFlightModifier - Time.deltaTime * .6f, 0);
                // Debug.Log(currentFlightModifier);
            }
        }


        public void Jump(float verticalInput)
        {
            if (!_grounded)
            {
                return;
            }

            var hasDownInput = verticalInput < -VerticalVelocityThreshold;
            if (hasDownInput)
            {
                var platformsBeneath = new List<Collider2D>();

                var hasPlatformBeneath = CheckObjectBeneath(PlatformLayerName, null, platformsBeneath);
                if (hasPlatformBeneath)
                {
                    var platform = platformsBeneath[0];
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(Vector2.up * -JumpForce, ForceMode2D.Impulse);
                    StartCoroutine(BypassCollisionTemporarily(platform));
                }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            }
            _grounded = false;

            Jumped?.Invoke();
        }

        private IEnumerator BypassCollisionTemporarily(Collider2D platform)
        {
            Physics2D.IgnoreCollision(platform, this.coll, true);
            yield return new WaitForSeconds(0.4f);
            Physics2D.IgnoreCollision(platform, this.coll, false);
        }

        private void ApplyGravityMultiplier(float multiplier)
        {
            var addition = multiplier - 1;

            rb.AddForce(addition * rb.mass * Physics.gravity);
        }

        // Setting platformColliders to anything but null will search for that SPECIFIC collider. Setting returnColliders to a valid list will input all the platform colliders found onto that list
        private bool CheckObjectBeneath(string objectType, Collider2D platformCollider = null,
            List<Collider2D> returnColliders = null)
        {
            RaycastHit2D[] hits = PerformGroundRaycast(LayerMask.GetMask(objectType));

            List<Collider2D> platformColliders = new List<Collider2D>();

            if (returnColliders != null)
            {
                platformColliders = returnColliders;
            }

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.CompareTag(objectType) &&
                    (platformCollider == null || platformCollider.Equals(hit.collider)))
                {
                    platformColliders.Add(hit.collider);
                    Physics2D.IgnoreCollision(hit.collider, coll, false); /// resetting platform collision
                }
            }

            return platformColliders.Count > 0;
        }
        
        private bool CheckGrounded()
        {
            RaycastHit2D[] groundCheck = PerformGroundRaycast(LayerMask.GetMask(GroundLayerName), 16);
            
            RaycastHit2D[] platformCheck = PerformGroundRaycast(LayerMask.GetMask(PlatformLayerName), 16);

            bool isGrounded = groundCheck.Length + platformCheck.Length > 0;


            return isGrounded;
        }
        
        private RaycastHit2D[] PerformGroundRaycast(int layerMask = 0, float scaleDownFactor = 4)
        {
            // Vector3 halfHeight = new Vector3(0, coll.bounds.extents.y);
            Vector3 halfHeight = new Vector3(0, coll.bounds.extents.y);

            Vector3 boxCastHeight = new Vector3(0, halfHeight.y / scaleDownFactor);

            Vector3 origin = transform.position + new Vector3(coll.offset.x, coll.offset.y, 0) - halfHeight -
                             boxCastHeight;

            Vector3 boxCastSize = new Vector3(coll.bounds.size.x * .95f, boxCastHeight.y);

            Debug.DrawLine(origin, origin - boxCastSize / 2);

            return Physics2D.BoxCastAll(origin, boxCastSize, 0, Vector2.zero, 0, layerMask);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            bool isPlatform = collision.gameObject.CompareTag(PlatformLayerName);
            bool isGround = collision.gameObject.CompareTag(GroundLayerName);

            bool isPlatformBeneath = CheckObjectBeneath(PlatformLayerName, collision.collider);
            bool isGroundBeneath = CheckObjectBeneath(GroundLayerName, collision.collider);

            if (isPlatform && !isPlatformBeneath)
            {
                // Debug.Log("Ignoring platform collision...");
                // Physics2D.IgnoreCollision(collision.collider, coll);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(PlatformLayerName) && !CheckObjectBeneath(PlatformLayerName, collision.collider))
            {
                // Debug.Log("Ignoring platform collision...");
                // Physics2D.IgnoreCollision(collision.collider, coll);
            }
        }
        
        /*
        private bool CheckGrounded()
        {
            List<Collider2D> collidersBeneath = new List<Collider2D>();
    
            CheckObjectBeneath("Ground", null, collidersBeneath);
            CheckObjectBeneath("Platform", null, collidersBeneath);
    
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            body.GetContacts(contacts);
    
            foreach(var contact in contacts)
            {
                if (collidersBeneath.Contains(contact.collider))
                {
                    return true;
                }
            }
    
            return collidersBeneath.Count > 0;
        }
        */

        /* I don't think we need the enter function that much
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform") && !CheckGroundedOnPlatform(collision.collider))
            {
                grounded = true;
                Debug.Log("Ignoring platform collision...");
                Physics2D.IgnoreCollision(collision.collider, collider);
            }
        }
        */

        /*
        private void UpdateCollider(float tolerance = 0.05f)
        {
            if (renderer.sprite != null)
            {
                collider.pathCount = renderer.sprite.GetPhysicsShapeCount();
    
                for (int i = 0; i < collider.pathCount; i++)
                {
                    renderer.sprite.GetPhysicsShape(i, colliderPoints);
                    LineUtility.Simplify(colliderPoints, tolerance, simplecolliderPoints);
                    collider.SetPath(i, simplecolliderPoints);
                }
            }
        }
        */
    }
}