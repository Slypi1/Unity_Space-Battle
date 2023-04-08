using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstructorShip))]
public class ControllerShip : MonoBehaviour
{
    #region Variables
    [Header("Information for the battle")]
    [SerializeField] private Transform _target;
    [SerializeField] private int _attackDistance;
    [Header("Information for the ship")]
    [SerializeField] private float _speed;

    private Ship _ship;
    private Rigidbody _rbShip;
    #endregion
    public static Action<Sprite> OnVictory;
    private void Start()
    {
        _ship = GetComponent<Ship>();
        _rbShip = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MainFinght();
        IsVictory();
    }

    private void MainFinght()
    {
        Vector3 targetDirection = _target.position - transform.position;
        float targetAngle = Vector3.Angle(targetDirection, transform.forward);
        if (targetDirection.magnitude > _attackDistance) MovementForward();
        else if (targetAngle < 30f && targetDirection.magnitude <= _attackDistance && !_ship.Shield.IsBroken)
        {
            transform.LookAt(_target);
            _rbShip.velocity = Vector3.zero;
            Attack();
        }
        else if (targetDirection.magnitude <= _attackDistance && _ship.Shield.IsBroken)    
            IsAttack(targetDirection);     
    }

    private void Attack() => _ship.Fire(_target);

    private void MovementForward() => _rbShip.velocity = transform.forward;

    private void IsAttack(Vector3 targetDirection)
    {
        RaycastHit hit;
        bool hitInfo = Physics.Raycast(transform.position, targetDirection, out hit);
        if (!hitInfo) return;
        if (hit.collider.GetComponent<Bullet>())
        {
            var bullet = hit.collider.GetComponent<Bullet>();
            Vector3 direction = transform.position - bullet.transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Vector3 retreatDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            transform.position += retreatDirection * _speed * Time.deltaTime;
        }
        else
        {
            Attack();
            transform.LookAt(_target);
        }
    }

    private void IsVictory()
    {
        if (!_target.gameObject.activeSelf)
            OnVictory(_ship.SpiteShip);
    }
}

