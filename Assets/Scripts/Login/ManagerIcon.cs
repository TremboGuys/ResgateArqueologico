using UnityEngine;

class ManagerIcon : MonoBehaviour
{
    public static ManagerIcon Instance { get; private set; }
    private string[] nameImages = new string[] { "alternativeA", "AnubisMask", "dama-warka", "cobra-asteca" };
    private int indexImageSelect = 0;
    [SerializeField] private IconSelected iconInstance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeIndexImage(int num)
    {
        indexImageSelect += num;

        if (indexImageSelect == -1)
        {
            indexImageSelect = nameImages.Length - 1;
        }
        if (indexImageSelect == nameImages.Length)
        {
            indexImageSelect = 0;
        }

        iconInstance.ChangeImage(nameImages[indexImageSelect]);
    }
}