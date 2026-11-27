using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill Tree / Skill")]
public class SkillTree : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;
    public int moneyCost;
    public float upgradeAmount;
    public bool skillUnlocked;
    public List<SkillTree> prerequisites = new();
}
