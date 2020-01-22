using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Character caster;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    //[SerializeField] private Vector3 size;
    [SerializeField] private bool isForFriend;
    [SerializeField] private SkillEffect skillEffect;

    public void Initialize(Character caster, Vector3 direction, float speed, Vector3 size, bool isForFriend, SkillEffect skillEffect)
    {
        this.caster = caster;
        this.direction = direction;
        this.speed = speed;
        transform.localScale = size;
        this.isForFriend = isForFriend;
        this.skillEffect = skillEffect;
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character target = other.GetComponent<Character>();
        if (target.isFriend != isForFriend) return;

        GameManager.instance.ApplySkill(target, skillEffect.GetEffectResult(caster, target));
    }
}


/*
public class AroundProjectile
{
    bool isFriend;
    float targetRange;
    LayerMask contactLayer;

    private void Start()
    {

    }

    private List<Character> FindTargets()
    {
        List<Character> targets = new List<Character>();

        Collider[] colls = Physics.OverlapSphere(caster.transform.position, targetRange, contactLayer);

        foreach(Collider coll in colls)
        {
            Character character = coll.GetComponent<Character>();
            if (character.isFriend != isFriend) continue;
            if (Vector3.Distance(caster.transform.position, character.transform.position) > targetRange) continue;

            targets.Add(character);
        }

        return targets;
    }
}
*/