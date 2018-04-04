using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EF_CF_Data.Mappings
{
    [InheritedExport(typeof(IEntityMapping))]
    public interface IEntityMapping
    {
        /// <summary>
        /// 添加EntityTypeConfiguration
        /// </summary>
        /// <param name="registrar"></param>
        void Configure(DbModelBuilder modeBuilder);
    }
}
