using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Player Stats")]
    public int maxHealth = 5;
    public int health = 5;
    public int maxArrowCount = 15;
    public int arrowCount = 15;
    public int coinCount = 0;
    public int potionCount = 0;
    public float moveSpeed = 1f;

    [Header("Upgrades")]    
    public int maxHealthUpgrade = 1;
    public int maxArrowCountUpgrade = 1;
    public int moveSpeedUpgrade = 1;
}
