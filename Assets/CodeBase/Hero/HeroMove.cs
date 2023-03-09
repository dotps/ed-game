using System;
using CodeBase.CameraLogic;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Progress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public CharacterController CharacterController;
        public float MovementSpeed = 4.0f;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = ServiceLocator.Instance.GetSingleInstance<IInputService>();;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                //Трансформируем экранныые координаты вектора в мировые
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.worldData.positionOnLevel = new PositionOnLevel(GetCurrentLevelName(), transform.position.AsVector3Data());
        }

        private static string GetCurrentLevelName() => 
            SceneManager.GetActiveScene().name;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.worldData.positionOnLevel.levelName != GetCurrentLevelName()) return;
            var savedPosition = progress.worldData.positionOnLevel.position;
            if (savedPosition == null) return;
            MoveToPosition(savedPosition);
        }

        private void MoveToPosition(Vector3Data savedPosition)
        {
            CharacterController.enabled = false; // необходимо выключить CharacterController при изменении позиции, т.к он связан с физикой и могут быть глюки
            transform.position = savedPosition.AsVector3().AddY(CharacterController.height);
            CharacterController.enabled = true; // включаем на новом месте
        }
    }
}