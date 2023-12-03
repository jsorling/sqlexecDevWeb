using Sorling.SqlExecMeta;
using Sorling.SqlExecMeta.constraints;

namespace Sorling.sqlexecDevWeb.extensions.sqlobjects;

public static class SqlCheckConstraintListItemExtensions
{
   public static CraftedSqlItem GetTableSqlItem(this SqlCheckConstraintListItem sqlCheckConstraint) {
      ArgumentException.ThrowIfNullOrEmpty(sqlCheckConstraint.ParentSchema, nameof(sqlCheckConstraint.ParentSchema));
      ArgumentException.ThrowIfNullOrEmpty(sqlCheckConstraint.ParentName, nameof(sqlCheckConstraint.ParentName));

      return new CraftedSqlItem(sqlCheckConstraint.Parent_object_id, sqlCheckConstraint.ParentName
         , sqlCheckConstraint.ParentSchema, SqlGroupFlags.Tables, sqlCheckConstraint.DBName);
   }
}
