using UnityEngine;
using BoxDefence.AI;
using BoxDefence.Pooling;

public class Bullet : MonoBehaviour, IPool
{
    [Header("Ñharacteristics")]
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    private Enemy _targetEnemy;

    private void Update()
    {
        MoveToEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Enemy enemy) && enemy == _targetEnemy)
        {
            enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }

    public void OnStart(float damage, Enemy target)
    {
        _damage = damage;
        _targetEnemy = target;
    }

    private void MoveToEnemy()
    {
        float step = _speed * Time.deltaTime;

        if (_targetEnemy != null)
            transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, step);
        else
            Destroy(gameObject);
    }

    public void Init(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;
    }
}
