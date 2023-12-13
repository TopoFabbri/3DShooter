using System.Collections;
using Abstracts;
using Game;
using GameStats;
using Patterns;
using Patterns.SM;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Weapons;

namespace Character
{
    /// <summary>
    /// Character controller class
    /// </summary>
    public class PlayerController : MonoBehaviour, IGunHolder
    {
        [SerializeField] private float moveSpeed = 500f;
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float grabDis = 3f;
        [SerializeField] private float barrelDis = 2f;

        [FormerlySerializedAs("godSpeedMultiplier")] [SerializeField]
        private float flashSpeedMultiplier = 2f;

        [SerializeField] private string gunTag = "Gun";

        [Header("Objects:")] [SerializeField] private Gun weapon;
        [SerializeField] private CameraMovement cameraMovement;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private HUD.Hud hud;
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private Id stateId;
        [SerializeField] private PlayerLethalController lethalController;
        [SerializeField] private Stats.Stats stats;
        [SerializeField] private Transform handPlace;

        private Vector3 movement;
        private bool paralyzed;
        private bool ads;
        private bool hasShot;
        private bool flash;

        public Gun Weapon
        {
            get => weapon;
            set => weapon = value;
        }

        public Transform GetHand => handPlace;

        private void OnEnable()
        {
            // Action subscriptions
            InputListener.Move += OnMove;
            InputListener.Shoot += OnShoot;
            InputListener.Aim += OnAim;
            InputListener.Drop += OnDrop;
            InputListener.Grab += OnGrab;
            InputListener.Reload += OnReload;
            InputListener.DropLethal += OnDropLethal;
            InputListener.ChangeLethal += OnChangeLethal;

            Cheats.GodMode += OnGodModeHandler;
            Cheats.Flash += OnFlashHandler;

            stateMachine.Subscribe(stateId, OnUpdate);
        }

        private void OnDisable()
        {
            // Action unsubscription
            InputListener.Move -= OnMove;
            InputListener.Shoot -= OnShoot;
            InputListener.Aim -= OnAim;
            InputListener.Drop -= OnDrop;
            InputListener.Grab -= OnGrab;
            InputListener.Reload -= OnReload;
            InputListener.DropLethal -= OnDropLethal;
            InputListener.ChangeLethal -= OnChangeLethal;

            Cheats.GodMode -= OnGodModeHandler;
            Cheats.Flash -= OnFlashHandler;

            stateMachine.UnSubscribe(stateId, OnUpdate);
        }

        private void FixedUpdate()
        {
            var trans = transform;
            rb.AddForce((movement.x * trans.right + movement.z * trans.forward) * Time.fixedDeltaTime,
                ForceMode.Acceleration);
            var tmp = ClampPlaneVelocity(rb.velocity, maxSpeed);
            rb.velocity = new Vector3(tmp.x, rb.velocity.y, tmp.y);
        }

        /// <summary>
        /// Gameplay-only update
        /// </summary>
        private void OnUpdate()
        {
            if (ads)
                AimStart();
            else
                AimStop();

            if (((IGunHolder)this).Weapon)
            {
                if (((IGunHolder)this).Weapon.IsReloading)
                {
                    var trans = transform;
                    ((IGunHolder)this).Weapon.transform.position = trans.position - trans.forward;
                }
                else
                {
                    Transform cameraMovementTransform;
                    Weapon.transform.localPosition = Vector3.zero;
                        (cameraMovementTransform = cameraMovement.transform).TransformPoint(new Vector3(.26f, -.234f,
                            .561f));
                    ((IGunHolder)this).Weapon.transform.LookAt(cameraMovement.worldMouseDir +
                                                                  cameraMovementTransform.up * -0.1597f);
                }
            }

            hud.SetPickupTextActive(PointingAtGun(out _));
        }

        /// <summary>
        /// Call character move action
        /// </summary>
        /// <param name="input"></param>
        public void OnMove(InputValue input)
        {
            if (paralyzed)
                return;

            Vector2 direction = input.Get<Vector2>();
            movement = new Vector3(direction.x, 0, direction.y) * moveSpeed;
        }

        /// <summary>
        /// Call character shoot action
        /// </summary>
        public void OnShoot()
        {
            ((IGunHolder)this).Shoot();
        }

        /// <summary>
        /// Call character aim action
        /// </summary>
        /// <param name="input"></param>
        public void OnAim(InputValue input)
        {
            ads = input.isPressed;
        }

        /// <summary>
        /// Call character drop weapon action
        /// </summary>
        public void OnDrop()
        {
            ((IGunHolder)this).DropGun();
        }

        /// <summary>
        /// Call character pickup weapon action
        /// </summary>
        public void OnGrab()
        {
            if (!PointingAtGun(out var hit))
                return;

            ((IGunHolder)this).AddGun(hit.transform.gameObject.GetComponent<Gun>());
        }

        /// <summary>
        /// Call character reload weapon action
        /// </summary>
        public void OnReload()
        {
            ((IGunHolder)this).Reload();
        }

        /// <summary>
        /// Call place equipped lethal
        /// </summary>
        private void OnDropLethal()
        {
            var position = transform.position;
            var camTrans = cameraMovement.transform;

            lethalController.SpawnLethal(position + camTrans.forward * barrelDis, camTrans.rotation);
        }

        /// <summary>
        /// Call change lethal
        /// </summary>
        /// <param name="diff">Amount scrolled</param>
        private void OnChangeLethal(float diff)
        {
            switch (diff)
            {
                case > 0:
                    lethalController.CurrentLethal++;
                    break;
                case < 0:
                    lethalController.CurrentLethal--;
                    break;
            }
        }

        /// <summary>
        /// Clamp x - z velocity
        /// </summary>
        /// <param name="vel"></param>
        /// <param name="clampValue"></param>
        /// <returns>x & z velocity normalized within 'clampValue'</returns>
        private static Vector2 ClampPlaneVelocity(Vector3 vel, float clampValue)
        {
            var res = new Vector2(vel.x, vel.z);
            res = Vector2.ClampMagnitude(res, clampValue);
            return res;
        }

        /// <summary>
        /// Set up state 'aiming'
        /// </summary>
        private void AimStart()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            cameraMovement.aimDownSight = true;
        }

        /// <summary>
        /// End character state 'aiming'
        /// </summary>
        private void AimStop()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            cameraMovement.aimDownSight = false;
        }

        /// <summary>
        /// Check if can grab gun
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        private bool PointingAtGun(out RaycastHit hit)
        {
            var cam = cameraMovement.gameObject.transform;
            var ray = new Ray(cam.position, cam.forward);

            if (!Physics.Raycast(ray, out hit)) return false;

            return hit.transform.gameObject.CompareTag(gunTag) &&
                   Vector3.Distance(hit.transform.position, transform.position) < grabDis;
        }

        /// <summary>
        /// Sets player velocity to 0
        /// </summary>
        public void StopMovement()
        {
            rb.velocity = Vector3.zero;
        }

        /// <summary>
        /// Toggles god mode
        /// </summary>
        private void OnGodModeHandler()
        {
            stats.ToggleGodMode();
        }

        /// <summary>
        /// Toggles flash
        /// </summary>
        private void OnFlashHandler()
        {
            flash = !flash;

            if (flash)
            {
                maxSpeed *= flashSpeedMultiplier;
                moveSpeed *= flashSpeedMultiplier;
            }
            else
            {
                maxSpeed /= flashSpeedMultiplier;
                moveSpeed /= flashSpeedMultiplier;
            }
        }

        /// <summary>
        /// Paralyze player
        /// </summary>
        public void Paralyze(float time)
        {
            rb.velocity = Vector3.zero;
            movement = Vector3.zero;
            paralyzed = true;
            cameraMovement.Paralyzed = true;
            hud.SetParalyzedHud(true);
            StartCoroutine(StopParalyzeOnTime(time));
        }

        /// <summary>
        /// Wait for time and stop paralyze effect
        /// </summary>
        /// <param name="time">Time to wait</param>
        /// <returns></returns>
        private IEnumerator StopParalyzeOnTime(float time)
        {
            float endTime = GameTimeCounter.Instance.GameTime + time;

            yield return new WaitUntil(() => GameTimeCounter.Instance.GameTime > endTime);

            cameraMovement.Paralyzed = false;
            paralyzed = false;
            hud.SetParalyzedHud(false);
        }

        /// <summary>
        /// Play step sound
        /// </summary>
        public void Step()
        {
            AkSoundEngine.PostEvent("PlayStep", gameObject);
        }
    }
}