using FOBOS_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Repositories
{
    public abstract class _BaseRepository
    {
        protected readonly DataConnection db;

        public _BaseRepository()
        {
            this.db = new DataConnection();
        }
    }
}
