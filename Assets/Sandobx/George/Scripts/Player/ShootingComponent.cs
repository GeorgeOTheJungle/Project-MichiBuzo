using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private Transform target;

    [SerializeField] private SoundWave soundWave;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void Start()
    {
        InputComponent.Instance.shotTrigger += Shoot;
    }

    private void OnEnable()
    {
        if(InputComponent.Instance)InputComponent.Instance.shotTrigger += Shoot;
    }

    private void OnDisable()
    {
        if (InputComponent.Instance) InputComponent.Instance.shotTrigger -= Shoot;
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, attackRange, enemyMask);

        if (collider)
        {
            if (collider.TryGetComponent<ITargeteable>(out ITargeteable targeteable))
            {
                // Display a target thing;
                target = collider.transform;
            }
        } else
        {
            target = null;
        }
    }

    private void Shoot()
    {
        if (target == null) return;
        soundWave.LaunchWave(target);
    }
}
