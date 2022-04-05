using System;
using System.Collections;
using System.Collections.Generic;
using Config;
using UnityEngine;
using Zenject;


// now use PlayerController and CharacterMovement
[Obsolete]
public class PlayerManager : MonoBehaviour
{
//     [Inject] private PlayerConfig _playerConfig;
//     
//     private Collider2D collider;
//     // private List<Vector2> colliderPoints = new List<Vector2>();
//     // private List<Vector2> simplecolliderPoints = new List<Vector2>();
//
//     private Rigidbody2D body;
//     private BodyManager bodyManager;
//     // private SpriteRenderer renderer;
//     
//     private float currentFlightModifier = 0;
//     private bool grounded;
//
//     private const float JumpVelocityThreshold = 0.01f;
//     
//     private float Speed => _playerConfig.MovementData;
//     private float JumpForce => _playerConfig.JumpForce;
//     private float FlightModifier => _playerConfig.FlightModifier;
//     private float JumpGravityMultiplier => _playerConfig.JumpGravityMultiplier;
//     private float FallGravityMultiplier => _playerConfig.FallGravityMultiplier;
//
//     private bool IsJumping => !grounded && body.velocity.y > JumpVelocityThreshold;
//     private bool IsFalling => !grounded && body.velocity.y < -JumpVelocityThreshold;
//
//     private void Awake()
//     {
//         // renderer = GetComponent<SpriteRenderer>();
//         bodyManager = GetComponent<BodyManager>();
//         collider = GetComponent<Collider2D>();
//         body = GetComponent<Rigidbody2D>();
//     }
//
//     private void Update()
//     {
//         bool isGrounded = CheckGrounded();
//         float horizontal = Input.GetAxisRaw("Horizontal");
//
//         // set correct animator direction
//         if (body.velocity.x >= 0.01)
//         {
//             SetAnimatorFloat("Speed", body.velocity.x);
//             bodyManager.SetFlip(false);
//         } else if (body.velocity.x <= 0.01 && body.velocity.x >= -0.01)
//         {
//             SetAnimatorFloat("Speed", 0);
//         } else if (body.velocity.x <= 0.01)
//         {
//             SetAnimatorFloat("Speed", Mathf.Abs(body.velocity.x));
//             bodyManager.SetFlip(true);
//         }
//
//         List<Collider2D> platformsBeneath = new List<Collider2D>();
//         if (CheckObjectBeneath("Platform", null, platformsBeneath) && Input.GetAxisRaw("Vertical") < 0) // Jump down script is here
//         {
//             // Debug.Log("We are going down...");
//
//             foreach(var platform in platformsBeneath)
//             {
//                 Physics2D.IgnoreCollision(platform, collider);
//             }
//         }
//
//         else if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             Jump();
//             isGrounded = false;
//         }
//
//         if(isGrounded)
//         {
//             body.velocity = new Vector2(horizontal * Speed, body.velocity.y);
//             currentFlightModifier = FlightModifier;
//             // UpdateCollider();
//         }
//         else
//         { // this is for control in the air. Currently is awful. Someone smart should take a look
//             body.AddForce(new Vector2(horizontal * Speed * currentFlightModifier, 0));
//             currentFlightModifier = System.Math.Max(currentFlightModifier - Time.deltaTime * .6f, 0);
//             // Debug.Log(currentFlightModifier);
//         }
//     }
//
//     private void FixedUpdate()
//     {
//         if (IsJumping)
//         {
//             ApplyGravityMultiplier(JumpGravityMultiplier);
//         }
//
//         if (IsFalling)
//         {
//             ApplyGravityMultiplier(FallGravityMultiplier);
//         }
//     }
//
//     private void ApplyGravityMultiplier(float multiplier)
//     {
//         var addition = multiplier - 1;
//         
//         body.AddForce(addition * body.mass * Physics.gravity);
//     }
//
//     // Setting platformColliders to anything but null will search for that SPECIFIC collider. Setting returnColliders to a valid list will input all the platform colliders found onto that list
//     private bool CheckObjectBeneath(string objectType, Collider2D platformCollider = null, List<Collider2D> returnColliders = null)
//     {
//
//         RaycastHit2D[] hits = PerformGroundRaycast(LayerMask.GetMask(objectType));
//
//         List<Collider2D> platformColliders = new List<Collider2D>();
//
//         if(returnColliders != null)
//         {
//             platformColliders = returnColliders;
//         }
//
//         foreach(var hit in hits) {
//             if(hit.collider.gameObject.CompareTag(objectType) && (platformCollider == null || platformCollider.Equals(hit.collider)))
//             {
//                 platformColliders.Add(hit.collider);
//                 Physics2D.IgnoreCollision(hit.collider, collider, false); /// resetting platform collision
//             }
//         }
//
//         return platformColliders.Count > 0;
//     }
//
//     /*
//     private bool CheckGrounded()
//     {
//         List<Collider2D> collidersBeneath = new List<Collider2D>();
//
//         CheckObjectBeneath("Ground", null, collidersBeneath);
//         CheckObjectBeneath("Platform", null, collidersBeneath);
//
//         List<ContactPoint2D> contacts = new List<ContactPoint2D>();
//         body.GetContacts(contacts);
//
//         foreach(var contact in contacts)
//         {
//             if (collidersBeneath.Contains(contact.collider))
//             {
//                 return true;
//             }
//         }
//
//         return collidersBeneath.Count > 0;
//     }
//     */
//
//     private bool CheckGrounded()
//     {
//         RaycastHit2D[] groundCheck = PerformGroundRaycast(LayerMask.GetMask("Ground"), 8);
//         RaycastHit2D[] platformCheck = PerformGroundRaycast(LayerMask.GetMask("Platform"), 8);
//
//         Debug.Log(groundCheck.Length + platformCheck.Length);
//
//         return groundCheck.Length + platformCheck.Length > 0;
//     }
//
//     /* I don't think we need the enter function that much
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Platform") && !CheckGroundedOnPlatform(collision.collider))
//         {
//             grounded = true;
//             Debug.Log("Ignoring platform collision...");
//             Physics2D.IgnoreCollision(collision.collider, collider);
//         }
//     }
//     */
//
//     private void OnCollisionStay2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Platform") && !CheckObjectBeneath("Platform", collision.collider))
//         {
//             // Debug.Log("Ignoring platform collision...");
//             Physics2D.IgnoreCollision(collision.collider, collider);
//         }
//     }
//
//     public void Jump()
//     {
//         SetAnimatorBool("IsJumping", true);
//         grounded = false;
//         body.velocity = new Vector2(body.velocity.x, 0f);
//         body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
//     }
//
//     /*
//     private void UpdateCollider(float tolerance = 0.05f)
//     {
//         if (renderer.sprite != null)
//         {
//             collider.pathCount = renderer.sprite.GetPhysicsShapeCount();
//
//             for (int i = 0; i < collider.pathCount; i++)
//             {
//                 renderer.sprite.GetPhysicsShape(i, colliderPoints);
//                 LineUtility.Simplify(colliderPoints, tolerance, simplecolliderPoints);
//                 collider.SetPath(i, simplecolliderPoints);
//             }
//         }
//     }
//     */
//
//     private RaycastHit2D[] PerformGroundRaycast(int layerMask = 0, float scaleDownFactor = 4)
//     {
//         Vector3 halfHeight = new Vector3(0, collider.bounds.extents.y);
//
//         Vector3 boxCastHeight = new Vector3(0, halfHeight.y / scaleDownFactor);
//
//         Vector3 origin = transform.position + new Vector3(collider.offset.x, collider.offset.y, 0) - halfHeight - boxCastHeight;
//
//         Vector3 boxCastSize = new Vector3(collider.bounds.size.x, boxCastHeight.y);
//
//         Debug.DrawLine(origin, origin - boxCastSize / 2);
//
//         return Physics2D.BoxCastAll(origin, boxCastSize, 0, Vector2.zero, 0, layerMask);
//     }
//
//     private void SetAnimatorBool(string flagName, bool value) //used for safe flag setting
//     {
//         bodyManager.SetAnimatorBool(flagName, value);
//     }
//
//     private void SetAnimatorFloat(string flagName, float value) //used for safe flag setting
//     {
//         bodyManager.SetAnimatorFloat(flagName, value);
//     }

}
