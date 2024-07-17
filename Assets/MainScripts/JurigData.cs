using UnityEngine;

[CreateAssetMenu(fileName = "Jurig", menuName = "Jurig/Jurig Biasa")]
public class JurigData : ScriptableObject
{
    public string jurigName;
    public Sprite jurigImage;
    public JurigRoles jurigRole;
    public float salary;
    public float passiveSalary;
    public float income;
    public float passiveIncome;
    [TextArea]
    public string[] description = new string[1];
}

public enum JurigRoles
{
    Penambah_Duid,
    Penglaris
}
