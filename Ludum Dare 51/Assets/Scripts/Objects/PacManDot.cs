using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManDot : MonoBehaviour
{
    public int points = 15;
    private Vector3 _finalScale;
    private bool _eaten;

    private void OnEnable()
    {
        _finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        var targetScale = _eaten ? Vector3.zero : _finalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 6f * Time.deltaTime);
        if(_eaten && transform.localScale.sqrMagnitude < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {   
        var player = collider.GetComponentInParent<PlayerMovement>();
        if(player && !_eaten)
        {
            GameManager.AddPoints(points);
            _eaten = true;
        }
    }
}
