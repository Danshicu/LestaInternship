using UnityEngine.SceneManagement;

namespace Tools
{
    public static class LevelManager
    {
        public static void ReloadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}