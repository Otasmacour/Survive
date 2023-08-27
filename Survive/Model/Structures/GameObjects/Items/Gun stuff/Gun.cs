using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Gun : Item
    {

        public override string getItemName()
        {
            return "Gun";
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 10;
        public override int floorNumberWhereItemSpawns => 0;
        public override string useSoundFileName => "firingGun";
        public override string dropSoundFileName => "FallingTool";
        public Gun(SoundsController soundsController) : base(soundsController)
        {
            this.soundsController = soundsController;
        }
        public override void PickUp(Character character)
        {
            pickUp(character);
        }
        public override void Drop(Character character)
        {
            drop(character);
        }
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts)
        {
            Bullet bullet = new Bullet(soundsController);
            bool hasABullet = false;
            foreach(Item item in character.inventory.items)
            {
                if(item is Bullet) { hasABullet = true; bullet = (Bullet)item; break; }
            }
            if(hasABullet == false) { alerts.Add("You can't shoot, you have no bullets"); return; }
            bullet.Use(character, mapHelper, alerts);
            this.soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(useSoundFileName)));
            character.inventory.InventoryUpdate();
        }
        public override char GetSymbol(Map map)
        {
            return 'G';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}