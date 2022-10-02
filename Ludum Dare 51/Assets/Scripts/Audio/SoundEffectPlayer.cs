using UnityEngine;

[RequireComponent(typeof(AudioSource))]
    public class SoundEffectPlayer : MonoBehaviour
    {
        internal static float MainVolume = 1f;
        private const float PlayerHearingDistance = 20f;

        private bool _scaleVolumeWithPlayerDistance = true;
        private bool _scalePitchWithTimeScale = true;

        private float _activeSoundEffectVolume;

        private AudioSource _source;
        private Transform _playerTransform;
        private Transform _creatorTransform; // The transform of the object that created this sound

        private void Awake()
        {
            _source = GetComponent<AudioSource>();

            if (_scaleVolumeWithPlayerDistance)
            {
                _playerTransform = FindObjectOfType<PlayerMovement>().transform;
            }
        }

        private void Update()
        {
            if (_scaleVolumeWithPlayerDistance)
            {
                FollowCreatorObject();
                _source.volume = CalculateVolume();
            }

            if (_scalePitchWithTimeScale)
            {
                _source.pitch = Time.timeScale;
            }

            if (!_source.isPlaying)
            {
                Destroy(gameObject);
            }
        }

        private void FollowCreatorObject()
        {
            if (_creatorTransform != null)
            {
                transform.position = _creatorTransform.position;
            }
        }

        internal static void PlaySoundEffect(SoundEffect soundEffect, Transform sender, bool scaleVolumeWithPlayerDistance = true, bool scalePitchWithTimeScale = true)
        {
            var player = new GameObject($"{sender.name} Sound Player", typeof(SoundEffectPlayer)).GetComponent<SoundEffectPlayer>();
            player.Initialize(soundEffect, sender, scaleVolumeWithPlayerDistance, scalePitchWithTimeScale);
        }

        private void Initialize(SoundEffect soundEffect, Transform sender, bool scaleVolumeWithDistance, bool scalePitchWithTimeScale)
        {
            _creatorTransform = sender;
            _scaleVolumeWithPlayerDistance = scaleVolumeWithDistance;
            _scalePitchWithTimeScale = scalePitchWithTimeScale;

            transform.position = _creatorTransform.position;

            if (soundEffect == null)
            {
                Debug.LogWarning($"{sender.name} does not have a sound effect.", gameObject);
                Destroy(gameObject);
                return;
            }

            var volume = Random.Range(soundEffect.MinVolume, soundEffect.MaxVolume);
            var pitch = Random.Range(soundEffect.MinPitch, soundEffect.MaxPitch);
            var clip = soundEffect.Clips[Random.Range(0, soundEffect.Clips.Length)];
                
            _activeSoundEffectVolume = volume; // Must be assigned before CalculateVolume() is called.

            _source.pitch = pitch;
            _source.volume = CalculateVolume();
            _source.clip = clip;
            _source.loop = soundEffect.Loop;

            _source.Play();
        }

        private float CalculateVolume()
        {
            var volume = _activeSoundEffectVolume * MainVolume;

            if (_scaleVolumeWithPlayerDistance)
            {
                volume *= PlayerDistanceMultiplier();
            }

            return volume;
        }

        /// <summary>
        /// Returns a value between 0 and 1 that represents the Vector2.Distance between 
        /// the player and the position of this object divided by the player hearing distance.
        /// </summary>
        private float PlayerDistanceMultiplier()
        {
            var distance = Vector2.Distance(_playerTransform.position, transform.position);

            if (distance > PlayerHearingDistance) { return 0; }

            return 1 - distance / PlayerHearingDistance;
        }
    }
