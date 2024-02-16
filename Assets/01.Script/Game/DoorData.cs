using System;
using UnityEngine;

    [CreateAssetMenu]
    public class DoorData:ScriptableObject
    {
        public Sprite EntitySprite
        {
            get {
                if(entitySprite == null)
                    entitySprite = Resources.Load<Sprite>(($"image/Entity/{index}.png"));
                return entitySprite;
            }
        }

        public Sprite EntityHurtSprite
        {
            get {
                if(entityHurtSprite == null)
                    entityHurtSprite = Resources.Load<Sprite>(($"image/Entity/{index}_2.png"));
                return entityHurtSprite;
            }
        }

        public Sprite CloseSprite
        {
            get {
                if(closeSprite == null)
                    closeSprite = Resources.Load<Sprite>(String.Format("image/Door/door{0:D2}_close.png", index));
                return closeSprite;
            }
        }

        public Sprite OpenSprite
        {
            get
            {
                if(openSprite == null)
                    openSprite = Resources.Load<Sprite>(String.Format("image/Door/door{0:D2}_open.png", index));
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
