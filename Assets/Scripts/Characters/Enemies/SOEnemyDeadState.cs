using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Dead State", fileName = "Enemy Dead State")]
public class SOEnemyDeadState : SOState<EnemyController>
{
    public override void CaptureInput() {}
    public override void Update() {}
    public override void CheckForStateChangeConditions() {}
    public override void FixedUpdate() {}
    public override void Exit() {}
}