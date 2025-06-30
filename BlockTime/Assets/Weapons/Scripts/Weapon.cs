using UnityEngine;

public abstract class Weapon: MonoBehaviour
{
    public WeaponData weaponData;
    public AnimationClip weaponIdle;
    public AnimationClip weaponAttack;
    public float pivotOffset = 0.0f;

    protected Animator animator;
    protected AnimatorOverrideController overrideController;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
    }

    protected virtual void Start()
    {
        updateAnimations();

        // Calculate the new position for the weapon based on the pivotOffset
        WeaponParent par = transform.parent.GetComponent<WeaponParent>();
        Vector2 parDirection = par.getDirection();
        transform.position += new Vector3(parDirection.x, parDirection.y, 0) * pivotOffset;
    }

    public void updateAnimations()
    {
        // Note, the override controller overrides using the name of the original animationClip, not the state name
        overrideController["TutorialSwordIdle"] = weaponIdle;
        overrideController["TutorialSwordAttack"] = weaponAttack;
    }

    public abstract void Attack();
}