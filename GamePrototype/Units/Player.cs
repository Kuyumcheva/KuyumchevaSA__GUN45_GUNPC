using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {            
        }

        public EquipItem GetEquippedWeapon()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var weapon))
            {
                return weapon;
            }
            else if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeWeapon))
            {
                return rangeWeapon;
            }
            return null;
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                return BaseDamage + weapon.Damage;
            }
            else if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeItem) && rangeItem is RangeWeapon rangeWeapon)
            {
                return BaseDamage + rangeWeapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++) 
            {
                if (items[i] is EconomicItem economicItem) 
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                if (_equipment.ContainsKey(equipItem.Slot))
                {
                    Console.WriteLine($"You have found a new {equipItem.Name}. Do you want to change your current {_equipment[equipItem.Slot].Name}? (Y/N)");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        var oldItem = _equipment[equipItem.Slot];
                        _equipment[equipItem.Slot] = equipItem;
                        Console.WriteLine($"The equipment was successfully replaced {oldItem.Name} -> {equipItem.Name}");
                    }
                    else
                    {
                        base.AddItemToInventory(item);
                    }
                }
                else
                {
                    _equipment.Add(equipItem.Slot, equipItem);
                    Console.WriteLine($" {equipItem.Name} was successfully equiped");
                }
            }
            else
            {
                base.AddItemToInventory(item);
            }
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion) 
            {
                Health += healthPotion.HealthRestore;
            }
            else if (economicItem is Grindstone grindstone)
            {
                UseGrindstone(grindstone);
            }
        }

        private void UseGrindstone(Grindstone grindstone)
        {
            foreach (var item in _equipment.Values)
            {
                if (item is EquipItem equipItem)
                {
                    equipItem.Repair(grindstone.GrindstoneRepair);
                    Console.WriteLine($"Restored {grindstone.GrindstoneRepair} durability to {item.Name} (current: {equipItem.Durability})");
                }
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            uint totalDefence = 0;

            if (_equipment.TryGetValue(EquipSlot.Armour, out var armourItem) && armourItem is Armour armour)
            {
                totalDefence += armour.Defence;
                armour.ReduceDurability(1);
                Console.WriteLine($"The armor durability has decreased by 1. Remaining: {armour.Durability}");
            }

            if (_equipment.TryGetValue(EquipSlot.Helmet, out var helmetItem) && helmetItem is Helmet helmet)
            {
                totalDefence += helmet.Defence;
                helmet.ReduceDurability(1);
                Console.WriteLine($"The helmet durability has decreased by 1. Remaining: {helmet.Durability}");
            }

            damage -= (uint)(damage * (totalDefence / 100f));
            return damage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++) 
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}
