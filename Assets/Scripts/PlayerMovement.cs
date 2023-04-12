using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject attack;
    [SerializeField] Transform attackSpawnPoint;

    [SerializeField] GameObject spikes;
    [SerializeField] GameObject bigAttack;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myBodyCollider;
    float gravityScaleAtStart;

    bool isAlive = true;
    bool isInvulnerable = false;

    bool isThreeShotsSuperPowerActive = false;
    int threeShotsSuperPowerCounter = 0;

    bool isSpikesSuperPowerActive = false;

    bool isBigShotSuperPowerActive = false;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }


    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        if (isThreeShotsSuperPowerActive == true) {
            StartCoroutine(ThreeShots());
        } else {
            Instantiate(attack, attackSpawnPoint.position, transform.rotation);
        }
    }

    void OnSpikesSuperPower(InputValue value)
    {
        if (!isAlive) { return; }
        if (isSpikesSuperPowerActive == true) {
            if (transform.localScale.x == 1) {
                Instantiate(spikes, new Vector3(attackSpawnPoint.position.x + 3f, attackSpawnPoint.position.y + 1.5f, attackSpawnPoint.position.z), transform.rotation);
            } else {
                Instantiate(spikes, new Vector3(attackSpawnPoint.position.x - 3f, attackSpawnPoint.position.y + 1.5f, attackSpawnPoint.position.z), transform.rotation);
            }
        }
    }

    void OnBigShotSuperPower(InputValue value)
    {
        if (isBigShotSuperPowerActive == true) {
            if (transform.localScale.x == 1) {
                Instantiate(bigAttack, new Vector3(attackSpawnPoint.position.x + 1.5f, attackSpawnPoint.position.y + 1f, attackSpawnPoint.position.z), transform.rotation);
            } else {
                Instantiate(bigAttack, new Vector3(attackSpawnPoint.position.x - 1.5f, attackSpawnPoint.position.y + 1f, attackSpawnPoint.position.z), Quaternion.Euler(0f, 180f, 0f));
            }
        }
    }

    IEnumerator ThreeShots() {
		while (true) {
            Instantiate(attack, attackSpawnPoint.position, transform.rotation);
            threeShotsSuperPowerCounter++;
            if (threeShotsSuperPowerCounter == 3) {
                threeShotsSuperPowerCounter = 0;
                yield break;
            }
			yield return new WaitForSeconds(0.150F);
		}
	}

    public void ActivateThreeShotsSuperPower()
    {
        if (!isAlive) { return; }
        isThreeShotsSuperPowerActive = true;
    }

    public void ActivateSpikesSuperPower()
    {
        if (!isAlive) { return; }
        isSpikesSuperPowerActive = true;
    }

    public void ActivateBigShotSuperPower()
    {
        if (!isAlive) { return; }
        isBigShotSuperPowerActive = true;
    }

    void OnMove(InputValue value)

    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed) {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")) && isInvulnerable == false) {
            BeInvulnerable();
            // isAlive = false;
            // myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    IEnumerator InvulnerabilityTime()
    {
        yield return new WaitForSeconds(1.5F);
        isInvulnerable = false;
    }

    void BeInvulnerable()
    {
        isInvulnerable = true;
        StartCoroutine(InvulnerabilityTime());
    }
}
