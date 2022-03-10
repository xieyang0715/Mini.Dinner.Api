using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Model
{
    [Serializable]
    public class Entity
    {
        public Entity()
        {
            Id = Uuid.NewUuid();
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>		
        [ExplicitKey]
        public Uuid Id { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

    }
}
