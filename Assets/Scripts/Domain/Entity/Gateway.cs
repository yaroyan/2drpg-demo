using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Github.Yaroyan.Rpg
{
    public class Gateway : MonoBehaviour
    {
        [SerializeField] Entry _departure;
        [SerializeField] Entry _arrival;
        [SerializeField] bool _isOneWay;

        void Start()
        {
            Deactivate();
        }

        /// <summary>
        /// 一方通行の場合はオブジェクトを無効化する。 
        /// </summary>
        void Deactivate()
        {
            if (_isOneWay) this.gameObject.SetActive(!_isOneWay);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (SceneManager.GetActiveScene().name == _arrival.Scene.Name)
            {
                Warp(other);
            }
            else
            {
                LoadScene();
            }
        }

        /// <summary>
        /// キャラクターを遷移ポイントの座標に生成する。
        /// </summary>
        void Spawn()
        {

        }

        /// <summary>
        /// キャラクターを同一シーン内の遷移先の座標に移動させる。
        /// </summary>
        /// <param name="other"></param>
        void Warp(Collider2D other)
        {
            if (other.TryGetComponent<IGatewayActor>(out var actor))
            {
                actor.LeaveFor(_arrival);
            }
            other.gameObject.transform.position = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// 新規シーンをロードする。
        /// </summary>
        void LoadScene()
        {
            Spawn();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(_arrival.Scene.Name);
        }

        /// <summary>
        /// シーンロード完了後に実行する処理。
        /// OnEnable後に呼び出しされる。
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

    }
}