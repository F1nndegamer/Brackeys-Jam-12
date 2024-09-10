using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene = 0,
        LoadScene = 1,
        GameScene = 2,
    }
    private static Scene targetScene;
    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene((int)Scene.LoadScene);
    }
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene((int)targetScene);
    }
}
