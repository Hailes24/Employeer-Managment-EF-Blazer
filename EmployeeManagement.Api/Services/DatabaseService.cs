using EmployeeManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Services
{
    public class DatabaseService
    {
        public readonly AppDbContext _context;

        public DatabaseService(AppDbContext context)
        {
            _context = context;
        }

        public List<TableInfo> GetTableNames()
        {
            var tableNames = _context.Model.GetEntityTypes()
                .Select(t => new TableInfo { TableName = t.GetTableName() })
                .ToList();

            return tableNames;
        }
    }
    public class TableInfo
    {
        public string TableName { get; set; }
    } 
}
