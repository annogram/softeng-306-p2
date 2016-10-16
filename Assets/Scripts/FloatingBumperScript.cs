using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class FloatingBumperScript : BumperScript {
    public float ChangeHeight;
    public float MoveSpeed;

    private bool _translate;
    private float _increment;

    protected override void Start() {
        base.Start();
        _increment = 0;
    }

    void FixedUpdate() {
        moveBumper();
    }

    public override bool Trigger() {
        this._translate = true;
        return base.Trigger();
    }

    public override bool UnTrigger() {
        this._translate = false;
        return base.UnTrigger();
    }

    #region Helper methods

    private void moveBumper()
    {
        if (ChangeHeight > 0)
        {
            if (_translate && _increment != ChangeHeight)
            {
                this.transform.Translate(Vector2.up * MoveSpeed);
                _increment++;
            }
            else if (!_translate && _increment != 0)
            {
                this.transform.Translate(Vector2.down * MoveSpeed);
                _increment--;
            }
        }
        else
        {
            if (_translate && _increment != ChangeHeight)
            {
                this.transform.Translate(Vector2.down * MoveSpeed);
                _increment--;
            }
            else if (!_translate && _increment != 0)
            {
                this.transform.Translate(Vector2.up * MoveSpeed);
                _increment++;
            }
        }
        
    }
    #endregion
}

