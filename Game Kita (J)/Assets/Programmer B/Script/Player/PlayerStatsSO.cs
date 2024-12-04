using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Player Stats")]
    public int maxHealth = 5;
    public int health = 5;
    public int maxArrowCount = 15;
    public int baseArrowCount = 15;
    public int arrowCount = 15;
    public int coinCount = 0;
    public int maxPotionCount = 5;
    public int potionCount = 0;
    public float moveSpeed = 9f;
    public float baseMoveSpeed = 9f;

    [Header("Upgrades")]    
    public int skillUpgrade = 0; //max arrow count
    public int speedUpgrade = 0; //move speed
}
