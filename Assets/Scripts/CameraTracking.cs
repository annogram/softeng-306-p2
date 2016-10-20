    using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class CameraTracking : MonoBehaviour
    //// http://answers.unity3d.com/questions/674225/2d-camera-to-follow-two-players.html
    //// http://answers.unity3d.com/questions/912621/make-a-camera-to-follow-two-players.html
    {
        public Transform player1, player2;
        public float minSizeY = 5f;
        public float horizontalPadding = 10f;
        public float verticalPadding = 30f;

        void SetCameraPos()
        {
            Vector3 middle = (player1.position + player2.position) * 0.5f;

            GetComponent<Camera>().transform.position = new Vector3(
                middle.x,
                middle.y,
                GetComponent<Camera>().transform.position.z
            );
        }

        void SetCameraSize()
        {
            //horizontal size is based on actual screen ratio
            float minSizeX = minSizeY * Screen.width / Screen.height;

            //multiplying by 0.5, because the ortographicSize is actually half the height
            float width = Mathf.Abs(player1.position.x - player2.position.x) * 0.5f + horizontalPadding;
            float height = Mathf.Abs(player1.position.y - player2.position.y) * 0.5f + verticalPadding;

            //computing the size
            float camSizeX = Mathf.Max(width, minSizeX);
            GetComponent<Camera>().orthographicSize = Mathf.Max(height,
                camSizeX * Screen.height / Screen.width, minSizeY);
        }

        void Update()
		{
            SetCameraPos();
            SetCameraSize();
        }

    }
}
