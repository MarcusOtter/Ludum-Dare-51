using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
    [SerializeField] private int points = 500;
    [SerializeField] private SoundEffect _holedBallSound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Golf Ball"))
        {
            other.GetComponent<Hurtable>()?.Die();
            SoundEffectPlayer.PlaySoundEffect(_holedBallSound, transform);
            GameManager.AddPoints(points);
        }
    }
}
