using UnityEngine;

public class BaseMenu : MonoBehaviour
{
    protected void Settings(GameObject self, GameObject settings)
    {
        self.SetActive(false);
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
#if DEBUG
        Debug.Break();
#endif
    }
}
