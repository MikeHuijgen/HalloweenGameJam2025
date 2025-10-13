using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/DialogData")]
public class DialogData : ScriptableObject
{
    public string speakersName;
    [TextArea]public string dialogText;
}
