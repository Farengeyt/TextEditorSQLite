using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorSQLite
{
    public class ApplicationContext : DbContext
    {
        private static ApplicationContext db;
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<File> Files { get; set; }

        public static ApplicationContext Instance
        {
            get
            {
                if (db == null)
                {
                    db = new ApplicationContext();
                }

                return db;
            }
        }
    }
}
