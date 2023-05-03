using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElementBehavior 
{
    enum Elements { Water, Fire, Air,Terra, Null }

    enum Reactions { DamageUp, MovementDown, VisionDown, Null }

    void Reaction(IElementBehavior.Elements secondary, float buff, float damage);
}
