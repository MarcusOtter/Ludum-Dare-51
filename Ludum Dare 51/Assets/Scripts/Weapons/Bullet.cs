using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Impact settings")]
    [SerializeField] private SoundEffect impactSound;
    [SerializeField] private ParticleSystem impactParticleSystem;
    [SerializeField] private GameObject spawnOnImpact;

    private int _damage;
    private float _speed;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }
    
    internal void Shoot(int damage, float speed)
    {
        _damage = damage;
        _speed = speed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (spawnOnImpact != null)
        {
            Instantiate(spawnOnImpact, transform.position, Quaternion.identity);
        }

        var hurtable = collision.transform.root.GetComponentInChildren<IHurtable>();
        if (hurtable == null)
        {
            SoundEffectPlayer.PlaySoundEffect(impactSound, transform);
            Destroy(gameObject);
            return;
        }

        var willKillHurtable = hurtable.IsLethalDamage(_damage);

        if (!willKillHurtable)
        {
            Destroy(gameObject);
        }

        hurtable.TakeDamage(_damage);
    }
}
