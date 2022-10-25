using UnityEngine;
using BoxDefence.AI;
using BoxDefence.Pooling;
using BoxDefence.DamageSystem;

public class Bullet : MonoBehaviour, IPoolObject, IDamager
{
    #region Fields

    [Header("Ñharacteristics")]
    [SerializeField] private float _speed;
    [SerializeField] private IDamager _defaultDamage;

    private Enemy _targetEnemy;

    #endregion

    #region Properties

    [field: Space]
    [field: SerializeField] public PoolType PoolTypeObject { get; private set; }

    #endregion

    #region Unity Methods

    private void Update()
    {
        MoveToEnemy();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Enemy enemy) && enemy == _targetEnemy)
        {
            enemy.TakeDamage(_defaultDamage);

            Destroy();
        }
    }

    #endregion

    #region Public Methods

    public void Init(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;
    }
    public void OnStart(IDamager damager, Enemy target)
    {
        _defaultDamage = damager;
        _targetEnemy = target;
    }

    public float GetDamage()
    {
        return _defaultDamage.GetDamage();
    }
    public DamageType GetDamageType()
    {
        return _defaultDamage.GetDamageType();
    }
    public void DepleteBy(float percentageInDozens)
    {
        _defaultDamage.DepleteBy(percentageInDozens);
    }
    public void IncreaseBy(float percentageInDozens)
    {
        _defaultDamage.IncreaseBy(percentageInDozens);
    }

    #endregion

    #region Private Methods

    private void MoveToEnemy()
    {
        float step = _speed * Time.deltaTime;

        if (_targetEnemy != null)
            transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, step);
        else
            Destroy();
    }
    private void Destroy()
    {
        ObjectPooler.Instance.DeleteObject(this);

        gameObject.SetActive(false);
    }

    #endregion
}
