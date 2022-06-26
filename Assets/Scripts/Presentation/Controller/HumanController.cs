using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Github.Yaroyan.Constant;

namespace Com.Github.Yaroyan.Rpg.Controller
{
    public class HumanController : MonoBehaviour, IPlayable
    {
        [SerializeField] Rigidbody2D _rigidbody2d;
        [SerializeField] Animator _animator;
        [SerializeField] float _moveSpeed = 1.0f;

        void Start()
        {

        }

        void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// キャラクターを移動します。
        /// </summary>
        void Move()
        {
            float axisX = Input.GetAxisRaw(InputKeys.Horizontal.ToString());
            float axisY = Input.GetAxisRaw(InputKeys.Vertical.ToString());
            _rigidbody2d.velocity = new Vector2(axisX, axisY) * _moveSpeed;
            AnimateMove();
        }

        /// <summary>
        /// キャラクターの移動アニメーションを開始します。
        /// </summary>
        void AnimateMove()
        {
            float axisX = Input.GetAxisRaw(InputKeys.Horizontal.ToString());
            float axisY = Input.GetAxisRaw(InputKeys.Vertical.ToString());
            _animator.SetFloat(AnimatorParameters.AxisX.ToString(), _rigidbody2d.velocity.x);
            _animator.SetFloat(AnimatorParameters.AxisY.ToString(), _rigidbody2d.velocity.y);
            if (axisX == 1 || axisX == -1 || axisY == 1 || axisY == -1)
            {
                _animator.SetFloat(AnimatorParameters.LastAxisX.ToString(), axisX);
                _animator.SetFloat(AnimatorParameters.LastAxisY.ToString(), axisY);
            }
        }
    }
}
