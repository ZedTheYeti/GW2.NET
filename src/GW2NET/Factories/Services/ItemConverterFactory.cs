// <copyright file="ItemConverterFactory.cs" company="GW2.NET Coding Team">
// This product is licensed under the GNU General Public License version 2 (GPLv2). See the License in the project root folder or the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>

namespace GW2NET.Factories.V2
{
    using System.Diagnostics;
    using GW2NET.Common;
    using GW2NET.Common.Converters;
    using GW2NET.Items;
    using GW2NET.V2.Items.Converters;
    using GW2NET.V2.Items.Json;

    public class ItemConverterFactory : ITypeConverterFactory<ItemDTO, Item>
    {
        public IConverter<ItemDTO, Item> Create(string discriminator)
        {
            var infusionSlotFlagCollectionConverter = new InfusionSlotFlagCollectionConverter(new InfusionSlotFlagConverter());
            var infusionSlotCollectionConverter = new CollectionConverter<InfusionSlotDTO, InfusionSlot>(new InfusionSlotConverter(infusionSlotFlagCollectionConverter));
            var combatAttributeCollectionConverter = new CollectionConverter<AttributeDTO, CombatAttribute>(new CombatAttributeConverter(new CombatAttributeConverterFactory()));
            var infixUpgradeConverter = new InfixUpgradeConverter(combatAttributeCollectionConverter, new CombatBuffConverter());
            switch (discriminator)
            {
                case "Armor":
                    return new ArmorConverter(new ArmorConverterFactory(), new WeightClassConverter(), infusionSlotCollectionConverter, infixUpgradeConverter);
                case "Back":
                    return new BackpackConverter(infusionSlotCollectionConverter, infixUpgradeConverter);
                case "Bag":
                    return new BagConverter();
                case "Consumable":
                    return new ConsumableConverter(new ConsumableConverterFactory(new UnlockerConverterFactory()));
                case "Container":
                    return new ContainerConverter(new ContainerConverterFactory());
                case "CraftingMaterial":
                    return new CraftingMaterialConverter();
                case "Gathering":
                    return new GatheringToolConverter(new GatheringToolConverterFactory());
                case "Gizmo":
                    return new GizmoConverter(new GizmoConverterFactory());
                case "MiniPet":
                    return new MiniatureConverter();
                case "Tool":
                    return new ToolConverter(new ToolConverterFactory());
                case "Trait":
                    return new TraitGuideConverter();
                case "Trinket":
                    return new TrinketConverter(new TrinketConverterFactory(), infusionSlotCollectionConverter, infixUpgradeConverter);
                case "Trophy":
                    return new TrophyConverter();
                case "UpgradeComponent":
                    return new UpgradeComponentConverter(new UpgradeComponentConverterFactory(), new UpgradeComponentFlagCollectionConverter(new UpgradeComponentFlagConverter()), infusionSlotFlagCollectionConverter, infixUpgradeConverter);
                case "Weapon":
                    return new WeaponConverter(new WeaponConverterFactory(), new DamageTypeConverter(), infusionSlotCollectionConverter, infixUpgradeConverter);
                default:
                    Debug.Assert(false, "Unknown type discriminator: " + discriminator);
                    return new UnknownItemConverter();
            }
        }
    }
}