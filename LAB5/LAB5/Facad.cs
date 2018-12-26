using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5
{
    /// <summary>
    /// управляющий блок
    /// </summary>
    class Facad
    {
        public Edit_data link_e;
        public Select_data link_s;
        public void go_edit()
        {
            link_e = new Edit_data();
            link_e = null;
        }
        public void go_select()
        {
            link_s = new Select_data();
            link_s = null;
        }
    }
}
