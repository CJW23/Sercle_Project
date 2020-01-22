using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeType { Self, Around, Direction }

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Skill")]
public class Skill : ScriptableObject
{
    [Header("Basic Info")]
    public string skillName;
    public string description;

    [Header("Time")]
    public float preDelay;
    public float postDelay;
    public float coolDown;
    public bool isCooling;

    [Header("Projectile Info")]
    public Projectile proj;
    public float speed;
    public Vector3 size;
    public bool isForFriend;
    public SkillEffect skillEffect;

    public IEnumerator Fire(Character caster)
    {
        #region Check Cool Time
        if (isCooling) yield break;
        #endregion

        #region Get Direction
        Vector3? dir = null;
        while (dir.HasValue == false)
        {
            dir = GameManager.instance.GetDirection(caster);
            yield return new WaitForFixedUpdate();
        }
        #endregion

        isCooling = true;

        #region Wait for Pre delay
        caster.ControlState(false);
        yield return new WaitForSeconds(preDelay);
        #endregion

        #region 투사체 발사
        Vector3 spawnPos = caster.transform.position;
        Projectile projectile = Instantiate(proj, spawnPos, Quaternion.identity);
        projectile.Initialize(caster, dir.Value, speed, size, isForFriend, skillEffect);
        #endregion

        #region Wait for Post Delay
        yield return new WaitForSeconds(postDelay);
        caster.ControlState(true);
        #endregion

        yield return new WaitForSeconds(coolDown);

        isCooling = false;
    }

    
}
