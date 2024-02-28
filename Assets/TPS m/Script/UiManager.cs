using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public Button startHostButton;
    public Button startServerButton;
    public Button startClientButton;
    public TextMeshProUGUI playerInside;

    public GameObject mainCam;

    


    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Start()
    {

        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
               mainCam.SetActive(false);
               Debug.Log("Host Started...");
            }

            else
            {

            }

        });

        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {

            }

            else
            {

            }

        });

        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {

            }

            else
            {

            }

        });

    }
}
