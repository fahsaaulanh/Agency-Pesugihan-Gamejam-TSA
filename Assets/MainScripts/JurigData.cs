using UnityEngine;

[CreateAssetMenu(fileName = "Jurig biasa", menuName = "Tycoon Game/Jurig")]
public class JurigData : ScriptableObject
{
    public string jurigName;
    public float salary;
    public float income;
    [TextArea]
    public string description;
}
