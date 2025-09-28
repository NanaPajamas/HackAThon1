using UnityEngine;

public class ArcadeMachine : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 3f; // How close the player needs to be
    public KeyCode interactionKey = KeyCode.E; // Key to press for interaction

    public GameObject ticketRequirement;
    public GameObject gameWinScreen;
    public TicketManager ticketManager;

    private Transform player;

    private void Start()
    {
        // Find the player by tag (make sure your player has the "Player" tag)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Player not found! Make sure your player has the 'Player' tag.");
    }

    private void Update()
    {
        if (player == null) return;

        // Check distance between player and this object
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionDistance)
        {
            ticketRequirement.SetActive(true);

            if (Input.GetKeyDown(interactionKey))
            {
                Interact();
            }
        }
        else
        {
            if (ticketRequirement.activeSelf) ticketRequirement.SetActive(false);
        }
    }

    // This function is called when the player interacts
    private void Interact()
    {
        if (ticketManager.HasEnoughTickets())
            WinGame();
    }

    private void WinGame()
    {
        gameWinScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
