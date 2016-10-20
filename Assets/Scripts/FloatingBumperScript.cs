using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

///<summary>
/// This class is responsible for the floating bumper logic.
/// Floating bumpers are the bumpers that change position when a button is pressed
///</summary>

class FloatingBumperScript : BumperScript {
    public float ChangeHeight;
    public float MoveSpeed;

    private bool _translate;
    private float _increment;

    // This method is for initialization
    protected override void Start() {
        base.Start();
        _increment = 0;
    }

    void FixedUpdate() {
        moveBumper();
    }

    // This method triggers the bumper into overcharged bumper and returns the boolean status
    public override bool Trigger() {
        this._translate = true;
        return base.Trigger();
    }

    // This method un-triggers the bumper from overcharged to normal and returns the boolean status
    public override bool UnTrigger() {
        this._translate = false;
        return base.UnTrigger();
    }

    #region Helper methods

    private void moveBumper()
    {
        // Moves bumper up if ChangeHeight is positive
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
        // Moves bumper down if ChangeHeight is negative
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
