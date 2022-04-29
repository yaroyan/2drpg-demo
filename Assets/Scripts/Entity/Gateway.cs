using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Github.Yaroyan.Constant;
using Com.Github.Yaroyan.Rpg.Service;
using UnityEngine.SceneManagement;

namespace Com.Github.Yaroyan
{
    public class Gateway : MonoBehaviour
    {
        [SerializeField] Entry _departure;
        [SerializeField] Entry _arrival;
        [SerializeField] bool _isOneWay;
        [SerializeField] CharacterService characterService;
        void Start()
        {
            // if (PlayerPrefs.GetInt(PlayerPrefsKeys.Departure.ToString(), -1) == _departure.Id)
            // {
            //     Spawn();
            //     // 一方通行の場合はオブジェクトを無効化する。
            //     this.gameObject.SetActive(!_isOneWay);
            // }
            // else
            // {

            // }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == GameObjectTags.Player.ToString())
                Transit(other);
        }

        void Transit(Collider2D other)
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
            GameObject player = characterService.CreateCharacter("0");
            player.transform.position = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// キャラクターを同一シーン内の遷移先の座標に移動させる。
        /// </summary>
        /// <param name="other"></param>
        void Warp(Collider2D other)
        {
            other.gameObject.transform.position = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// 新規シーンをロードする。
        /// </summary>
        void LoadScene()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.Arrival.ToString(), _departure.Id);
            PlayerPrefs.SetInt(PlayerPrefsKeys.Departure.ToString(), _arrival.Id);
            SceneManager.LoadScene(_arrival.Scene.Name);
        }
    }
}