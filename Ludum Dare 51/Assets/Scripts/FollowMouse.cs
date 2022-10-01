using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private PlayerInput _input;
    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        transform.position = _input.MouseWorldPosition;
    }
}
