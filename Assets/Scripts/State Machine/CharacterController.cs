using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : StateRunner<CharacterController>
{
	//public PlayerCharacterAnimation PlayerCharacterAnimation;

    protected override void Awake()
    {
        //PlayerCharacterAnimation = ...
        base.Awake();
    }
}