using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    [Header("Bullet Characteristics")]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [Header("Additional Information")]
    [SerializeField] private Transform _target;
    [Header("Effect")]
    [SerializeField] private ParticleSystem _effect;
    #endregion
    public Transform Target {set { _target = value; } }  
  
    private void Update() => Move();
    private void Move()
    {
        if(_target != null)
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }
    public void AddDamage(int value) => _damage = value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Shield>())
        {
            other.GetComponent<Shield>().TakeDamage(_damage);
            OnEffect();
            Destroyed();
        }else if(other.GetComponent<ConstructorShip>())
        {
            other.GetComponent<ConstructorShip>().TakeDamage(_damage);
            OnEffect();
            Destroyed();
        }
    }

    public void Destroyed() => Destroy(gameObject);    
    public void OnEffect() => Instantiate(_effect, transform.position, transform.rotation,_target);   
}
