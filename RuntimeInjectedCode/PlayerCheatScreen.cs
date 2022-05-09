using UnityEngine;

namespace RuntimeInjectedCode
{
    public class PlayerCheatScreen : MonoBehaviour, ICheatScreen
    {
        private bool _noclip = false;
        private float _originalRadius;
        private float _originalStepOffset;
        private bool _bigStep = false;
        private bool _forceJump = false;

        public string Name => "Player";

        public void DrawUI()
        {
            if (GUILayout.Button("Noclip"))
            {
                // Also disable OOB detection by force
                StanleyController.OnOutOfBounds = null;
                
                var charControl = StanleyController.Instance.GetComponent<CharacterController>();

                _noclip = !_noclip;

                if (_noclip)
                {
                    _originalRadius = charControl.radius;
                    charControl.enabled = false;
                    charControl.detectCollisions = false;
                    charControl.radius = float.PositiveInfinity;
                }
                else
                {
                    charControl.detectCollisions = true;
                    charControl.radius = _originalRadius;
                    charControl.enabled = true;
                }
                
            }

            if (GUILayout.Button("Big Steps"))
            {
                var charControl = StanleyController.Instance.GetComponent<CharacterController>();
                _bigStep = !_bigStep;
                
                if (_bigStep)
                {
                    _originalStepOffset = charControl.stepOffset;
                    charControl.stepOffset = charControl.height - 0.1f;
                }
                else
                {
                    charControl.stepOffset = _originalStepOffset;
                }
            }
            
            if (GUILayout.Button("Can Jump"))
            {
                _forceJump = !_forceJump;

                RefUtil.Get<BooleanConfigurable>("jumpConfigurable", StanleyController.Instance).SetValue(_forceJump);
                RefUtil.Set("executeJump", StanleyController.Instance, _forceJump);
            }

            if (GUILayout.Button("Bark!"))
            {
                if (Singleton<GameMaster>.Instance.barking)
                    Singleton<GameMaster>.Instance.BarkModeOff();
                else
                    Singleton<GameMaster>.Instance.BarkModeOn();
            }
            
            if (GUILayout.Button("Boxes Next Time"))
            {
                Singleton<GameMaster>.Instance.Boxes();
            }

            if (GUILayout.Button("Toggle Bucket"))
            {
                StanleyController.Instance.Bucket.SetBucket(!BucketController.HASBUCKET, false, true, true);
            }

            if (GUILayout.Button("Neuter Out-Of-Bounds Detection"))
            {
                StanleyController.OnOutOfBounds = null;
            }
            
            if (GUILayout.Button("Start Floating"))
            {
                StanleyController.Instance.StartFloating();
            }

            if (GUILayout.Button("Stop Floating"))
            {
                StanleyController.Instance.EndFloating();
            }

            if (GUILayout.Button("Reset To Map Start"))
            {
                StanleyController.Instance.NewMapReset();
            }
        }

        public void Update()
        {
            if (_noclip)
            {
                StanleyController.Instance.GetComponent<CharacterController>().enabled = false;

                Vector3 movementInput = new Vector3();
                float y = Singleton<GameMaster>.Instance.stanleyActions.Movement.Y;
                movementInput.x = Singleton<GameMaster>.Instance.stanleyActions.Movement.X;
                movementInput.z = y;
                movementInput = Vector3.ClampMagnitude(movementInput, 1f);
                movementInput *= Time.deltaTime * 2.0f;

                StanleyController.Instance.transform.position += StanleyController.Instance.camTransform.TransformDirection(movementInput);
            }
        }
    }
}