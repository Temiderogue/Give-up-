using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Direction;
    public bool isOnGround;
    [SerializeField] private int _lastDirection;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _speed, _jumpSpeed;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PhysicsMaterial2D _default;
    [SerializeField] private PhysicsMaterial2D _jumpy;
    private int _angle = 75;
    private bool _moveLeft, _moveRight;
    private IEnumerator _jump;
    private bool _isPressingJump = false;

    private void Start()
    {
        _lastDirection =  -1;
    }

    private void Update()
    {
        if (_jumpDuration > 1.2f)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_isPressingJump && isOnGround)
        {
            _jumpDuration += 0.013f;
        }

        if (_moveRight && isOnGround)
        {
            _rigidBody.velocity = new Vector2(_speed, _rigidBody.velocity.y);
        }
        else if (_moveLeft && isOnGround)
        {
            _rigidBody.velocity = new Vector2(-_speed, _rigidBody.velocity.y);
        }
    }

    public void ChangeMat(string mat)
    {
        if (mat == "default")
        {
            _rigidBody.sharedMaterial = _default;
        }
        else if (mat == "jumpy")
        {
            _rigidBody.sharedMaterial = _jumpy;
        }
    }

    public void MoveRight(string State)
    {
        if (State == "on")
        {
            _moveRight = true;
            _lastDirection = 1;
        }
        else if (State == "off")
        {
            _moveRight = false;
            _rigidBody.velocity = new Vector2(0.0f, _rigidBody.velocity.y);
        }
    }

    public void MoveLeft(string State)
    {
        if (State == "on")
        {
            _moveLeft = true;
            _lastDirection = -1;
        }
        else if (State == "off")
        {
            _moveLeft = false;
            _rigidBody.velocity = new Vector2(0.0f, _rigidBody.velocity.y);
        }
    }

    public void JumpBegin()
    {
        if (isOnGround)
        {
            _isPressingJump = true;
            _rigidBody.velocity = new Vector2(0.0f, _rigidBody.velocity.y);
        }
       
    }

    public void Jump()
    {
        if (isOnGround)
        {
            _isPressingJump = false;

            if (_jumpDuration < 0.8f)
            {
                _angle = 45;
                _jumpSpeed = 5;
            }
            else if (_jumpDuration >= 0.8f && _jumpDuration < 1f)
            {
                _angle = 55;
                _jumpSpeed = 8;
            }
            else if (_jumpDuration >= 1f)
            {
                _angle = 75;
                _jumpSpeed = 10;
            }

            float velx = _jumpSpeed * Mathf.Cos(_angle * Mathf.Deg2Rad) * _lastDirection * _jumpDuration;
            float vely = _jumpSpeed * Mathf.Sin(_angle * Mathf.Deg2Rad) * _jumpDuration;
            _rigidBody.velocity = new Vector2(velx, vely);
            _jumpDuration = 0.4f;
        }
        
    }

}
