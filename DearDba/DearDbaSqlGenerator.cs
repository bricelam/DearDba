using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

class DearDbaSqlGenerator : IMigrationsSqlGenerator
{
    readonly MigrationsSqlGeneratorDependencies _dependencies;

    public DearDbaSqlGenerator(MigrationsSqlGeneratorDependencies dependencies)
        => _dependencies = dependencies;

    public IReadOnlyList<MigrationCommand> Generate(
        IReadOnlyList<MigrationOperation> operations,
        IModel? model,
        MigrationsSqlGenerationOptions options)
    {
        var builder = new MigrationCommandListBuilder(_dependencies);

        builder
            .AppendLine("Dear DBA,")
            .AppendLine()
            .AppendLine($"We lowly developers would once again petition you for a new database.");

        var firstTable = true;
        var firstIndex = true;
        foreach (var operation in operations)
        {
            if (operation is CreateTableOperation table)
            {
                builder
                    .AppendLine()
                    .Append($"We'll {(firstTable ? "" : "also ")}need a {table.Name} table with the following columns.");

                foreach (var column in table.Columns)
                {
                    var isPk = table.PrimaryKey != null && table.PrimaryKey.Columns.Length == 1 && table.PrimaryKey.Columns[0] == column.Name;
                    var isUnique = isPk || table.UniqueConstraints.Any(uc => uc.Columns.Length == 1 && uc.Columns[0] == column.Name);
                    var fk = table.ForeignKeys.FirstOrDefault(fk => fk.Columns.Length == 1 && fk.Columns[0] == column.Name);

                    builder.Append($" {(column.IsNullable ? "An optional" : "A required")} {column.Name} column to store {((isUnique) ? "unique " : "")}{column.ColumnType} values{(fk == null ? "" : $" that reference the {fk.PrincipalTable} table")}.");

                    if (isPk)
                        builder.Append($" We'll use the {column.Name} value as the primary way of identifying rows in the table.");
                }

                builder.AppendLine();

                firstTable = false;
            }
            else if (operation is CreateIndexOperation index
                && index.Columns.Length == 1)
            {
                builder
                    .AppendLine();

                if (firstIndex)
                    builder
                        .AppendLine("We don't really know what an index is, but in the past, you've been able to use them to improve performance and compensate for some of our more incompetent queries. We will, of course, defer to your far superior expertise on this matter. Nevertheless, we will suggest a few that we're reasonably certain about.")
                        .AppendLine();

                builder.AppendLine($"We {(firstIndex ? "" : "also ")}think the {index.Columns[0]} column on the {index.Table} table should be indexed.");

                firstIndex = false;
            }
        }

        builder
            .AppendLine()
            .AppendLine("Sincerely,")
            .AppendLine("The Developers")
            .EndCommand();

        return builder.GetCommandList();
    }
}
