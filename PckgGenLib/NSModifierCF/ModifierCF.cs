using Microsoft.SqlServer.Dts.Runtime;

namespace PckgGenLib.NSModifierCF
{
    public partial class ModifierCF: IModifierCF
    {
        public ModifierCF(Package p)
        {
            this.p = p;
        }

        private Package p;
    }
}
