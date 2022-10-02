using UnityEngine;

[CreateAssetMenu(menuName = "Sound Effect")]
public class SoundEffect : ScriptableObject
{
	[Header("Clips")]
	[SerializeField] internal AudioClip[] Clips;

	[Header("Volume")]
	[SerializeField] internal float MinVolume = 0.4f;
	[SerializeField] internal float MaxVolume = 0.6f;

	[Header("Pitch")]
	[SerializeField] internal float MinPitch = 0.95f;
	[SerializeField] internal float MaxPitch = 1.05f;
	
	[Header("Other")]
	[SerializeField] internal bool Loop;
}
