using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GroundChecker))]

public class Worm : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform _groundDetecter;

    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody2d;
    private bool _isLeft = true;
    private Animator _animator;
    private float _rotateAngle = 180;
    private int _wormHash = Animator.StringToHash("Worm");

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _groundChecker = GetComponent<GroundChecker>();
    }

    private void Start()
    {
        _animator.Play(_wormHash);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float directionX = -1f;
        _rigidbody2d.velocity = new Vector2(directionX * _speed, _rigidbody2d.velocity.y);

        if (_groundChecker.CheckGround() == false)
        {
            _speed *= directionX;
            Rotate();
        }
    }

    private void Rotate()
    {
        if (_isLeft == true)
        {
            transform.eulerAngles = new Vector3(0, _rotateAngle, 0);
            _isLeft = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            _isLeft = true;
        }
    }

    private bool CheckGround()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetecter.position, Vector2.down, 1f);

        return groundInfo.collider;
    }
}
