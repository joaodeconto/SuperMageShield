using SuperMageShield;
using System.Collections;
using UnityEngine;

public class EnemyController : EntityController
{
    private EnemySO _enemyData;
    private float _shootingFrequency;
    private int _enemyLevel;
    private Vector3 _projectileOffset = new(0, .5F, 0);

    protected override void Awake()
    {
        base.Awake();
        _enemyData = _entityData as EnemySO;
        _shootingFrequency = _enemyData.shootingFrequency;
    }
    private void OnEnable()
    {
        HandleEnemyLevel();
    }

    private void HandleEnemyLevel()
    {
        _enemyLevel = _enemyData.enemyLevel;
        switch (_enemyLevel)
        {
            case 0: StartCoroutine(SingleForwardShoot()); break;
            case 1: StartCoroutine(SingleDirectionalShoot(new Vector2(1, -1))); break;
            case 2: StartCoroutine(AllDirectionShoot()); break;
        }
    }

    private IEnumerator SingleForwardShoot()
    {
        _projectileObj = PoolManager.Instance.AvailableGameObject();
        _projectileObj.transform.SetPositionAndRotation(transform.position - _projectileOffset, Quaternion.Euler(Vector3.zero));
        _projectileObj.SetActive(true);
        _projectileObj.GetComponent<Rigidbody2D>().velocity = Vector2.up * _enemyData.enemyProjectileSpeed;
        yield return new WaitForSeconds(_shootingFrequency);
        StartCoroutine(SingleForwardShoot());
    }

    private IEnumerator SingleDirectionalShoot(Vector2 dir)
    {
        int random = Random.Range(0, 1);
        dir.x *= random == 0 ? -1 : 1;

        _projectileObj = PoolManager.Instance.AvailableGameObject();
        _projectileObj.transform.SetPositionAndRotation(transform.position - _projectileOffset, Quaternion.Euler(Vector3.zero));
        _projectileObj.SetActive(true);
        _projectileObj.GetComponent<Rigidbody2D>().velocity = dir * _enemyData.enemyProjectileSpeed;
        yield return new WaitForSeconds(_shootingFrequency);
        StartCoroutine(SingleDirectionalShoot(dir));
    }

    private IEnumerator AllDirectionShoot()
    {
        int random = Random.Range(-1, 1);
        Vector2 dir = Vector2.up; 
         dir.x = random;
        _projectileObj = PoolManager.Instance.AvailableGameObject();
        _projectileObj.transform.SetPositionAndRotation(transform.position - _projectileOffset, Quaternion.Euler(Vector3.zero));
        _projectileObj.SetActive(true);
        _projectileObj.GetComponent<Rigidbody2D>().velocity = dir * _enemyData.enemyProjectileSpeed;
        yield return new WaitForSeconds(_shootingFrequency);
        StartCoroutine(AllDirectionShoot());
    }

    private void DropChance()
    {
        float random = Random.Range(0f, 1f);
        if (random < _enemyData.buffDropChance)
        {
            BuffManager.Instance.DropBuff(this.transform.position, _enemyData.BuffType);
        }
    }

    protected override void DoDestroy()
    {
        OnEntityDefeated(_entityData);

        if (_pointsFeedback != null)
            _pointsFeedback.text = _entityData.entityScore.ToString();

        DropChance();

        gameObject.SetActive(false);
    }

}
