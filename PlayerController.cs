using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public CharacterController controller;
    public float speed = 10f;
    public float sprintSpeed = 15f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    private Vector3 velocity;
    private bool isGrounded;
    public ItemEquip itemEquip;

    public LayerMask enemyLayer;

    public HealthBar playerHealth;

    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode meleeAttackKey = KeyCode.Mouse0;
    public Transform selectedItem;
    public KeyCode rangedAttackKey = KeyCode.Mouse1;

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Create a new Vector3 called move, that will essentially point in the direction that the player will move
        //If we use transform.Translate, it will move the player on the global variables while this will move on the local var.
        float moveSpeed;

        //Use the controller component to get the player to move
        if (Input.GetKey(sprintKey))
            moveSpeed = sprintSpeed;
        else
            moveSpeed = speed;

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(moveSpeed * Time.deltaTime * move);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(rangedAttackKey))
        if (Input.GetKeyDown(KeyCode.Mouse0))
        if (Input.GetKeyDown(KeyCode.Mouse0))
            FireWeapon();
        else if (Input.GetKeyDown(meleeAttackKey))
            SwingWeapon();
    }

    private void FireWeapon()
    {
        Item selectedItem = Inventory.Instance.GetSelectedItem();
        if (selectedItem.projectile != null) Instantiate(selectedItem.projectile, transform.position, selectedItem.projectile.transform.rotation);
        StartCoroutine(PlayItemAnimation(selectedItem));
    }

    private void SwingWeapon()
    {
        Debug.Log($"[{gameObject.name}] SwingWeapon called.");
        Item selectedItem = Inventory.Instance.GetSelectedItem();
        if (selectedItem == null)
        {
            Debug.LogWarning("No item selected for melee attack!");
            return;
        }

        if (selectedItem.attackStates == null || selectedItem.attackStates.Length == 0)
        {
            Debug.LogWarning($"Selected item {selectedItem.name} has no attack states!");
            return;
        }

        Debug.Log($"Swinging weapon: {selectedItem.name}");

        if (selectedItem.equippedPrefab != null)
        {
            Debug.Log("Starting weapon animation coroutine...");
            StartCoroutine(PlayItemAnimation(selectedItem));
        }
        else
        {
            Debug.LogWarning("Equipped prefab is null. Cannot play animation.");
        }

        AttackHitbox attackHitbox = GetComponent<AttackHitbox>();
        if (attackHitbox != null)
        {
            Debug.Log("Calling PerformHit on AttackHitbox...");
            attackHitbox.PerformHit();
        }
        else
        {
            Debug.LogWarning("No AttackHitbox component found on player.");
        }
    }

    private IEnumerator PlayItemAnimation(Item item)
    {
        // Try to get the UI Image component
        if (!itemEquip.GetEquippedItem().TryGetComponent(out Image img))
        {
            Debug.LogWarning("Equipped prefab has no Image component. Cannot animate.");
            yield break;
        }

        Debug.Log($"Playing animation for {item.name} with {item.attackStates.Length} frames.");

        for (int i = 0; i < item.attackStates.Length; i++)
        {
            img.sprite = item.attackStates[i];
            Debug.Log($"Setting frame {i}: {item.attackStates[i].name}");
            float frameTime = 0.1f; // 0.1 seconds per frame
            yield return new WaitForSeconds(frameTime);

        }

        // Reset to first frame after attack
        img.sprite = item.attackStates[0];
        Debug.Log("Animation finished, reset to first frame.");
    }

    private void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float meleeRange = 2f;
        float attackRadius = 3f;
        Vector3 attackCenter = cam.transform.position + 0.5f * meleeRange * cam.transform.forward;

        // Draw a semi-transparent red sphere
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(attackCenter, attackRadius);

        // Optional: Draw the outline
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter, attackRadius);
    }
}
    