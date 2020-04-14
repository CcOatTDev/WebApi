using Api.Models.Entities.MySqlDatacenter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Persists.MySqlDatacenter
{
    public class UserPersist : BasePersist<user, MysqlDatacenterEntities>
    {
        public UserPersist(DbContext context) : base(context)
        {

        }

        public List<user> GetUsers()
        {
            return dbSet.ToList();
        }
    }
}
