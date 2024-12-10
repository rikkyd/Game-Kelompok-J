using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{

    public float originalSpeed;
    private float freezetimer = 1f;
    private bool isFrozen = false;
    public PlayerStatsSO playerStats;

    // Start is called before the first frame update
    void Start()
    {
        //tambahan dari rizza
        originalSpeed = playerStats.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //tambahan dari rizza untuk boss buaya yang ngeluarin bola pasir hisap
    public void ModifySpeed(float slowAmount, float duration)
    {
        playerStats.moveSpeed *= slowAmount;
        StartCoroutine(ResetSpeedAfterDuration(duration));
    }

    //tambahan untuk boss Harimau yang bikin ngefreeze
    public void Freeze(float duration)
    {
        if (!isFrozen) // Pastikan efek freeze tidak diaktifkan berulang kali
        {
            Debug.Log("Ngefreeze");
            isFrozen = true;
            originalSpeed = playerStats.moveSpeed;
            playerStats.moveSpeed = 0;
            StartCoroutine(UnfreezeAfterDuration(duration));
        }
    }

    private IEnumerator UnfreezeAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Unfreeze();
    }

    private void Unfreeze()
    {
        isFrozen = false;
        playerStats.moveSpeed = originalSpeed;
    }

    private IEnumerator ResetSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerStats.moveSpeed = originalSpeed;
    }
}
