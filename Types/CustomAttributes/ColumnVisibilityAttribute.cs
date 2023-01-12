using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types.CustomAttributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnVisibilityAttribute : Attribute
    {
        private byte visibility;
        public byte Visibility 
        {
            get
            {
                return visibility;
            } 
            set
            {
                visibility = value;
            }
        }

        public ColumnVisibilityAttribute(Vis vis) 
        {
            this.visibility = (byte)vis;
        }

    }

    public enum Vis : byte
    {
       
        Visible = 0,
        Hidden = 1,
        Collapsed = 2
    }
}
