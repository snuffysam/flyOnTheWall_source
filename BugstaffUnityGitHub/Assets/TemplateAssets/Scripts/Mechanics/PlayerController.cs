using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public static bool hasInvisibility = true;
        public static bool hasExtraJumps = true;
        public static bool hasPepperSpray = true;
        public static bool hasBackJump = true;

        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        Vector2 moveOverride;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;
        private bool chargingWeapon = false;
        private float weaponCharge = 0f;
        private float maxWeaponCharge = 1f;
        public GameObject bugChargeVisual;
        public GameObject parabolaVisual;
        private float bugChargeRelativeX;
        public GameObject bugShotPrefab;
        private bool fired = false;
        private bool weaponButton = false;

        public GameObject listenerPrefab;
        public GameObject aromaParticlePrefab;
        public GameObject pepperSprayPrefab;
        public GameObject reflectHandPrefab;

        public static string currentCheckpoint;
        public static float currentCheckX;
        public static float currentCheckY;
        public static bool currentCheckDir;
        private float invisTimer;
        private float invisMaxTimer = 0.75f;
        private bool currentlyInvisible;
        private bool canFirePepperSpray;
        private bool isBackJumping;

        public GameObject indicatorPrefab;
        private List<GameObject> indicators;
        private float indicatorTimer;
        private float indicatorMaxTimer = 0.05f;

        TextboxScript tbs;

        public static void ResetPowerups(){
            PlayerController.hasInvisibility = false;
            PlayerController.hasExtraJumps = false;
            PlayerController.hasPepperSpray = false;
            PlayerController.hasBackJump = false;
        }

        public static void AllPowerups(){
            PlayerController.hasInvisibility = true;
            PlayerController.hasExtraJumps = true;
            PlayerController.hasPepperSpray = true;
            PlayerController.hasBackJump = true;
        }

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            indicators = new List<GameObject>();
            bugChargeRelativeX = bugChargeVisual.transform.localPosition.x;
            Instantiate<GameObject>(listenerPrefab).GetComponent<FollowScript>().go = this.gameObject;
            moveOverride = Vector2.zero;
            indicatorMaxTimer = 99999f;
        }

        protected override void Update()
        {
            if (tbs == null){
                tbs = FindObjectOfType<TextboxScript>();
            }

            if (controlEnabled)
            {
                if (hasInvisibility){
                    if (Input.anyKey || !IsGrounded){
                        invisTimer = 0f;
                        currentlyInvisible = false;
                    } else {
                        invisTimer += Time.deltaTime;
                        if (invisTimer > invisMaxTimer){
                            currentlyInvisible = true;
                        }
                    }
                }

                if (Input.GetAxis("Fire1") > 0f){
                    weaponButton = true;
                }
                if (Input.GetAxis("Fire1") <= 0f){
                    weaponButton = false;
                }
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BackJump_Character")){
                    isBackJumping = false;
                }

                if (weaponButton){
                    chargingWeapon = true;
                    fired = false;

                    weaponCharge += Time.deltaTime*0.8f;
                    if (weaponCharge > maxWeaponCharge){
                        weaponCharge = maxWeaponCharge;
                    }

                    bugChargeVisual.SetActive(true);
                    parabolaVisual.SetActive(true);
                    bugChargeVisual.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
                    Vector3 bugPos = Vector3.zero;
                    if (spriteRenderer.flipX){
                        bugPos = new Vector3(-bugChargeRelativeX, bugChargeVisual.transform.localPosition.y, 0f);
                        bugChargeVisual.transform.localPosition = bugPos;
                        parabolaVisual.transform.localPosition = bugPos;
                        parabolaVisual.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                        bugChargeVisual.transform.Rotate(0f, 0f, weaponCharge*Time.deltaTime*1500f);
                    } else {
                        bugPos = new Vector3(bugChargeRelativeX, bugChargeVisual.transform.localPosition.y, 0f);
                        bugChargeVisual.transform.localPosition = bugPos;
                        parabolaVisual.transform.localPosition = bugPos;
                        parabolaVisual.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                        bugChargeVisual.transform.Rotate(0f, 0f, -weaponCharge*Time.deltaTime*1500f);
                    }
                    parabolaVisual.transform.localScale = new Vector3((weaponCharge+0.01f)*22f, 2f, 1f);

                    move.x = 0f;

                    indicatorTimer += Time.deltaTime;
                    if (indicatorTimer > indicatorMaxTimer){
                        indicatorTimer = 0f;
                        GameObject ind = Instantiate<GameObject>(indicatorPrefab);
                        ind.transform.position = transform.position + bugPos;
                        if (spriteRenderer.flipX){
                            ind.GetComponent<Rigidbody2D>().velocity = new Vector3(-weaponCharge*25f, 0f, 0f);
                        } else {
                            ind.GetComponent<Rigidbody2D>().velocity = new Vector3(weaponCharge*25f, 0f, 0f);
                        }
                        indicators.Add(ind);
                    }
                } else if (!chargingWeapon && !animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Character")) {
                    move.x = Input.GetAxis("Horizontal");
                    if (move.x > 0.01f){
                        spriteRenderer.flipX = false;}
                    else if (move.x < -0.01f){
                        spriteRenderer.flipX = true;}

                    if ((jumpState == JumpState.Grounded || hasExtraJumps) && Input.GetButtonDown("Jump")){
                        if (jumpState != JumpState.Grounded){
                            float partNum = Random.Range(4f, 8f);
                            for (int i = 0; i < partNum; i++){
                                GameObject go = Instantiate<GameObject>(aromaParticlePrefab);
                                go.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.6f, 0f), 0f);
                            }
                        }
                        jumpState = JumpState.PrepareToJump;
                    } else if (Input.GetButtonUp("Jump")){
                        stopJump = true;
                        Schedule<PlayerStopJump>().player = this;
                    } else if (hasPepperSpray && Input.GetButtonDown("Fire2") && !animator.GetCurrentAnimatorStateInfo(0).IsName("FireBreathe_Character")){
                        canFirePepperSpray = true;
                        animator.SetTrigger("fireBreathe");
                    } else if (canFirePepperSpray && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.25f){
                        for (int i = 0; i < 15; i++){
                            GameObject go = Instantiate<GameObject>(pepperSprayPrefab);
                            if (spriteRenderer.flipX){
                                go.transform.position = transform.position + new Vector3(0.475f*-1f, 0.24f);
                            } else {
                                go.transform.position = transform.position + new Vector3(0.475f*1f, 0.24f);
                            }
                            go.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
                        }
                        canFirePepperSpray = false;
                    } else if (hasBackJump && Input.GetButtonDown("Fire3")){
                        animator.SetTrigger("backJump");
                        isBackJumping = true;
                        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BackJump_Character")){
                            GameObject go = Instantiate<GameObject>(reflectHandPrefab);
                            if (spriteRenderer.flipX){
                                go.transform.position = transform.position + new Vector3(0.75f*-1f, 0f);
                            } else {
                                go.transform.position = transform.position + new Vector3(0.75f*1f, 0f);
                            }
                        }
                    }
                } else {
                    FireBug();
                }
            }
            else
            {
                currentlyInvisible = false;
                isBackJumping = false;
                invisTimer = 0f;
                move.x = 0;
                if (moveOverride.magnitude > 0){
                    move = moveOverride;
                }
                if (weaponButton){
                    FireBug();
                }
            }

            animator.SetBool("chargingGun", chargingWeapon);
            animator.SetBool("invisibility", currentlyInvisible);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player-Death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && tbs.IsEmpty()){
                Respawn();
            }

            UpdateJumpState();
            base.Update();
        }

        public void FireBug(){
            weaponButton = false;
            chargingWeapon = false;

            if (!controlEnabled){
                fired = true;
            }

            if (!fired){
                fired = true;
                GameObject bugShot = Instantiate<GameObject>(bugShotPrefab);
                bugShot.transform.position = bugChargeVisual.transform.position;
                bugShot.transform.rotation = bugChargeVisual.transform.rotation;
                bugShot.GetComponent<SpriteRenderer>().flipX = bugChargeVisual.GetComponent<SpriteRenderer>().flipX;
                if (bugShot.GetComponent<SpriteRenderer>().flipX){
                    bugShot.GetComponent<Rigidbody2D>().velocity = new Vector3(-weaponCharge*25f, 0f, 0f);
                    bugShot.GetComponent<Rigidbody2D>().angularVelocity = weaponCharge*1500f;
                } else {
                    bugShot.GetComponent<Rigidbody2D>().velocity = new Vector3(weaponCharge*25f, 0f, 0f);
                    bugShot.GetComponent<Rigidbody2D>().angularVelocity = -weaponCharge*1500f;
                }
                AudioHandlerScript.PlayClipAtPoint("MenuScroll", "MenuScroll", 0.8f, transform.position);
            }

            bugChargeVisual.SetActive(false);
            parabolaVisual.SetActive(false);
            weaponCharge = 0f;

            foreach (GameObject ind in indicators){
                Destroy(ind);
            }
            indicators = new List<GameObject>();
        }

        void Respawn(){
            GameObject go = new GameObject();
            go.AddComponent<DoorHelper>();
            go.GetComponent<DoorHelper>().xPosLoad = currentCheckX;
            go.GetComponent<DoorHelper>().yPosLoad = currentCheckY;
            go.GetComponent<DoorHelper>().loadDirection = currentCheckDir;
            SceneManager.LoadScene(currentCheckpoint);
            health.Heal();
        }

        public void SetCheckpoint(string sceneName, float xPos, float yPos, bool dir){
            currentCheckpoint = sceneName;
            currentCheckX = xPos;
            currentCheckY = yPos;
            currentCheckDir = dir;
        }

        public bool IsInvisible(){
            return currentlyInvisible;
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        public void SetMove(Vector2 newMove){
            moveOverride = newMove;
        }

        protected override void ComputeVelocity()
        {
            if (jump && (IsGrounded || hasExtraJumps))
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }
            
            if (isBackJumping){
                move.x = -2.5f;
                if (spriteRenderer.flipX){
                    move.x *= -1f;
                }
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        public void PlayerDeath(){
            if (weaponButton){
                FireBug();
            }
            health.Die();
            //model.virtualCamera.m_Follow = null;
            //model.virtualCamera.m_LookAt = null;
            // player.collider.enabled = false;
            controlEnabled = false;

            if (audioSource && ouchAudio){
                audioSource.PlayOneShot(ouchAudio);
            }
            if (!animator.GetBool("dead") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player-Death")){
                animator.SetTrigger("hurt");
                animator.SetBool("dead", true);
                AudioHandlerScript.PlayDeathAtPoint(transform.position, 0);
            }
            //Simulation.Schedule<PlayerSpawn>(2);
        }

        public void PlayerHit(){
            if (weaponButton){
                FireBug();
            }
            if (!animator.GetBool("dead") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player-Death")){
                animator.SetTrigger("hurt");
                AudioHandlerScript.PlayHitAtPoint(transform.position, 0);
                //AudioHandlerScript.PlayClipAtPoint("MenuScroll", "MenuScroll", 0.8f, transform.position);
            }
        }
    }
}