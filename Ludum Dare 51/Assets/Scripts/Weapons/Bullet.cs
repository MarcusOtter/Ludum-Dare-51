using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Impact settings")]
    [SerializeField] private SoundEffect impactSound;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private int _damage;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    internal void Shoot(int damage, float speed)
    {
        _damage = damage;
        _rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody.velocity = Vector2.zero;
        
        // TODO: Figure out collision.
        // Get an IShootable which has a color that we can set to the particle system
        // Try to send damage to IShootable
        
        // Play impact sound
    }
}
