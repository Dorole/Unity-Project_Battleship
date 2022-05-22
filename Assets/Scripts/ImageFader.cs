using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class ImageFader : MonoBehaviour
    {
        Animator _anim;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void FadeOut()
        {
            _anim.SetTrigger("FadeOut");
        }
    }
}
