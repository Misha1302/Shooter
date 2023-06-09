namespace Player
{
    using System;
    using System.Collections.Generic;
    using Player.IInitializable;
    using Player.Jump;
    using UnityEngine;

    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private JumpController jumpController;
        [SerializeField] private AutoJumpController autoJumpController;
        [SerializeField] private GameObject[] managerInitializableObjects;

        public PlayerController PlayerController => playerController;
        public PlayerMovement PlayerMovement => playerMovement;
        public JumpController JumpController => jumpController;
        public AutoJumpController AutoJumpController => autoJumpController;


        private void Start()
        {
            InitScripts(managerInitializableObjects);
        }

        private void InitScripts(GameObject[] arr)
        {
            foreach (var managerInitializable in arr)
            {
                var list = new List<IManagerInitializable>();
                GetComponents(list);

                if (list.Count == 0)
                    throw new InvalidOperationException(
                        $"{managerInitializable.name} must implement IManagerInitializable interface"
                    );

                list.ForEach(x => x.Init(this));
            }
        }
    }
}