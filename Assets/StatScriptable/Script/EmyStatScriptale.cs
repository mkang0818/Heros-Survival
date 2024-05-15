using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EmyStat",menuName = "ScriptableObject/EmyStat", order =int.MaxValue)]
public class EmyStatScriptale : ScriptableObject
{
    [SerializeField]
    private int EmyCode;
    public int emyCode { get { return EmyCode; } set { EmyCode = value; } }


    [SerializeField]
    private int EmyCost;
    public int emyCost { get { return EmyCost; } set { EmyCost = value; } }
    [SerializeField]
    private float EmyMaxHP;
    public float emyMaxHP { get { return EmyMaxHP; } set { EmyMaxHP = value; } }
    [SerializeField]
    private float EmyCurHP;
    public float emyCurHP { get { return EmyCurHP; } set { EmyCurHP = value; } }
    [SerializeField]
    private float EmyAttack;
    public float emyAttack { get { return EmyAttack; } set { EmyAttack = value; } }
    [SerializeField]
    private float EmyMaxAttackSp;
    public float emyMaxAttackSp { get { return EmyMaxAttackSp; } set { EmyMaxAttackSp = value; } }
    [SerializeField]
    private float EmyCurAttackSp;
    public float emyCurAttackSp { get { return EmyCurAttackSp; } set { EmyCurAttackSp = value; } }
    [SerializeField]
    private float EmyMoveSp;
    public float emyMoveSp { get { return EmyMoveSp; } set { EmyMoveSp = value; } }
}
