using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThurst = 15f;

    private Animator myAnimator;
    public bool halfHealth = false;
    public bool quarterHealth = false;


    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    readonly int DESTROY_HASH = Animator.StringToHash("Destroy");

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= startingHealth * 1 / 2) { halfHealth = true; }
        if (currentHealth <= startingHealth * 1 / 4) { quarterHealth = true; }

        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThurst);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            myAnimator.SetTrigger(DESTROY_HASH);
            GetComponent<PickUpSpawner>().DropItems();

        }

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
