using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Classes
{
    /// <summary>
    /// Это модель для описания проекта
    /// </summary>
   public class ProjectModel
    {
        public int Id { get; set; }
        public string Target { get; set; }
        public string Task { get; set; }
        public string Term { get; set; }
        public string Resourses { get; set; }
        public long Budget { get; set; }
        public int idEmployee { get; set; }
        public string EmployeeName { get; set; }
        public string Readiness { get; set; }
    }
}
