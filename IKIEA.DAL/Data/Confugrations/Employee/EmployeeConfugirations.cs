using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKIEA.DAL.Models.Employess;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IKIEA.DAL.Common.Enums;

namespace IKIEA.DAL.Data.Confugrations.Employee
{
    internal class EmployeeConfugirations : IEntityTypeConfiguration<Employees>
    {

        public void Configure(EntityTypeBuilder<Employees> builder)
        {
           
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender).HasConversion(
                (gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );

            builder.Property(E => E.EmployeeType).HasConversion(
                (emp) => emp.ToString(),
                (emp) => (EmployeeType)Enum.Parse(typeof(EmployeeType), emp)
                );
        }
    }
}
