using FOBOS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Data
{
    public class TypeMapper
    {
        public void MapperInit()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(Bank), new ColumnAttributeTypeMapper<Bank>());
            Dapper.SqlMapper.SetTypeMap(typeof(Card), new ColumnAttributeTypeMapper<Card>());
            Dapper.SqlMapper.SetTypeMap(typeof(Category), new ColumnAttributeTypeMapper<Category>());
            Dapper.SqlMapper.SetTypeMap(typeof(CategoryType), new ColumnAttributeTypeMapper<CategoryType>());
            Dapper.SqlMapper.SetTypeMap(typeof(Goal), new ColumnAttributeTypeMapper<Goal>());
            Dapper.SqlMapper.SetTypeMap(typeof(Statement), new ColumnAttributeTypeMapper<Statement>());
            Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
        }
    }
}
