using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Elements { Water, Fire, Air, Terra, Null }
public enum Reactions { DamageUp, MovementDown, VisionDown, Null }
public interface IElementBehavior 
{
    void Reaction(Elements secondary, float buff, float damage);
}

public interface IMoveBehavior
{
    static float speed;

    public enum EnemyLevel 
    {
        Дружелюбный,
        Враждебный,
        Опасный,
        Демонический,
    }

    void Movement()
    {

    }
}

public interface IDamageBehavior
{
    void TakeDamage(float damage)
    {

    }

    void SetHealth(float bounus)
    {

    }
}

public interface INPCTask
{
    void Task(Transform[] enemys);
}
