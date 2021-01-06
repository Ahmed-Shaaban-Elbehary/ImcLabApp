using ImcLabApp.Models.BackUpSystemModels;
using System.Data.Entity;

namespace ImcLabApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name = AppConnection")
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Labs> Labs { get; set; }
        public DbSet<Radios> Radios { get; set; }
        public DbSet<Tumors> Tumors { get; set; }
        public DbSet<tb_labsBackUp> tb_LabsBackUps { get; set; }
        public DbSet<tb_radiosBackUp> tb_RadiosBackUps { get; set; }
        public DbSet<tb_TumorsBackUp> tb_TumorsBackUps { get; set; }
        public DbSet<PatientsRegisteration> PatientsRegisterations { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<RequestesBackUp> RequestsBackUps { get; set; }
    }
}