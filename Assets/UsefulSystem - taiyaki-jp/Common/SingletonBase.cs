using System;
using UnityEngine;

namespace UsefulSystem.Common
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
    {
        protected static T _instance;

        protected static void SetInstance(T instance)
        {
            if (_instance == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} is already initialized");
            }
            _instance = instance;
        }
        protected static T GetInstance()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} is not initialized");
            }

            return _instance;
        }
        /*public static T Instance
        {
            get
            {
                if (_instance == null) //インスタンスがなければこっちに入る
                {
                    var obj = FindObjectOfType<T>(); //Tを探してくる
                    if (obj == null) //なければエラー
                    {
                        Debug.LogError(typeof(T) + "をアタッチしてあるGameObjectがないよー");
                    }
                    else //あればインスタンスに
                        _instance = obj;
                }

                return _instance; //基本すぐこれ
            }
        }*/

        //よくあるシングルトンAwake
        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
