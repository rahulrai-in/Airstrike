﻿/*
 * Requirements:
 * -------------
 * - Layer: Projectile
 * - surfaceHitFXLayers: Set to layers against which collisions will be handled
 *   here. This will probably only be the spatial mapping layer, for which we
 *   can generate hit effects.
 * - Collision Detection: Should be Continuous Dynamic, otherwise bullets will
 *   pass through surfaces if moving too quickly.
 */

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Bullet : MonoBehaviour, IProjectile
{
  [Tooltip("How much damage this inflicts.")]
  public int hitPoints = 1;

  [Tooltip("Velocity (m/sec).")]
  public float velocity = 6f;

  [Tooltip("Maximum lifetime before being removed (sec).")]
  public float lifetime = 3f;

  [Tooltip("A surface hit effect will be created when the bullet collides with any of these layers.")]
  public LayerMask surfaceHitFXLayers;

  public int HitPoints
  {
    get
    {
      return hitPoints;
    }
  }

  public float Lifetime
  {
    get
    {
      return lifetime;
    }
  }
    
  private bool m_collided = false;
  private float m_t0;

  public void IgnoreCollisions(GameObject obj)
  {
    Collider ourCollider = GetComponent<Collider>();
    if (ourCollider == null)
      return;

    foreach (Collider collider in obj.GetComponentsInChildren<Collider>())
    {
      Physics.IgnoreCollision(ourCollider, collider);
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    // Sphere collider can collide at multiple points, we only care about first
    if (m_collided)
      return;

    // Any object that wishes to react to bullets should implement its own
    // collision callback. Spatial mesh collisions, however, are handled here
    // directly.
    GameObject target = collision.collider.gameObject;

    // We are responsible for collisions with the spatial mesh (which includes SurfacePlanes)  
    if ((surfaceHitFXLayers.value & (1 << target.layer)) != 0)
    {
      // Create a blast effect
      //ContactPoint contact = collision.contacts[0];
      //CreateSurfaceHitFX(target, contact.point, contact.normal);
    }

    // Destroy the bullet
    m_collided = true;
    gameObject.SetActive(false);
  }

  private void FixedUpdate()
  {
    if (Time.time - m_t0 >= lifetime)
      gameObject.SetActive(false);
  }

  private void OnEnable()
  {
    Vector3 forward = Vector3.Normalize(transform.forward);
    GetComponent<Rigidbody>().velocity = forward * velocity;
    m_collided = false;
    m_t0 = Time.time;
  }
}