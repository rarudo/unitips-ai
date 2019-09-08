using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageParts : MonoBehaviour {
    
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private List<Renderer> _renderers;

    public void Initialize(bool renderStage = true) {
        _rigidbody = GetComponent<Rigidbody2D>();
        foreach (var r in _renderers) {
            r.enabled = renderStage;
        }
    }
    
    public void SetVelocity(Vector3 velocity) {
        _rigidbody.velocity = velocity;
    }
}
