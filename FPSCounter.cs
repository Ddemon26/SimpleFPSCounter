using UnityEngine;

/// <summary>
/// FPSCounter measures and displays frames per second in a Unity scene.
/// Attach this script to an empty GameObject in the initial scene.
/// </summary>
public class FPSCounter : MonoBehaviour
{
    private GUIStyle fpsDisplayStyle;
    private Rect fpsDisplayRect;

    private int frameCounter;
    private float timeSinceLastUpdate;
    private double currentFPS;

    private const float UpdateInterval = 0.5f;
    private const int ScreenSizeChangeThreshold = 10;
    private int screenMaxDimension;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// It initializes the UI style and ensures that the GameObject is not destroyed on scene changes.
    /// </summary>
    /// <remarks>
    /// This method is useful for setting up the initial state of the FPSCounter and GUI style.
    /// </remarks>
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitializeUIStyle();
        AdjustUIForScreenSize();
    }

    /// <summary>
    /// Update is called once per frame. It updates the FPS counter and checks for screen size changes.
    /// </summary>
    /// <remarks>
    /// This method is essential for continuously tracking and displaying FPS in the scene.
    /// </remarks>
    private void Update()
    {
        UpdateFPSCounter();
        CheckForScreenSizeChange();
    }

    /// <summary>
    /// Initializes the GUI style used for displaying FPS information.
    /// </summary>
    /// <remarks>
    /// This method is responsible for setting up the visual style of the FPS display.
    /// </remarks>
    private void InitializeUIStyle()
    {
        fpsDisplayStyle = new GUIStyle
        {
            fontSize = CalculateFontSize(),
            normal = { textColor = Color.white }
        };
    }

    /// <summary>
    /// Updates the FPS counter by counting frames and calculating FPS over time intervals.
    /// </summary>
    /// <remarks>
    /// This method calculates and updates the current FPS value for display.
    /// </remarks>
    private void UpdateFPSCounter()
    {
        frameCounter++;
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= UpdateInterval)
        {
            currentFPS = System.Math.Round(frameCounter / timeSinceLastUpdate, 1, System.MidpointRounding.AwayFromZero);
            frameCounter = 0;
            timeSinceLastUpdate = 0;
        }
    }

    /// <summary>
    /// Checks for changes in screen size and adjusts UI elements accordingly.
    /// </summary>
    /// <remarks>
    /// This method monitors the screen size and adjusts the UI to ensure proper display.
    /// </remarks>
    private void CheckForScreenSizeChange()
    {
        if (Mathf.Abs(screenMaxDimension - Mathf.Max(Screen.width, Screen.height)) > ScreenSizeChangeThreshold)
        {
            AdjustUIForScreenSize();
        }
    }

    /// <summary>
    /// Adjusts UI elements for the current screen size.
    /// </summary>
    /// <remarks>
    /// This method is responsible for resizing and repositioning UI elements based on screen size.
    /// </remarks>
    private void AdjustUIForScreenSize()
    {
        screenMaxDimension = Mathf.Max(Screen.width, Screen.height);
        fpsDisplayRect = new Rect(10, 10, screenMaxDimension / 10, screenMaxDimension / 30);
        fpsDisplayStyle.fontSize = CalculateFontSize();
    }

    /// <summary>
    /// Calculates the font size based on the screen size.
    /// </summary>
    /// <returns>The calculated font size.</returns>
    /// <remarks>
    /// This method is used to determine the appropriate font size for the FPS display based on screen dimensions.
    /// </remarks>
    private int CalculateFontSize()
    {
        return screenMaxDimension / 36;
    }

    /// <summary>
    /// OnGUI is called for rendering and displaying the FPS counter on the screen.
    /// </summary>
    /// <remarks>
    /// This method handles the rendering and display of the current FPS value on the screen.
    /// </remarks>
    private void OnGUI()
    {
        GUI.Box(fpsDisplayRect, string.Empty);
        GUI.Label(fpsDisplayRect, $"{currentFPS} FPS", fpsDisplayStyle);
    }
}
