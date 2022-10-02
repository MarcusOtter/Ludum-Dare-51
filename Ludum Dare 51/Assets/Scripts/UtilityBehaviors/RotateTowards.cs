using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.With(x: 0));
    }
}
