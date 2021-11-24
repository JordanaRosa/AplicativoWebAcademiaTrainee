using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AplicativoWebAcademiaTrainee.Models;

namespace AplicativoWebAcademiaTrainee.Data
{
    public class AplicativoWebAcademiaTraineeContext : DbContext
    {
        public AplicativoWebAcademiaTraineeContext (DbContextOptions<AplicativoWebAcademiaTraineeContext> options)
            : base(options)
        {
        }

        public DbSet<AplicativoWebAcademiaTrainee.Models.PessoaModel> PessoaModel { get; set; }
    }
}
