using System;
using UnityEngine;

    [CreateAssetMenu]
    public class DoorData:ScriptableObject
    {
        public Sprite EntitySprite
        {
            get {
                if(entitySprite == null)
                {
                    entitySprite = Resources.Load<Sprite>(($"image/Entity/{index}"));
                    Debug.Log($"image/Entity/{index}");
                    Debug.Log(entitySprite.name);
                }
                
                return entitySprite;
            }
        }

        public Sprite EntityHurtSprite
        {
            get {
                if(entityHurtSprite == null)
                {
                    entityHurtSprite = Resources.Load<Sprite>(($"image/Entity/{index}_2"));
                }
                return entityHurtSprite;
            }
        }

        public Sprite CloseSprite
        {
            get {
                if(closeSprite == null)
                {
                    closeSprite = Resources.Load<Sprite>(String.Format("image/Gate/door{0:D2}_close", index));
                    Debug.Log(String.Format("image/Gate/door{0:D2}_close", index));
                    Debug.Log(closeSprite.name);
                }
                return closeSprite;
            }
        }

        public Sprite OpenSprite
        {
            get
            {
                if(openSprite == null)
                {
                    openSprite = Resources.Load<Sprite>(String.Format("image/Gate/door{0:D2}_open", index));
                    Debug.Log(String.Format("image/Gate/door{0:D2}_open", index));
                }
                return openSprite;
            }
        }

        public int index;
        public string Doorname;
        public int hp;
        public int ability;
        public bool isRare;


        private Sprite entitySprite;
        private Sprite entityHurtSprite;
        private Sprite closeSprite;
        private Sprite openSprite;

        
    }
