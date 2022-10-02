using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Follow mouse & player settings")]
    [SerializeField] [Range(0, 1)] private float smoothTime = 0.1f;
    [SerializeField] private float maxDistance = 4;

    private PlayerInput _player;
    private Vector2 _velocity;
    
    private void Awake()
    {
        _player = FindObjectOfType<PlayerInput>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    /// <summary>
    /// Smoothly follows the player and offsets the position depending on where the cursor is relative to the player
    /// </summary>
    private void FollowPlayer()
    {
        var playerPos = _player.transform.position;
        var transformPos = transform.position;

        // The point between the cursor and the player
        var midPoint = (playerPos +  _player.MouseWorldPosition) / 2;

        // Offset between mid point and player, with clamped magnitude
        var offset = midPoint - playerPos;
        offset = Vector3.ClampMagnitude(offset, maxDistance);

        var targetPos = new Vector3(playerPos.x + offset.x, playerPos.y, playerPos.z + offset.z);
        transform.position = Vector3.Lerp(transformPos, targetPos, smoothTime);
    }
}
