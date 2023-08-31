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
        public int numberOfBullets;
        public override string getItemName()
        {
            return "Gun ("+numberOfBullets.ToString()+")";
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 10;
        public override int floorNumberWhereItemSpawns => 0;
        string noAmmoSoundFileName = "NoAmmo";
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
            Bullets bullet = character.inventory.items.OfType<Bullets>().FirstOrDefault();
            if (numberOfBullets < 1)
            {
                if (character.inventory.items.OfType<Bullets>().FirstOrDefault() == null) { alerts.Add("You can't shoot, you have no bullets"); }
                else { alerts.Add("First you need to load the gun"); }
                this.soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(noAmmoSoundFileName)));
                return;
            }
            else
            {
                this.soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(useSoundFileName)));
                numberOfBullets--;
                Monster monster = character.mapWhereIsLocated.mapInformations.charactersOnMap.OfType<Monster>().FirstOrDefault();
                if (monster != null)
                {
                    monster.Die("Died because of the shooting", alerts);
                }
            }
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