using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool
{
    public partial class Client
    {

        public string allClient // объединями ФИО в единое целое
        {
            get
            {
                return FirstName + " " + LastName + " " + Patronymic;
            }
        }
    }
}
