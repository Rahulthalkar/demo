using GridProjectPortals.Domain.Tables;
using Microsoft.EntityFrameworkCore;

namespace GridProjectPortals.DB
{
    public class GridDbContext:DbContext
    {
        public string ConnectionString { get; set; }

        public GridDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var counter = 1;
            //REMOVES ON DELETE CASCADE FROM EVERYWHERE
            //REFERENCE: https://entityframeworkcore.com/knowledge-base/34768976/specifying-on-delete-no-action-in-entity-framework-7-
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
            base.OnModelCreating(modelBuilder);

            counter = 1;
            modelBuilder.Entity<tblGridPages>().HasData(
                new tblGridPages { Id = counter++, Page = "Employees" }
            );
            counter = 1;
            modelBuilder.Entity<tblGridColumnDef>().HasData(
                new tblGridColumnDef
                {
                    Id = counter++,
                    GridName = "grdEmployeeDetails",
                    UserId = 1,
                    PageId = 1,
                   
                });
            counter = 1;
            modelBuilder.Entity<tblGridColumnDetails>().HasData(
                new tblGridColumnDetails
                {
                    Id = counter++,
                    GridId = 1,
                    ColumnName = "id",
                    ColumnDataType = "int",
                    isSearchable = false,
                    isVisible = false,
                    Sequence = 1,
                    isFix = false,
                },
                new tblGridColumnDetails
                {
                    Id = counter++,
                    GridId = 1,
                    ColumnName = "firstname",
                    ColumnDataType = "string",
                    isSearchable = true,
                    isVisible = true,
                    Sequence = 2,
                    isFix = false,
                },
                new tblGridColumnDetails
                {
                    Id = counter++,
                    GridId = 1,
                    ColumnName = "lastname",
                    ColumnDataType = "string",
                    isSearchable = true,
                    isVisible = true,
                    Sequence = 3,
                    isFix = false,
                },
                new tblGridColumnDetails
                {
                    Id = counter++,
                    GridId = 1,
                    ColumnName = "username",
                    ColumnDataType = "string",
                    isSearchable = true,
                    isVisible = true,
                    Sequence = 4,
                    isFix = false,
                },
                new tblGridColumnDetails
                {
                    Id = counter++,
                    GridId = 1,
                    ColumnName = "phone",
                    ColumnDataType = "string",
                    isSearchable = true,
                    isVisible = true,
                    Sequence = 5,
                    isFix = false,
                }
                );
            counter = 1;
            modelBuilder.Entity<tblGridDataTypeOperator>().HasData(
                new tblGridDataTypeOperator { Id = counter++, DataType = "int", Operator = ">", OperatorInWords = "GreaterThan" },
                new tblGridDataTypeOperator { Id = counter++, DataType = "int", Operator = "<", OperatorInWords = "LessThan" },
                new tblGridDataTypeOperator { Id = counter++, DataType = "int", Operator = "=", OperatorInWords = "EqualsTo" },
                new tblGridDataTypeOperator { Id = counter++, DataType = "string", Operator = "", OperatorInWords = "StartsWith" },
                new tblGridDataTypeOperator { Id = counter++, DataType = "string", Operator = "", OperatorInWords = "EndsWith" },
                new tblGridDataTypeOperator { Id = counter++, DataType = "string", Operator = "", OperatorInWords = "Contains" },
                new tblGridDataTypeOperator { Id = counter++, DataType = "string", Operator = "", OperatorInWords = "In" }
                );
        }

        // set dbset here
        public DbSet<tblGridPages> tblGridPages { get; set; }
        public DbSet<tblGridColumnDetails> tblGridColumnDetails { get; set; }
        public DbSet<tblGridColumnDef> tblGridColumnDefs { get; set; }
        public DbSet<tblEmployee> tblEmployees { get; set; }
        public DbSet<tblGridDataTypeOperator> tblGridDataTypeOperators { get; set;}
        public DbSet<tblComments> tblComments { get; set; }
        public DbSet<tblReplayComment> tblReplayComments { get; set; }
        public DbSet<tblUser>tblUsers { get; set; }
    }
}
