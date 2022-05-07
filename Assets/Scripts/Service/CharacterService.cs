using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Github.Yaroyan.Rpg.Repository;
using UnityEngine.Networking;
using Microsoft.Data.Sqlite;

namespace Com.Github.Yaroyan.Rpg.Service
{
    public class CharacterService : MonoBehaviour, VContainer.Unity.IInitializable
    {
        [VContainer.Inject]
        readonly CharacterRepository _characterRepository;
        [SerializeField] GameObject characterPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _characterRepository.TestFind();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// IDからキャラクターを生成します。
        /// </summary>
        /// <param name="id">キャラクターID</param>
        /// <returns></returns>
        public GameObject CreateCharacter(string id)
        {
            _characterRepository.FindById<System.Object>(id);
            GameObject gameObject = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
            gameObject.name = "player";
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("");
            gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("");
            return gameObject;
        }

        public void Initialize()
        {

        }
    }
}
