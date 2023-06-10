using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IElementBehavior 
{
    enum Elements { Water, Fire, Air,Terra, Null }

    enum Reactions { DamageUp, MovementDown, VisionDown, Null }

    void Reaction(IElementBehavior.Elements secondary, float buff, float damage);

    void SetIcon()
    {

    }

    void SetDefauntStatus()
    {

    }
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
