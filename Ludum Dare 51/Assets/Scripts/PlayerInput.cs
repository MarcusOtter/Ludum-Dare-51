using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    internal event EventHandler OnAttackKeyDown;
    internal event EventHandler OnAttackKeyUp;

    internal Vector3 MouseWorldPosition { get; private set; }
    internal float HorizontalAxis { get; private set; }
    internal float VerticalAxis { get; private set; }

	[SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private string horizontalAxisName = "Horizontal";
    [SerializeField] private string verticalAxisName = "Vertical";
    [SerializeField] private LayerMask groundLayer;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            OnAttackKeyDown?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyUp(attackKey))
        {
            OnAttackKeyUp?.Invoke(this, EventArgs.Empty);
        }

        HorizontalAxis = Input.GetAxis(horizontalAxisName);
        VerticalAxis = Input.GetAxis(verticalAxisName);
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer.value))
        {
            MouseWorldPosition = hit.point;
        }
    }
}
