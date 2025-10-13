using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/DialogData")]
public class DialogData : ScriptableObject
{
    [TextArea]public string dialogText;
}
