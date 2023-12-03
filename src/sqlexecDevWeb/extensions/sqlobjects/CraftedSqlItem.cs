using Sorling.SqlExecMeta;

namespace Sorling.sqlexecDevWeb.extensions.sqlobjects;

public record CraftedSqlItem(int Id, string Name, string SchemaName, SqlGroupFlags GroupFlag, string DBName) : ISqlItem
{
   public string Group => GroupFlag.GetGroupItemName();
}
