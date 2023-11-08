using SuperMageShield;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySO enemyData;

    private GameObject projectileObj;
    private float shootingFrequency;

    private void Start()
    {
        shootingFrequency = enemyData.shootingFrequency;
        StartCoroutine(Shoot() );
    }

    private IEnumerator Shoot()
    {

        projectileObj = Instantiate(enemyData.enemyProjectile);
        projectileObj.SetActive(true);
        projectileObj.GetComponent<Rigidbody2D>().velocity = Vector2.up * -1 * enemyData.enemyProjectileSpeed;
        yield return new WaitForSeconds(shootingFrequency);
        StartCoroutine(Shoot());
    }

}
