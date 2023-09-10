using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 自动泛型单利模式
/// </summary>
/// <returns></returns>
public class GameAutoinstance<T> : MonoBehaviour where T:MonoBehaviour
{ private static T instance;
public static T Getinstance()
    { if (instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            instance= obj.AddComponent<T>();
             DontDestroyOnLoad(obj);
        
        
        
        }
        return instance;
    
   
    }

}
public class GameAutotest : GameAutoinstance<GameAutotest>//定义新类构建方法，调用单例
{
    // Start is called before the first frame update

    public void GameTest000()
    {

        Debug.Log("fuck you");


    }
}