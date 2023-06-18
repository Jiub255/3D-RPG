using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable
{
    public abstract void GetKnockedBack(Vector3 knockbackVector);
}