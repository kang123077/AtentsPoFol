using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
                _anim = GetComponentInChildren<Animator>();
            return _anim;
        }
        set { _anim = value; }
    }
}
