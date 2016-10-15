using UnityEngine;
using System.Collections;

public class PlayerEffects : PlayerController {

    private bool isBalloon;

    #region Helper methods
    // Helper method that deals with movement.
    private void movementManager()
    {

        // Updates the speed parameter in the animator to animate the walk
        float speed = Input.GetAxis("Horizontal");
        _anim.SetFloat("Speed", Mathf.Abs(speed));

        // Deals with flippin the player left or right
        if (speed > 0 && !facingRight)
            Flip();
        else if (speed < 0 && facingRight)
            Flip();

        if (_rb.velocity.y < 0)
        {
            _anim.SetBool("Land", true);
        }

        // Check if we need to do player 1 or player 2 controls
        if (gameObject.tag == "Player" && !_ball)
        {
            // Horizontal movement
            Vector2 forceX = Vector2.zero;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                forceX = new Vector2(1, 0f);

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                forceX = new Vector2(-1, 0f);
            }
            if (Mathf.Abs(_rb.velocity.x) <= maxSpeed)
            {
                _rb.AddForce(forceX * (accl * _airDrag));
            }
            if (isGrounded())
            {
                _airDrag = 1;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    _jump = new Vector2(0f, jumpStrength);
                    _rb.AddForce(_jump, ForceMode2D.Impulse);

                    // Animation for jump
                    _anim.SetTrigger("Jump");
                }
            }
            else {
                _airDrag = 1 / airCtrl;
            }
        }
        else if (gameObject.tag == "Player2" && !_ball)
        {
            // Player 2 keys
            Vector2 forceX = Vector2.zero;
            if (Input.GetKey(KeyCode.D))
            {
                forceX = new Vector2(1, 0f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                forceX = new Vector2(-1, 0f);
            }
            if (Mathf.Abs(_rb.velocity.x) <= maxSpeed)
            {
                _rb.AddForce(forceX * (accl * _airDrag));
            }
            if (isGrounded())
            {
                _airDrag = 1;
                if (Input.GetKey(KeyCode.W))
                {
                    Vector2 jump = new Vector2(0f, jumpStrength);
                    _rb.AddForce(jump, ForceMode2D.Impulse);

                    _anim.SetTrigger("Jump");
                }
            }
            else {
                _airDrag = 1 / airCtrl;
            }
        }

        //moveX = (Mathf.Abs(rb.velocity.x) >= maxSpeed) ? 0 : Input.GetAxis("Horizontal");
        //Vector2 forceX = new Vector2(moveX, 0f);

        // Horizontal movement to player object

    }
    #endregion

}

