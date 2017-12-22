﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionCollapse {
    public class PlayerManager : Photon.PunBehaviour, IPunObservable {

        #region IPunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            //throw new System.NotImplementedException ();
        }
        #endregion

        #region Public Variables

        [Tooltip("The current Health of our player")]
        public float Health = 1f;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public float Direction;

        #endregion

        private Health health;

        void awake() {

            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.isMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
        }

        // Use this for initialization
        void Start() {
			if (photonView.isMine) {
				GameObject.Find ("UIManager").SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
			}
            Direction = transform.rotation.eulerAngles.y;
            health = transform.GetComponent<Health>();
        }

        // Update is called once per frame
        void Update() {
            Direction = transform.rotation.eulerAngles.y;
            Health = health.health/health.maxHealth;
    
    }
    }
}
