using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    /*
     * essas classe representa o relacionamento n - n entre usuários e skills
     */
    public class UserSkill : BaseEntity
    {
        public UserSkill(int idUser, int idSkill)
        {
            IdUser = idUser;
            IdSkill = idSkill;
        }

        public int IdUser { get; private set; }
        public int IdSkill { get; private set; }
    }
}
