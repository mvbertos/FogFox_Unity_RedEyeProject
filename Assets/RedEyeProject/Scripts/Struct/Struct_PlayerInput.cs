using UnityEngine;

[System.Serializable]
public struct Struct_PlayerInput
{
    [Header("Movementation")]
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Dash;
    [Header("Insteraction")]
    public KeyCode Interact;
    [Header("Combat")]
    public KeyCode MeleeFast;
    public KeyCode MeleeStrong;
}
