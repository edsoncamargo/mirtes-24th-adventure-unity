using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRangeController : MonoBehaviour {
    [SerializeField]
    private EnemyController _enemyController;

    private void OnTriggerStay2D(Collider2D other) {
        AnimatorClipInfo[] clipInfo = _enemyController.GetAnimator().GetCurrentAnimatorClipInfo(0);

        if (other.CompareTag("Player") && clipInfo[0].clip.name != "Attack" && clipInfo[0].clip.name != "Hit") {
            _enemyController.Attack();
        }
    }
}
