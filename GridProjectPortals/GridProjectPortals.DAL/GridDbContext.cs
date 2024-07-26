
using GridProjectPortals.Domain.Tables;
using Microsoft.EntityFrameworkCore;

namespace GridProjectPortals.DAL
{
    public class GridDbContext:DbContext
    {
        public string ConnectionString {  get; set; }
        public GridDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<tblGridPages>  tblGridPages { get; set; }
        public DbSet<tblGridColumnDef> tblGridColumnDefs { get; set; }
        public DbSet<tblGridColumnDetails>  tblGridColumnDetails { get; set; }
        public DbSet<tblEmployee>tblEmployees { get; set; }
        public DbSet<tblGridDataTypeOperator> tblGridDataTypeOperators { get; set; }
        public DbSet<tblComments> tblComments { get; set; }
        public DbSet<tblReplayComment> tblReplayComments { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }

    }
}
