namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem
    {
        public override bool Stackable => false;

        public uint GrindstoneRepair => 4;
        public Grindstone(string name) : base(name)
        {
        }    
    }
}
