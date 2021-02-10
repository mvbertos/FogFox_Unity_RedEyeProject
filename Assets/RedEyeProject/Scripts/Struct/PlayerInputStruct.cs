using UnityEngine;

[System.Serializable]
public class PlayerInputStruct
{
    [Header("Movementation")]
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Dash;
}
