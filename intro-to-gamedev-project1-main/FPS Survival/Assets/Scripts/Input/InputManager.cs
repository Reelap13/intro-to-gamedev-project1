using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Singleton instance
    private static InputManager instance;

    // Input system instance
    private InputMaster inputMaster;

    // Public accessor for the singleton instance
    public static InputManager Instance
    {
        get
        {
            // If the instance is null, find it or create it
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();

                // If there is no InputManager object in the scene, create one
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(InputManager).Name);
                    instance = singletonObject.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Assign the input system instance
        inputMaster = new InputMaster();

        // Ensure the object is not destroyed when loading a new scene
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    // Method to access the InputMaster instance
    public InputMaster GetInputMaster()
    {
        return inputMaster;
    }
}
