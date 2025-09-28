using TMPro;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    private int ticketCount;
    public TextMeshProUGUI ticketCountText;
    public int requiredTickets;

    private void Start()
    {
        ticketCount = 0;
    }

    public void AddTicket()
    {
        ticketCount++;
        ticketCountText.text = ticketCount.ToString();
    }

    public bool HasEnoughTickets()
    {
        return ticketCount >= requiredTickets;
    }
}
