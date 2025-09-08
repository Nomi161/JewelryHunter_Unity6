using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;    // 切り替えたいシーン名を氏名

    // シーンを切り替える機能をもったメソッド作成
    public void Load()
    {
        // 引数に指定した名前のシーン切り替えのメソッド呼び出し
        SceneManager.LoadScene(sceneName);
    }
}
